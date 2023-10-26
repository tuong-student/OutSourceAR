using System;
using Game.UI;
using NOOD;
using NOOD.UI;
using UnityEngine;

namespace Game
{
    public enum AppStage
    {
        Intro,
        ChoosingLayout,
        Tutor, // Guild player to place a small piece of house, this will be the center of the house
        Showing,
        Exit
    }

    public class AppManager : MonoBehaviour
    {
        [SerializeField] GameObject house;
        public static Action onCompleteStage;
        public AppStage AppStage { get; private set; }
        private bool _isLoaded;

        void Awake()
        {
            AppStage = AppStage.Intro;
            onCompleteStage += NextStage;
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if(!_isLoaded) 
                LoadCurrentUI();
        }

        private void LoadCurrentUI()
        {
            switch (AppStage)
            {
                case AppStage.Intro:
                    // Player enter the app, show intro 
                    UILoader.LoadUI<UIIntro>();
                    _isLoaded = true;
                    break;
                case AppStage.ChoosingLayout:
                    // Show Choosing layout UI
                    UILoader.LoadUI<UIChoseLayout>();
                    _isLoaded = true;
                    break;
                case AppStage.Tutor:
                    // Show small tutorial gif or video 
                    UILoader.LoadUI<UITutor>();
                    _isLoaded = true;
                    break;
                case AppStage.Showing:
                    // Main active of the app
                    UILoader.LoadUI<UIMain>();
                    NoodyCustomCode.StartDelayFunction(LoadHouse, 0.2f);
                    _isLoaded = true;
                    break;
                case AppStage.Exit:
                    // Player Exit the app, app run off
                    _isLoaded = true;
                    break;
            }
        }

        private void LoadHouse()
        {
            UIMain uIMain = UILoader.GetUI<UIMain>();
            Vector3 position = uIMain.GetRaycastPosition();

            Instantiate(house, position, Quaternion.identity);
        }

        private void NextStage()
        {
            AppStage++;
            Debug.Log(AppStage.ToString());
            _isLoaded = false;
        }
    }

}
