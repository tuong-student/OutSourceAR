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
        [SerializeField] private TextMeshProUGUI _notifyText;

        [SerializeField] private RectTransform showTrans, hideTrans;

        public static UITutor Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UITutor>("Prefab/Game/UI/UITutor"), parent);
		}
		
		void Awake()
		{
            _button.onClick.AddListener(Next);
        }

	 	IEnumerator Start()
		{
            yield return new WaitForSeconds(1f);
            ShowNotify();
        }

		public void Next()
		{
            Close();
            AppManager.onCompleteStage?.Invoke();
        }

		public void ShowNotify()
		{
            _notifyGameObject.transform.DOMove(showTrans.position, 1f).SetEase(Ease.OutFlash);
        }
		public void HideNotify()
		{
            _notifyGameObject.transform.DOMove(hideTrans.position, 1f).SetEase(Ease.InFlash);
        }
	}
}
