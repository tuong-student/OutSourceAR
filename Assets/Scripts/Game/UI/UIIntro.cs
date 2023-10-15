using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI;
using TMPro;
using UnityEngine.UI;

namespace Game.UI
{
	public class UIIntro : NoodUI 
	{
        [SerializeField] private Button _button;

        public static UIIntro Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UIIntro>("Prefab/Game/UI/UIIntro"), parent);
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
