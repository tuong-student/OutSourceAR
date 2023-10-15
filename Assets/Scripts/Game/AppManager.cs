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
            switch (AppStage)
            {
                case AppStage.Intro:
                    // Player enter the app, show intro 
                    UILoader.LoadUI<UIIntro>();
                    break;
                case AppStage.ChoosingLayout:
                    // Show Choosing layout UI
                    UILoader.LoadUI<UIChoseLayout>();
                    break;
                case AppStage.Tutor:
                    // Show small tutorial gif or video 
                    UILoader.LoadUI<UITutor>();
                    break;
                case AppStage.ChoosingRoom:
                    // Show Choosing Room UI
                    UILoader.LoadUI<UIMain>();
                    break;
                case AppStage.Showing:
                    // Main active of the app
                    break;
                case AppStage.Exit:
                    // Player Exit the app, app run off
                    break;
            }
        }

        private void NextStage()
        {
            AppStage++;
        }
    }

}
