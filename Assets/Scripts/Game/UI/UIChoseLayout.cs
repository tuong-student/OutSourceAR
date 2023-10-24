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
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] float _positionX;
        private LayoutElement _chosenLayoutElement;

        public static UIChoseLayout Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UIChoseLayout>("Prefab/Game/UI/UIChoseLayout"), parent);
		}

		void Awake()
		{
            _button.onClick.AddListener(Next);
        }

		void Update()
		{
            _scrollRect.horizontalNormalizedPosition = _positionX;
        }

		public void Next()
		{
            Close();
            AppManager.onCompleteStage?.Invoke();
        }

		public void SetChosenLayoutElement(LayoutElement layoutElement)
		{
            _chosenLayoutElement = layoutElement;
            ScrollToElement();
        }

		public void ScrollToElement()
		{
            CoroutineScript coroutineScript = NoodyCustomCode.CreateNewCoroutineObj();
            coroutineScript.StartCoroutineLoop(() =>
            {
				if(_chosenLayoutElement.ScreenPosition.x > 0.3f)
				{
                    ScrollToLeft(0.1f);
                }
				else if(_chosenLayoutElement.ScreenPosition.x < -0.3f)
				{
                    ScrollToRight(0.1f);
				}
				else
				{
                    coroutineScript.Complete();
                }
            }, Time.deltaTime);
        }

		public void ScrollToLeft(float speed = 1)
		{
            _positionX += Time.deltaTime * speed;
        }
		public void ScrollToRight(float speed = 1)
		{
            _positionX -= Time.deltaTime * speed;
        }
	}
}
