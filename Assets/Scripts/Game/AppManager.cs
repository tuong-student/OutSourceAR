using System;
using System.Collections.Generic;
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
        [SerializeField] private Transform _kitchenHolder, _livingRoomHolder, _workingAreaHolder;
        [SerializeField] GameObject house;
        public static Action onCompleteStage;
        public static NativeGallery.MediaSaveCallback OnSaveSSCallback;
        public AppStage AppStage { get; private set; }
        private bool _isLoaded;
        [SerializeField] private ARPlaneManager _aRPlaneManager;
        private GameObject chosenObject;

        void Awake()
        {
            UILoader.ResetData();
            AppStage = AppStage.Intro;
            onCompleteStage += NextStage;
        }

        // Update is called once per frame
        void Update()
        {
            if(!_isLoaded) 
                LoadCurrentUI();
        }

        private void ReplaceObject(List<ARObjectSO> aRObjectSOs)
        {
            foreach(var arObject in aRObjectSOs)
            {
                GameObject model = arObject._pref;
                switch (arObject._objectKind)
                {
                    case ObjectKind.House:
                        break;
                    case ObjectKind.LivingRoom:
                        Destroy(_livingRoomHolder.transform.GetChild(0).gameObject);
                        Instantiate(model, _livingRoomHolder);
                        break;
                    case ObjectKind.WorkingArea:
                        Destroy(_workingAreaHolder.transform.GetChild(0).gameObject);
                        Instantiate(model, _workingAreaHolder);
                        break;
                    case ObjectKind.Kitchen:
                        Destroy(_kitchenHolder.transform.GetChild(0).gameObject);
                        Instantiate(model, _kitchenHolder);
                        break;
                }
            }
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
                    NoodyCustomCode.StartDelayFunction(GetTransform, 0.3f);
                    _aRPlaneManager.enabled = false;
                    _isLoaded = true;
                    break;
                case AppStage.Exit:
                    // Player Exit the app, app run off
                    _isLoaded = true;
                    break;
            }
        }

        private void GetTransform()
        {
            _kitchenHolder = HouseManager.Instance._kitchenHolder;
            _livingRoomHolder = HouseManager.Instance._livingRoomHolder;
            _workingAreaHolder = HouseManager.Instance._workingAreaHolder;
        }

        private void LoadHouse()
        {
            UIMain uIMain = UILoader.GetUI<UIMain>();
            Vector3 position = uIMain.GetRaycastPosition();
            position.y += 0.01f;

            Instantiate(house, position, Quaternion.identity);
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
