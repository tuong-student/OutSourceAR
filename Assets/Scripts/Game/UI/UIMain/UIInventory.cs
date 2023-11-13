using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using NOOD.Extension;
using System;
using NOOD.UI;
using Game.UI;

namespace Game
{
    public enum FilterType
    {
        All,
        LivingRoom,
        WorkingArea,
        Kitchen
    }

    public class UIInventory : MonoBehaviour
    {
        #region Event
        public  Action OnBackAction;
        #endregion

        private List<InventoryElement> _inventoryElements = new List<InventoryElement>();
        [SerializeField] private InventoryElement _inventoryElementPref;
        [SerializeField] private GameObject _content;
        [SerializeField] private List<ARObjectSO> _arObjectData = new List<ARObjectSO>();
        [SerializeField] private List<ARObjectSO> _chosenObjects = new List<ARObjectSO>();
        [SerializeField] private Button _backBtn, _addToYourSpaceBtn;

        [SerializeField] private FilterCustomBtn _all, _livingRoom, _workingArea, _kitchen;
        [SerializeField] private Color _normalColor, _chosenColor;

        void Awake()
        {
            _inventoryElementPref.gameObject.SetActive(false);
            DisplayDataList(_arObjectData);
        }

        void Start()
        {
            SetBtn();
            ChooseBtn(FilterType.All);
        }

        private void SetBtn()
        {
            _backBtn.onClick.AddListener(() => OnBackAction?.Invoke());
            _addToYourSpaceBtn.onClick.AddListener(() => 
            {
                UILoader.GetUI<UIMain>().OnInventoryConfirm?.Invoke(_chosenObjects);
            });

            _all._normalColor = _normalColor;
            _livingRoom._normalColor = _normalColor;
            _workingArea._normalColor = _normalColor;
            _kitchen._normalColor = _normalColor;

            _all._choseColor = _chosenColor;
            _livingRoom._choseColor = _chosenColor;
            _workingArea._choseColor = _chosenColor;
            _kitchen._choseColor = _chosenColor;

            _all.AddButtonAction(() => ChooseBtn(FilterType.All));
            _livingRoom.AddButtonAction(() => ChooseBtn(FilterType.LivingRoom));
            _workingArea.AddButtonAction(() => ChooseBtn(FilterType.WorkingArea));
            _kitchen.AddButtonAction(() => ChooseBtn(FilterType.Kitchen));
        }
        private void ChooseBtn(FilterType filterType)
        {
            switch (filterType)
            {
                case FilterType.All:
                    _all.SetChosen(true);
                    _livingRoom.SetChosen(false);
                    _workingArea.SetChosen(false);
                    _kitchen.SetChosen(false);
                    break;
                case FilterType.LivingRoom:
                    _all.SetChosen(false);
                    _livingRoom.SetChosen(true);
                    _workingArea.SetChosen(false);
                    _kitchen.SetChosen(false);
                    break;
                case FilterType.WorkingArea:
                    _all.SetChosen(false);
                    _livingRoom.SetChosen(false);
                    _workingArea.SetChosen(true);
                    _kitchen.SetChosen(false);
                    break;
                case FilterType.Kitchen:
                    _all.SetChosen(false);
                    _livingRoom.SetChosen(false);
                    _workingArea.SetChosen(false);
                    _kitchen.SetChosen(true);
                    break;
            }
            FilterSearch(filterType);
        }
        private void FilterSearch(FilterType filterType)
        {
            switch (filterType)
            {
                case FilterType.All:
                    DisplayDataList(_arObjectData);
                    break;
                case FilterType.LivingRoom:
                    List<ARObjectSO> datasToDisplay = _arObjectData.Where(x => x._objectKind == ObjectKind.LivingRoomSofa).ToList();
                    DisplayDataList(datasToDisplay);
                    break;
                case FilterType.WorkingArea:
                    datasToDisplay = _arObjectData.Where(x => x._objectKind == ObjectKind.WorkingArea).ToList();
                    DisplayDataList(datasToDisplay);
                    break;
                case FilterType.Kitchen:
                    datasToDisplay = _arObjectData.Where(x => x._objectKind == ObjectKind.Kitchen).ToList();
                    DisplayDataList(datasToDisplay);
                    break;
            }
        }

        private void DisplayDataList(List<ARObjectSO> datasToDisplay)
        {
            _inventoryElements.DeActiveAllGameObjectInList();
            for(int i = 0; i < datasToDisplay.Count; i++) 
            {
                ARObjectSO data = datasToDisplay[i];
                if(i < _inventoryElements.Count)
                {
                    // Debug.Log("get old");
                    InventoryElement _inventoryElement;
                    _inventoryElement = _inventoryElements[i];
                    _inventoryElement.gameObject.SetActive(true);
                    _inventoryElement.DisplayData(data);
                }
                else
                {
                    // Debug.Log("get new");
                    CreateNewInventoryElement(data);
                }
            }
        }

        private InventoryElement CreateNewInventoryElement(ARObjectSO data)
        {
            InventoryElement inventoryElement = Instantiate(_inventoryElementPref, _content.transform);
            inventoryElement.gameObject.SetActive(true);
            inventoryElement.DisplayData(data);
            inventoryElement.OnPress += AddToChosenObjectList;
            _inventoryElements.Add(inventoryElement);
            return inventoryElement;
        }

        public void AddToChosenObjectList(ARObjectSO objectSO)
        {
            if(_chosenObjects.Contains(objectSO))
            {
                _chosenObjects.Remove(objectSO);
            }
            else
            {
                // Check if any object have the same type in the list
                if(_chosenObjects.Any(x => x._objectKind == objectSO._objectKind))
                {
                    ARObjectSO objectSameKind = _chosenObjects.First(x => x._objectKind == objectSO._objectKind);
                    _chosenObjects.Remove(objectSameKind);
                }
                _chosenObjects.Add(objectSO);
            }
            UpdateElement();
        }
        private void UpdateElement()
        {
            foreach(var element in _inventoryElements)
            {
                if(_chosenObjects.Contains(element.GetData()))
                {
                    element.Choose();
                }
                else
                {
                    element.UnChose();
                }
            }
        }
    }
}
