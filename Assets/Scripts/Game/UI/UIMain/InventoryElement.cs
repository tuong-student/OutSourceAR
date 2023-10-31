using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryElement : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;
    private ARObjectSO data;

    public void SetIcon(Sprite iconSprite)
    {
        icon.sprite = iconSprite;
    }
    public void SetData(ARObjectSO aRObjectSO)
    {
        data = aRObjectSO;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ChooseObject();
    }

    public void ChooseObject()
    {
        AppManager.onChooseObject(data);
    }
}
