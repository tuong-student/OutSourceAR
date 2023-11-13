using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.UI;
using NOOD.UI;
using UnityEngine;

namespace Game
{
    public class FurnitureManager : MonoBehaviour
    {
        [SerializeField] private Transform _livingRoomSofaHolder, _livingRoomTVHolder, _workingAreaHolder, _kitchenHolder, _kitchenSideTableHolder, _kitchenDiningTableHolder;

        private List<ARObject> _currentFurniture = new List<ARObject>();

        void Awake()
        {
            UILoader.GetUI<UIMain>().OnInventoryConfirm += (arData) => 
            {
                UILoader.LoadUI<UILoading>().SetOnCompleteAction(null);
                ShowObject(arData);
            };
        }

        public void ShowObject(List<ARObjectSO> chosenObjects)
        {
            foreach(var aRObjectSO in chosenObjects)
            {
                if(_currentFurniture.Count > 0)
                {
                    List<ARObjectSO> currentFurnitureData = _currentFurniture.Select(x => x.GetData()).ToList();
                    bool isContain = currentFurnitureData != null && currentFurnitureData.Contains(aRObjectSO);

                    if(isContain)
                    {
                        continue;
                    }
                }
                
                GameObject arGameObject = Instantiate(aRObjectSO._pref, null);
                Transform parentTransform = null;
                switch (aRObjectSO._objectKind)
                {
                    case ObjectKind.House:
                        // Skip
                        break;
                    case ObjectKind.LivingRoomSofa:
                        parentTransform = _livingRoomSofaHolder;
                        break;
                    case ObjectKind.LivingRoomTV:
                        parentTransform = _livingRoomTVHolder;
                        break;
                    case ObjectKind.WorkingArea:
                        parentTransform = _workingAreaHolder;
                        break;
                    case ObjectKind.Kitchen:
                        parentTransform = _kitchenHolder;
                        break;
                    case ObjectKind.KitchenSideTable:
                        parentTransform = _kitchenSideTableHolder;
                        break;
                    case ObjectKind.KitchenDiningTable:
                        parentTransform = _kitchenDiningTableHolder;
                        break;
                }
                _currentFurniture.Remove(parentTransform.GetChild(0).GetComponent<ARObject>());
                Destroy(parentTransform.GetChild(0).gameObject);
                arGameObject.transform.parent = parentTransform;
                arGameObject.transform.localPosition = Vector3.zero;
                _currentFurniture.Add(arGameObject.GetComponent<ARObject>());
            }
        }

        public void HideObject()
        {
            foreach(var obj in _currentFurniture)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }
}
