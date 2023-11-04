using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{

    public class InventoryElement : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _picture;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Image _stroke;
        [SerializeField] private Color _chosenColor;
        private ARObjectSO data;
        public Action<ARObjectSO> OnPress;
        private bool _isChosen;
        
        void Awake()
        {
            _isChosen = false;
        }

        public void SetIcon(Sprite iconSprite)
        {
            _picture.sprite = iconSprite;
        }
        public void SetName(string name)
        {
            _name.text = data._name;
        }
        public void DisplayData(ARObjectSO aRObjectSO)
        {
            data = aRObjectSO;
            SetIcon(aRObjectSO._iconSprite);
            SetName(aRObjectSO._name);
        }
        
        public ARObjectSO GetData()
        {
            return data;
        }

        public void Choose()
        {
            _isChosen = true;
            _stroke.gameObject.SetActive(true);
            _name.color = _chosenColor;
        }
        public void UnChose()
        {
            _isChosen = false;
            _stroke.gameObject.SetActive(false);
            _name.color = Color.black;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnPress?.Invoke(data);
        }
    }
}
