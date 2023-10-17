using System;
using System.Collections;
using System.Collections.Generic;
using Game.UI;
using NOOD.UI;
using UnityEngine;

namespace Game
{
    public enum AppStage
    {
        Intro,
        ChoosingLayout,
        Tutor, // Guild player to place a small piece of house, this will be the center of the house
        ChoosingRoom, // Player choose the room to be show
        Showing,
        Exit
    }

    public class AppManager : MonoBehaviour
    {
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
            if(_isLoaded) return;
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
                case AppStage.ChoosingRoom:
                    // Show Choosing Room UI
                    UILoader.LoadUI<UIMain>();

                    _isLoaded = true;
                    break;
                case AppStage.Showing:
                    // Main active of the app
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
            
        }

        private void NextStage()
        {
            AppStage++;
            _isLoaded = false;
        }
    }

}
