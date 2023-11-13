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
        [SerializeField] private GameObject _infoZone;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private List<LayoutElement> _layoutElements = new List<LayoutElement>();
        [SerializeField] private LayoutInfoManager _layoutInfoManager;
        private float _positionX;
        private LayoutElement _chosenLayoutElement;

        public static UIChoseLayout Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UIChoseLayout>("Prefab/Game/UI/UIChoseLayout"), parent);
		}

		void Awake()
		{
            _button.onClick.AddListener(Next);
            _scrollRect.onValueChanged.AddListener(UpdatePositionX);
            _layoutInfoManager ??= GetComponent<LayoutInfoManager>();
            _infoZone.SetActive(false);
        }

        private void UpdatePositionX(Vector2 vector2)
        {
            _positionX = vector2.x;
        }

		public void Next()
		{
            Close();
            AppManager.OnCompleteStage?.Invoke();
        }

		public void SetChosenLayoutElement(LayoutElement layoutElement)
		{
            if(_chosenLayoutElement != null && _chosenLayoutElement == layoutElement)
            {
                // Move to next scene with this layoutElement
                Global.data = layoutElement.GetData();
                Next();
                return;
            }

            _chosenLayoutElement = layoutElement;
            Debug.Log("_chosenLayoutElement null: " + _chosenLayoutElement == null);
            ScrollToElementLoop(_layoutElements.IndexOf(_chosenLayoutElement));
            _layoutInfoManager.UpdateData(layoutElement.GetData());
            _infoZone.SetActive(true);
        }

		public void ScrollToElementLoop(int elementIndex)
		{
            Debug.Log("Scroll To Element Loop");
            float targetPosition = (float)elementIndex / (float)(_layoutElements.Count - 1);
            CoroutineScript coroutineScript = NoodyCustomCode.CreateNewCoroutineObj();
            coroutineScript.StartCoroutineLoop(() =>
            {
                ScrollToPosition(targetPosition, 0.5f);
                if(Mathf.Abs(_scrollRect.horizontalNormalizedPosition - targetPosition) < 0.001f)
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
	}
}
