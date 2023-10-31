using System.Collections;
using System.Collections.Generic;
using Game.UI;
using NOOD.UI;
using UnityEngine;

namespace App
{
    [RequireComponent(typeof(Outline))]
    public class ARObject : MonoBehaviour
    {
        Outline _outline;
        [SerializeField] private ARObjectSO _data;

        void Awake()
        {
            _outline = GetComponent<Outline>();
            _outline.enabled = false;
        }

        public void ActiveOutlineAndShowInfo(bool value)
        {
            if(value)
            {
                _outline.enabled = value;
                if(_data == null)
                {
                    UILoader.LoadUI<UIInfoPanel>().SetInfoAndName(_data._name, _data._description);
                }
            }
            else
            {
                _outline.enabled = false;
                UILoader.CloseUI<UIInfoPanel>();
            }
        }

        public string GetInfo()
        {
            return name;
        }
    }
}
