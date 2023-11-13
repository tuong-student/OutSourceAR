using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI;
using UnityEngine.UI;

namespace Game.UI
{
	public class UILoading : NoodUI 
	{
        [SerializeField] private Slider _loadingSlider;
        [SerializeField] private Action OnComplete;
        private float value;
        private bool isComplete;

        public static UILoading Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UILoading>("Prefab/Game/UI/UILoading"), parent);
		}

		void OnEnable()
		{
            Debug.Log("LoadUILoading");
            value = 0;
            _loadingSlider.maxValue = 10;
            isComplete = false;
        }

		void Update()
		{
            if(isComplete) return;
            value += 4 * Time.deltaTime;
            _loadingSlider.value = value;
			if(_loadingSlider.value == _loadingSlider.maxValue)
			{
                isComplete = true;
                OnComplete?.Invoke();
                Close();
            }
        }

        public void SetOnCompleteAction(Action action)
        {
            OnComplete = action;
        }
	}
}
