using System;
using System.Collections;
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
        public static Action OnCompleteStage;
        public static NativeGallery.MediaSaveCallback OnSaveSSCallback;
        public AppStage AppStage { get; private set; }
        private bool _isLoaded;
        [SerializeField] private ARPlaneManager _aRPlaneManager;
        private GameObject chosenObject;
        private GameObject _cloneHouse;

        void Awake()
        {
            UILoader.ResetData();
            AppStage = AppStage.Intro;
            OnCompleteStage += NextWindow;
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
                    case ObjectKind.LivingRoomSofa:
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
                    UIMain uiMain = UILoader.LoadUI<UIMain>();
                    uiMain.OnTakeScreenshot += TakeScreenshot;
                    uiMain.OnBackAction += ReturnChoseLayout;
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
            if(_cloneHouse != null)
            {
                _cloneHouse.SetActive(true);
            }
            else
                _cloneHouse = Instantiate(house, position, Quaternion.identity);

        }


        private void NextWindow()
        {
            AppStage++;
            Debug.Log(AppStage.ToString());
            _isLoaded = false;
        }
        private void BackWindow()
        {
            AppStage--;
            _isLoaded = false;
        }
        private void ReturnChoseLayout()
        {
            UILoader.CloseUI<UIMain>();
            _cloneHouse.SetActive(false);
            AppStage = AppStage.ChoosingLayout;
            _isLoaded = false;
        }

        private void TakeScreenshot()
        {
            Texture2D image = null;
            StartCoroutine(TakeScreenShotCR(image));
        }

        IEnumerator TakeScreenShotCR(Texture2D image)
        {
            yield return new WaitForEndOfFrame();
            image = ScreenCapture.CaptureScreenshotAsTexture();
            UILoader.GetUI<UIMain>().OnScreenShotSuccess?.Invoke(image);
            NativeGallery.SaveImageToGallery(image, "ARApp", "ScreenCapture", OnSaveSSCallback);
        }
    }

}
