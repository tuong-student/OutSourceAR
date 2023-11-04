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


        void Awake()
        {
            if(_btn == null)
            {
                _btn = GetComponent<Button>();
            }
            _btn.onClick.AddListener(() => _action?.Invoke());
        }

        public virtual void SetChosen(bool value)
        {
            if(value)
            {
                SetIconColor(_normalColor);
            }
            else
            {
                SetIconColor(_choseColor);
            }
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
