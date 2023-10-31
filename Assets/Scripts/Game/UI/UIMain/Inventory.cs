using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventoryElement> _inventoryElements = new List<InventoryElement>();
    [SerializeField] private List<ARObjectSO> _arObjectData = new List<ARObjectSO>();

    void Awake()
    {
        for(int i = 0; i < _arObjectData.Count - 1; i++) 
        {
            ARObjectSO data = _arObjectData[i];
            _inventoryElements[i].SetIcon(data._iconSprite);
        }
    }
}
