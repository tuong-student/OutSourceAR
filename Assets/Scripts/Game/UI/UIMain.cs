using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI;
using UnityEngine.UI;

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

        public static UIMain Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UIMain>("Prefab/Game/UI/UIMain"), parent);
		}

		void Awake()
		{
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
            _electricBtn.SetChosen(false);
            _acBtn.SetChosen(false);
            _concreteBtn.SetChosen(false);
            _furnitureBtn.SetChosen(false);

            SetBtnAction();
        }

        private void SetBtnAction()
        {
            _electricBtn.SetButtonAction(() =>
            {
                ChooseBtn(BtnType.Electric);
            });
            _acBtn.SetButtonAction(() =>
            {
                ChooseBtn(BtnType.AC);
            });
            _concreteBtn.SetButtonAction(() =>
            {
                ChooseBtn(BtnType.Concrete);
            });
            _furnitureBtn.SetButtonAction(() =>
            {
                ChooseBtn(BtnType.Furniture);
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
