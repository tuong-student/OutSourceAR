using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI;
using UnityEngine.UI;
using System.Linq.Expressions;

namespace Game.UI
{
	public class UIChoseLayout : NoodUI 
	{
        [SerializeField] private Button _button;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private List<LayoutElement> _layoutElements = new List<LayoutElement>();
        private float _positionX;
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
            _positionX = _scrollRect.horizontalNormalizedPosition;
        }

		public void Next()
		{
            Close();
            AppManager.onCompleteStage?.Invoke();
        }

		public void SetChosenLayoutElement(LayoutElement layoutElement)
		{
            Debug.Log("Choose Element " + layoutElement.name);
            _chosenLayoutElement = layoutElement;
            ScrollToElementLoop(_layoutElements.IndexOf(_chosenLayoutElement));
        }

		public void ScrollToElementLoop(int elementIndex)
		{
            float targetPosition = (float)elementIndex / (float)(_layoutElements.Count - 1);
            CoroutineScript coroutineScript = NoodyCustomCode.CreateNewCoroutineObj();
            coroutineScript.StartCoroutineLoop(() =>
            {
                ScrollToPosition(targetPosition, 0.5f);
                if(Mathf.Abs(_scrollRect.horizontalNormalizedPosition - targetPosition) < 0.03f)
                {
                    coroutineScript.Complete();
                }
            }, 0.2f);
        }

        public void ScrollToPosition(float position, float speed = 1)
        {
            if(position > _scrollRect.horizontalNormalizedPosition)
            {
                _positionX = Mathf.Clamp(_positionX + Time.deltaTime * speed, 0, position);
            }
            else if(position < _scrollRect.horizontalNormalizedPosition)
            {
                _positionX = Mathf.Clamp(_positionX - Time.deltaTime * speed, position, 1);
            }
            _scrollRect.horizontalNormalizedPosition = _positionX;
        }

        // public void ScrollToElement(int elementIndex)
        // {
        //     Debug.Log("Index " + elementIndex);
        //     float elementPositionX = (float)elementIndex / (float)(_layoutElements.Count - 1);
        //     Debug.Log("elementPositionX " + elementPositionX);
        //     ScrollToPosition(elementPositionX);
        // }
	}
}
