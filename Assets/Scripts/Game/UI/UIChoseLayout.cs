using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI;
using UnityEngine.UI;

namespace Game.UI
{
	public class UIChoseLayout : NoodUI 
	{
        [SerializeField] private Button _button;

        public static UIChoseLayout Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UIChoseLayout>("Prefab/Game/UI/UIChoseLayout"), parent);
		}

		void Awake()
		{
            _button.onClick.AddListener(Next);
        }

		public void Next()
		{
            Close();
            AppManager.onCompleteStage?.Invoke();
        }
	}
}
