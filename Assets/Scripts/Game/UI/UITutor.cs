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
	public class UITutor : NoodUI 
	{
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _notifyGameObject;

        public static UITutor Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UITutor>("Prefab/Game/UI/UITutor"), parent);
		}
		
		void Awake()
		{
            _button.onClick.AddListener(Next);
        }

        void Start()
        {
            UILoader.LoadUI<UIFooterPopup>().SetText("Finding Ground...");
        }

        void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Vector2.zero);
            if(Physics.Raycast(ray))
            {
                UILoader.LoadUI<UIFooterPopup>().SetText("Ground found, press screen to place room");
                Next();
            }
        }

		public void Next()
		{
            Close();
            AppManager.onCompleteStage?.Invoke();
        }

	}
}
