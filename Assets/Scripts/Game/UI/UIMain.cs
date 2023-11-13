using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace Game.UI
{
	public enum BtnType
	{
		Electric,
		AC,
		Concrete,
		Furniture
	}

	public class UIMain : NoodUI 
	{
        #region Events
        public Action OnBackAction;
        public Action OnElectricBtn;
        public Action OnTakeScreenshot;
        public Action<Texture2D> OnScreenShotSuccess;
        public Action<List<ARObjectSO>> OnInventoryConfirm;
        #endregion

        [SerializeField] private GameObject _buttonZone;
        [SerializeField] private Color _normalColor, _chosenColor;
        [SerializeField] private CustomBtn _electricBtn, _acBtn, _concreteBtn, _furnitureBtn;
        [SerializeField] private Button _takeScreenshotBtn, _backBtn, _shopBtn;
        [SerializeField] private GameObject _inventoryGO;
        [SerializeField] private GameObject _SSObject;
        [SerializeField] private RawImage _rawImage;
        [SerializeField] private GameObject _SSBelowPosition, _SSHidePosition;
        [SerializeField] private GameObject _inventHidePosition, _inventShowPosition;
        [SerializeField] private UIInventory _uiInventory;


        private UIDebug _uiDebug;
        private UISelector _uiSelector;
        private ARObject _previousObj;
        private Texture2D _screenShotImage;

        public static UIMain Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UIMain>("Prefab/Game/UI/UIMain"), parent);
		}

		void Awake()
		{
            _takeScreenshotBtn.onClick.AddListener(() =>
            {
                HideUI();
                NoodyCustomCode.StartDelayFunction(() =>
                {
                    OnTakeScreenshot?.Invoke();
                }, 0.2f);
            });
            _shopBtn.onClick.AddListener(() => 
            {
                OpenInventory(true);
                Debug.Log("Shop");
            });
            OnInventoryConfirm += (aRObjectSOs) => 
            {
                // Close inventory
                OpenInventory(false);
            };
            _uiInventory.OnBackAction += () =>
            {
                OpenInventory(false);
            };
            OnScreenShotSuccess += (Texture2D image) => 
            {
                _screenShotImage = image;
            };
            AppManager.OnSaveSSCallback = AnimateScreenshot;
            _electricBtn._normalColor = _normalColor;
            _acBtn._normalColor = _normalColor;
            _concreteBtn._normalColor = _normalColor;
            _furnitureBtn._normalColor = _normalColor;

            _electricBtn._choseColor = _chosenColor;
            _acBtn._choseColor = _chosenColor;
            _concreteBtn._choseColor = _chosenColor;
            _furnitureBtn._choseColor = _chosenColor;
		}

		void Start()
		{
            // _uiDebug = UILoader.LoadUI<UIDebug>();
            _uiSelector = UILoader.LoadUI<UISelector>();

            SetBtnAction();
            OnInventoryConfirm += (list) => 
            {
                _electricBtn.SetChosen(false);
                _acBtn.SetChosen(false);
                _concreteBtn.SetChosen(false);
                _furnitureBtn.SetChosen(true);
            };
        }

        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(_uiSelector.AimPosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                if(hit.collider.TryGetComponent<ARObject>(out ARObject aRObject))
                {

                    if(_previousObj == null)
                    {
                        _previousObj = aRObject;
                    }
                    else
                    {
                        if(_previousObj != aRObject)
                        {
                            _previousObj.ActiveOutlineAndShowInfo(false);
                            _previousObj = aRObject;
                        }
                    }
                    aRObject.ActiveOutlineAndShowInfo(true);
                }
                else
                {
                    if (!_previousObj)
                        return;
                    _previousObj.ActiveOutlineAndShowInfo(false);
                }
            }
            else
            {
                if (!_previousObj)
                    return;
                _previousObj.ActiveOutlineAndShowInfo(false);
            }
        }
        
        void OnDisable()
        {
            UILoader.CloseUI<UIInfoPanel>();
        }
        
        public void HideUI()
        {
            _backBtn.gameObject.SetActive(false);
            _shopBtn.gameObject.SetActive(false);
            _buttonZone.gameObject.SetActive(false);
        }
        public void ShowUI()
        {
            _backBtn.gameObject.SetActive(true);
            _shopBtn.gameObject.SetActive(true);
            _buttonZone.gameObject.SetActive(true);
        }

        public void OpenInventory(bool value)
        {
            UILoader.CloseUI<UIInfoPanel>();
            _inventoryGO.transform.DOKill();
            if(value)
            {
                // Open
                _inventoryGO.SetActive(true);
                _inventoryGO.transform.DOMove(_inventShowPosition.transform.position, 0.3f);
                UILoader.CloseUI<UISelector>();
            }
            else
            {
                // Close
                _inventoryGO.transform.DOMove(_inventHidePosition.transform.position, 0.2f).OnComplete(() => _inventoryGO.SetActive(false));
                UILoader.LoadUI<UISelector>();
            }
        }

        public Vector3 GetRaycastPosition()
        {
            Camera camera = Camera.main;
            Ray raycast = camera.ScreenPointToRay(_uiSelector.AimPosition);
            if (Physics.Raycast(raycast, out RaycastHit raycastHit))
            {
                raycastHit.transform.gameObject.SetActive(false);
                return raycastHit.point;
            }
            else
            {
                return Vector3.zero;
            }
        }

        private void AnimateScreenshot(bool success, string path)
        {
            if(success)
            {
                NoodyCustomCode.StartDelayFunction(() =>
                {
                    ShowUI();
                    _SSObject.SetActive(true);
                    _rawImage.gameObject.SetActive(true);
                    _rawImage.texture = _screenShotImage;
                    _SSObject.transform.localScale = Vector3.one;
                    _SSObject.transform.position = Vector3.zero;
                    _SSObject.transform.DOScale(0.2f, 0.5f);
                    _SSObject.transform.DOMove(_SSBelowPosition.transform.position, 0.5f);
                    NoodyCustomCode.StartDelayFunction(() =>
                    {
                        _SSObject.transform.DOMove(_SSHidePosition.transform.position, 1f).SetEase(Ease.InBounce);
                    }, 1f);
                }, 0.2f);
            }
        }

        private void SetBtnAction()
        {
            _backBtn.onClick.AddListener(() => OnBackAction?.Invoke());
            _electricBtn.AddButtonAction(() =>
            {
                OnElectricBtn?.Invoke();
                HouseManager.Instance.ChangeShowObject(ObjectType.Electricity);
            });
            _acBtn.AddButtonAction(() =>
            {
                HouseManager.Instance.ChangeShowObject(ObjectType.AC);
            });
            _concreteBtn.AddButtonAction(() =>
            {
                HouseManager.Instance.ChangeShowObject(ObjectType.Concrete);
            });
            _furnitureBtn.AddButtonAction(() =>
            {
                HouseManager.Instance.ChangeShowObject(ObjectType.Furniture);
            });
        }
    }
}
