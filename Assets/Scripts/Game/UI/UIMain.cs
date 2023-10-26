using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI;
using UnityEngine.UI;
using App;
using TMPro;

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

        private UIDebug _uiDebug;
        private UISelector _uiSelector;

        private ARObject _previousObj;

        public static UIMain Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UIMain>("Prefab/Game/UI/UIMain"), parent);
		}

		void Awake()
		{
            // Read data from Global.data to Instantiate room
            

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
                    aRObject.ActiveOutline(true);

                    if(_previousObj == null)
                    {
                        _previousObj = aRObject;
                    }
                    else
                    {
                        if(_previousObj != aRObject)
                        {
                            _previousObj.ActiveOutline(false);
                            _previousObj = aRObject;
                        }
                    }
                }
                else
                {
                    if (!_previousObj)
                        return;
                    _previousObj.ActiveOutline(false);
                }
            }
            else
            {
                if (!_previousObj)
                    return;
                _previousObj.ActiveOutline(false);
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
