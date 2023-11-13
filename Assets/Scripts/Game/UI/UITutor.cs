using UnityEngine;
using NOOD;
using NOOD.UI;
using UnityEngine.UI;
using TMPro;

namespace Game.UI
{
    public class UITutor : NoodUI
    {
        [SerializeField] private Button _plusBtn;
        [SerializeField] private Image _tutorImage;
        private bool _isHit;
        private bool _isShowTutor;

        public static UITutor Create(Transform parent = null)
        {
            return Instantiate(Resources.Load<UITutor>("Prefab/Game/UI/UITutor"), parent);
        }

        private void Awake()
        {
            _plusBtn.onClick.AddListener(PressBtn);
        }

        void OnEnable()
        {
            ShowTutorAndText();
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Vector2.zero);
#if UNITY_EDITOR
            _isHit = true;
#else
            _isHit = Physics.Raycast(ray);
#endif

            if(_isHit == false && !_isShowTutor)
            {
                ShowTutorAndText();
            }
            if(_isHit && !_isShowTutor)
            {
                OffTutor();
            }
        }

        public void Next()
        {
            AppManager.OnCompleteStage?.Invoke();
        }

        public void PressBtn()
        {
            if (_isHit)
            {
                UILoader.CloseUI<UIFooterPopup>();
                Close();
                Next();
                UILoader.LoadUI<UILoading>().SetOnCompleteAction(null);
            }
            else
            {
                NoodyCustomCode.FadeInImage(_tutorImage);
                UILoader.LoadUI<UIFooterPopup>().SetText("No ground found");
            }
        }

        private void ShowTutorAndText()
        {
            _isShowTutor = true;
            UILoader.LoadUI<UIFooterPopup>().SetText("Finding Ground...");
            NoodyCustomCode.FadeInImage(_tutorImage);
        }
        private void OffTutor()
        {
            _isShowTutor = true;
            NoodyCustomCode.FadeOutImage(_tutorImage);
            UILoader.LoadUI<UIFooterPopup>().SetText("Ground found");
        }

    }
}
