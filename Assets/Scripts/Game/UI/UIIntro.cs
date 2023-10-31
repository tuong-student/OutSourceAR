using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

namespace Game.UI
{
	public class UIIntro : NoodUI 
	{
        [SerializeField] private GameObject _intro1, _intro2, _intro3, _hidePosition;
        [SerializeField] private Button _intro2BackBtn, _intro2NextBtn, _intro3GetStartBtn;
        private byte introCount = 1;

		#region Intro1
        [SerializeField] private Image _backGround, _appName, _logo;
        [SerializeField] private TextMeshProUGUI _text1, _text2;
		#endregion

        public static UIIntro Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UIIntro>("Prefab/Game/UI/UIIntro"), parent);
		}

		void Awake()
		{
            _intro3GetStartBtn.onClick.AddListener(Next);
            _intro2NextBtn.onClick.AddListener(NextIntro);
            ShowIntro1();
        }

		private void ShowIntro1()
		{
            _intro1.SetActive(true);
            _intro1.transform.DOScale(1.5f, 3);
            NoodyCustomCode.StartDelayFunction(NextIntro, 1);
        }

		private void NextIntro()
		{
            introCount += 1;
            switch(introCount)
			{
				case 1:
                    _intro1.SetActive(true);
                    _intro2.SetActive(false);
                    _intro2.SetActive(false);
                    break;
				case 2:
                    NoodyCustomCode.FadeOutImage(_backGround);
                    NoodyCustomCode.FadeOutImage(_appName);
                    NoodyCustomCode.FadeOutImage(_logo);

                    NoodyCustomCode.FadeOutTextMeshUGUI(_text1);
                    NoodyCustomCode.FadeOutTextMeshUGUI(_text2);
                    _intro2.SetActive(true);
                    break;
				case 3:
                    _intro3.SetActive(true);
                    _intro3.transform.position = _hidePosition.transform.position;
                    _intro3.transform.DOMove(_intro2.transform.position, 0.3f).OnComplete(() => _intro2.SetActive(false));
                    break;
            }
		}

		public void Next()
		{
            Close();
            AppManager.onCompleteStage?.Invoke();
        }
	}
}
