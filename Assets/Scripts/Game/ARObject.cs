using System.Collections;
using System.Collections.Generic;
using Game.UI;
using NOOD.UI;
using UnityEngine;

namespace Game 
{
    [RequireComponent(typeof(Outline))]
    public class ARObject : MonoBehaviour
    {
        Outline _outline;
        [SerializeField] private ARObjectSO _data;
        private bool _isShowInfo = false;

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
                if(_data != null && _isShowInfo == false)
                {
                    UILoader.LoadUI<UIInfoPanel>().SetInfoAndName(_data._name, _data._description);
                    _isShowInfo = true;
                }
            }
            else
            {
                _outline.enabled = false;
                UILoader.CloseUI<UIInfoPanel>();
                _isShowInfo = false;
            }
        }

        public string GetInfo()
        {
            return name;
        }

        public ARObjectSO GetData()
        {
            return _data;
        }
    }
}
