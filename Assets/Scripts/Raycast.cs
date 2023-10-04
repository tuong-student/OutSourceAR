using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using Unity.XR.CoreUtils;
using UnityEngine.XR.ARFoundation;
using NOOD;
using NOOD.UI;
using Game.UI;
using NOOD.Extension;
using System.IO;
using NOOD.Data;
using Unity.VisualScripting;

namespace App
{
    public class Raycast : MonoBehaviour
    {
        Camera _arCamera;
        ARRaycastManager _raycastManager;

        bool _isPlaceObj;
        ARObject _arObject;

        UIDebug _uiDebug;
        UISelector _uiSelector;

        [SerializeField] private GameObject _cloneObject;

        void Awake()
        {
            _arCamera = Camera.main;
            _raycastManager = GetComponent<ARRaycastManager>();
        }

        void Start()
        {
            // UILoader.LoadUI<UITest>();
            _uiDebug = UILoader.LoadUI<UIDebug>();
            _uiSelector = UILoader.LoadUI<UISelector>();
            TextAsset textAsset = Resources.Load<TextAsset>("Datas/UIDictionary");
            Debug.Log(textAsset.text);
        }

        void Update()
        {
            if(!_arCamera || !_uiDebug || !_uiSelector) return;
            Ray raycast = _arCamera.ScreenPointToRay(_uiSelector.AimPosition);
            if (Physics.Raycast(raycast, out RaycastHit raycastHit))
            {
                ShowArObjectInfo(raycastHit.collider.gameObject);
            }
            else
            {
                if(_arObject != null)
                {
                    _arObject.ActiveOutline(false);
                    _arObject = null;
                }
            }

            if(Input.touchCount > 0 && _isPlaceObj == false)
            {
                Ray ray = _arCamera.ScreenPointToRay(Input.GetTouch(0).position);
                if(Physics.Raycast(ray, out RaycastHit hit))
                {
                    _isPlaceObj = true;
                    Instantiate(_cloneObject, hit.point, Quaternion.identity);
                }
            }
        }

        private void ShowArObjectInfo(GameObject hitObject)
        {
            if(hitObject != _arObject)
            {
                if (hitObject.GetComponent<ARObject>())
                {
                    if (_arObject != null) 
                    {
                        _arObject.ActiveOutline(false);
                    }
                    _arObject = hitObject.GetComponent<ARObject>();
                    _arObject.ActiveOutline(true);

                }
                else return;
            }
        }
    }
}
