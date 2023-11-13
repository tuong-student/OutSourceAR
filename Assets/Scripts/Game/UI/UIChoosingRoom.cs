using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI; 
using UnityEngine.UI;

namespace Game.UI
{
	public class UIChoosingRoom : NoodUI 
	{
        [SerializeField] private Button _button;
		public static UIChoosingRoom Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UIChoosingRoom>("Prefab/Game/UI/UIChoosingRoom"), parent);
		}
		
		void Awake()
		{
            _button.onClick.AddListener(Next);
        }

		public void Next()
		{
            Close();
            AppManager.OnCompleteStage?.Invoke();
        }
	}
}
