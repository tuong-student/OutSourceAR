using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class CustomBtn : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _btn;
        private Action _action;

        public Color _normalColor, _choseColor;

        void Awake()
        {
            _btn.onClick.AddListener(() => _action?.Invoke());
        }

        public void SetChosen(bool value)
        {
            if(value)
            {
                SetIconColor(_normalColor);
                SetBackGroundColor(_choseColor);
            }
            else
            {
                SetIconColor(_choseColor);
                SetBackGroundColor(_normalColor);
            }
        }

        public void SetBackGroundColor(Color color)
        {
            _background.color = color;
        }
        public void SetIconColor(Color color)
        {
            _icon.color = color;
        }
        public void SetButtonAction(Action action)
        {
            _action = action;
        }
    }
}
