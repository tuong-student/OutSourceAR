using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI;
using UnityEngine.UI;
using App;
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
        [SerializeField] private Color _normalColor, _chosenColor;
        [SerializeField] private CustomBtn _electricBtn, _acBtn, _concreteBtn, _furnitureBtn;
        [SerializeField] private Button _takeScreenshotBtn, _backBtn, _moreBtn;
        [SerializeField] private GameObject _inventoryGO, _inventoryPanel;
        [SerializeField] private GameObject _SSObject;
        [SerializeField] private RawImage _rawImage;
        [SerializeField] private GameObject _SSBelowPosition, _SSHidePosition;

        public Func<Texture2D> OnTakeScreenshot;

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
            // Read data from Global.data to Instantiate room

            _takeScreenshotBtn.onClick.AddListener(() =>
            {
                HideUI();
                NoodyCustomCode.StartDelayFunction(() =>
                {
                    _screenShotImage = OnTakeScreenshot?.Invoke();
                }, 0.2f);
            });
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

            _electricBtn.SetChosen(false);
            _acBtn.SetChosen(false);
            _concreteBtn.SetChosen(false);
            _furnitureBtn.SetChosen(false);

            SetBtnAction();
        }

        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(_uiSelector.AimPosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                if(hit.collider.TryGetComponent<ARObject>(out ARObject aRObject))
                {
                    aRObject.ActiveOutlineAndShowInfo(true);

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
        
        public void HideUI()
        {
            _acBtn.gameObject.SetActive(false);
            _backBtn.gameObject.SetActive(false);
            _moreBtn.gameObject.SetActive(false);
            _concreteBtn.gameObject.SetActive(false);
            _electricBtn.gameObject.SetActive(false);
            _furnitureBtn.gameObject.SetActive(false);
            _takeScreenshotBtn.gameObject.SetActive(false);
        }
        public void ShowUI()
        {
            _acBtn.gameObject.SetActive(true);
            _backBtn.gameObject.SetActive(true);
            _moreBtn.gameObject.SetActive(true);
            _concreteBtn.gameObject.SetActive(true);
            _electricBtn.gameObject.SetActive(true);
            _furnitureBtn.gameObject.SetActive(true);
            _takeScreenshotBtn.gameObject.SetActive(true);
        }

        public void OpenInventory(bool value)
        {
            _inventoryGO.transform.DOKill();
            if(value)
            {
                // Open
                _inventoryPanel.SetActive(true);
                _inventoryGO.transform.localScale = Vector3.zero;
                _inventoryGO.transform.DOScale(1, 0.5f);
            }
            else
            {
                // Close
                _inventoryGO.transform.DOScale(0, 0.2f).OnComplete(() => _inventoryPanel.SetActive(false));
            }
        }

        public Vector3 GetRaycastPosition()
        {
            Camera camera = Camera.main;
            Ray raycast = camera.ScreenPointToRay(_uiSelector.AimPosition);
            if (Physics.Raycast(raycast, out RaycastHit raycastHit))
            {
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
            _electricBtn.SetButtonAction(() =>
            {
                ChooseBtn(BtnType.Electric);
                HouseManager.Instance.ShowObject(ObjectType.Electricity);
            });
            _acBtn.SetButtonAction(() =>
            {
                ChooseBtn(BtnType.AC);
                HouseManager.Instance.ShowObject(ObjectType.AC);
            });
            _concreteBtn.SetButtonAction(() =>
            {
                ChooseBtn(BtnType.Concrete);
                HouseManager.Instance.ShowObject(ObjectType.Concrete);
            });
            _furnitureBtn.SetButtonAction(() =>
            {
                ChooseBtn(BtnType.Furniture);
                HouseManager.Instance.ShowObject(ObjectType.Furniture);
            });
        }

		private void ChooseBtn(BtnType btnType)
		{
            switch (btnType)
            {
                case BtnType.Electric:
                    _electricBtn.SetChosen(true);
					_acBtn.SetChosen(false);
					_concreteBtn.SetChosen(false);
					_furnitureBtn.SetChosen(false);
                    break;
                case BtnType.AC:
                    _electricBtn.SetChosen(false);
					_acBtn.SetChosen(true);
					_concreteBtn.SetChosen(false);
					_furnitureBtn.SetChosen(false);
                    break;
                case BtnType.Concrete:
                    _electricBtn.SetChosen(false);
					_acBtn.SetChosen(false);
					_concreteBtn.SetChosen(true);
					_furnitureBtn.SetChosen(false);
                    break;
                case BtnType.Furniture:
                    _electricBtn.SetChosen(false);
					_acBtn.SetChosen(false);
					_concreteBtn.SetChosen(false);
					_furnitureBtn.SetChosen(true);
                    break;
            }
        }
    }
}
