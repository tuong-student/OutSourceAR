using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class CustomBtn : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [HideInInspector] public Color _normalColor, _choseColor;
        private Button _btn;
        private Action _action;
        private bool value = false;


        void Awake()
        {
            if(_btn == null)
            {
                _btn = GetComponent<Button>();
            }
            _btn.onClick.AddListener(() => _action?.Invoke());
            _action += SetChosen;
        }

        public virtual void SetChosen()
        {
            value = !value;
            if(value)
            {
                SetIconColor(_choseColor);
            }
            else
            {
                SetIconColor(_normalColor);
            }
        }

        public virtual void SetChosen(bool value)
        {
            if(value)
            {
                SetIconColor(_choseColor);
            }
            else
            {
                SetIconColor(_normalColor);
            }
        }

        public void SetIconColor(Color color)
        {
            _icon.color = color;
        }
        public void AddButtonAction(Action action)
        {
            _action += action;
        }
    }
}
