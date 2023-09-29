using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using Unity.XR.CoreUtils;
using UnityEngine.XR.ARFoundation;
using NOOD;
using NOOD.UI;
using Game.UI;

namespace App
{
    public class Raycast : MonoBehaviour
    {
        Camera _arCamera;
        ARRaycastManager _raycastManager;

        float _rayDistanceFromCamera;

        UIDebug _uiDebug;

        void Awake()
        {
            _arCamera = Camera.main;
            _raycastManager = GetComponent<ARRaycastManager>();
        }

        void Start()
        {
            UILoader.LoadUI<UIDebug>();
            // _uiDebug = UILoader.LoadUI<UIDebug>();
            // _uiObjectSelector = UILoader.LoadUI<UIObjectSelector>();
        }

        void Update()
        {
            // if(!_arCamera || !_uiDebug || !_uiObjectSelector) return;
            // Ray raycast = _arCamera.ScreenPointToRay(_uiObjectSelector.targetPosition);
            // RaycastHit raycastHit;
            // if(Physics.Raycast(raycast, out raycastHit, _rayDistanceFromCamera))
            // {
            //     _uiDebug.Display(raycastHit.collider.name);
            // }
        }
    }
}
