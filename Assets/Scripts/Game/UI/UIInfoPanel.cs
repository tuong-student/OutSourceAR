using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI; 
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

namespace Game.UI
{
	public class UIInfoPanel : NoodUI 
	{
        [SerializeField] private RectTransform _background;
        [SerializeField] private GameObject _panel;
        [SerializeField] private TextMeshProUGUI _objectName, _objectInfo;

        public static UIInfoPanel Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UIInfoPanel>("Prefab/Game/UI/UIInfoPanel"), parent);
		}

		public void SetInfoAndName(string name, string info)
		{
            _objectName.text = name;
            _objectInfo.text = info;
        }

        public override void Open()
        {
            StartCoroutine(OpenCR());
        }
        public override void Close()
        {
            Debug.Log(name + "Close");
            _panel.transform.DOKill();
            _panel.transform.DOScale(0, 0.2f).OnComplete(() => _panel.SetActive(false));
        }

        private IEnumerator OpenCR()
        {
            yield return null;
            _panel.transform.DOKill();
            Debug.Log(name + "Open");
            _panel.transform.localScale = Vector3.zero;
            _panel.SetActive(true);
            _panel.transform.DOScale(1, 0.2f);
            LayoutRebuilder.ForceRebuildLayoutImmediate(_panel.GetComponent<RectTransform>());
            _panel.GetComponent<RectTransform>().ForceUpdateRectTransforms();
        }
	}
}
