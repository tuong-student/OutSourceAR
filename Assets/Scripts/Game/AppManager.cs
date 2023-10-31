using System;
using System.IO;
using Game.UI;
using NOOD;
using NOOD.UI;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

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
        public static Action<ARObjectSO> onChooseObject;
        public static NativeGallery.MediaSaveCallback OnSaveSSCallback;
        public AppStage AppStage { get; private set; }
        private bool _isLoaded;
        [SerializeField] private ARPlaneManager _aRPlaneManager;
        private GameObject chosenObject;

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
                    UILoader.LoadUI<UIMain>().OnTakeScreenshot += TakeScreenshot;
                    NoodyCustomCode.StartDelayFunction(LoadHouse, 0.2f);
                    _aRPlaneManager.enabled = false;
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
            position.y += 0.01f;

            Instantiate(house, position, Quaternion.identity);
        }

        public void ChoseObject(ARObjectSO aRObjectSO)
        {
            Debug.Log(aRObjectSO._name);
            chosenObject = aRObjectSO._pref;
        }

        private void NextStage()
        {
            AppStage++;
            Debug.Log(AppStage.ToString());
            _isLoaded = false;
        }

        private Texture2D TakeScreenshot()
        {
            Texture2D image = ScreenCapture.CaptureScreenshotAsTexture();
            NativeGallery.SaveImageToGallery(image, "ARApp", "ScreenCapture", OnSaveSSCallback);
            return image;
        }
    }

}
