using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using NOOD.Extension;
using System;

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
        public static Action OnConfirmAction;
        #endregion

        private List<InventoryElement> _inventoryElements = new List<InventoryElement>();
        [SerializeField] private InventoryElement _inventoryElementPref;
        [SerializeField] private GameObject _content;
        [SerializeField] private List<ARObjectSO> _arObjectData = new List<ARObjectSO>();
        [SerializeField] private List<ARObjectSO> _chosenObject = new List<ARObjectSO>();
        [SerializeField] private Button _addToYourSpaceBtn;

        [SerializeField] private FilterCustomBtn _all, _livingRoom, _workingArea, _kitchen;
        [SerializeField] private Color _normalColor, _chosenColor;

        void Awake()
        {
            _inventoryElementPref.gameObject.SetActive(false);
        }

        void Start()
        {
            SetBtn();
            ChooseBtn(FilterType.All);
        }

        private void SetBtn()
        {
            _all._normalColor = _normalColor;
            _livingRoom._normalColor = _normalColor;
            _workingArea._normalColor = _normalColor;
            _kitchen._normalColor = _normalColor;

            _all._choseColor = _chosenColor;
            _livingRoom._choseColor = _chosenColor;
            _workingArea._choseColor = _chosenColor;
            _kitchen._choseColor = _chosenColor;

            _all.SetButtonAction(() => ChooseBtn(FilterType.All));
            _livingRoom.SetButtonAction(() => ChooseBtn(FilterType.LivingRoom));
            _workingArea.SetButtonAction(() => ChooseBtn(FilterType.WorkingArea));
            _kitchen.SetButtonAction(() => ChooseBtn(FilterType.Kitchen));
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
        public void FilterSearch(FilterType filterType)
        {
            switch (filterType)
            {
                case FilterType.All:
                    DisplayDataList(_arObjectData);
                    break;
                case FilterType.LivingRoom:
                    List<ARObjectSO> datasToDisplay = _arObjectData.Where(x => x._objectKind == ObjectKind.LivingRoom).ToList();
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
                InventoryElement _inventoryElement;
                if(i < _inventoryElements.Count)
                {
                    _inventoryElement = _inventoryElements[i];
                    _inventoryElement.gameObject.SetActive(true);
                    _inventoryElement.DisplayData(data);
                }
                else
                {
                    CreateNewInventoryElement(data);
                }
            }
        }

        private InventoryElement CreateNewInventoryElement(ARObjectSO data)
        {
            InventoryElement _inventoryElement = Instantiate(_inventoryElementPref, _content.transform);
            _inventoryElement.gameObject.SetActive(true);
            _inventoryElement.DisplayData(data);
            _inventoryElement.OnPress += AddToChosenObjectList;
            return _inventoryElement;
        }

        public void AddToChosenObjectList(ARObjectSO objectSO)
        {
            if(_chosenObject.Contains(objectSO))
            {
                _chosenObject.Remove(objectSO);
            }
            else
            {
                _chosenObject.Add(objectSO);
            }
            
        }
    }
}
