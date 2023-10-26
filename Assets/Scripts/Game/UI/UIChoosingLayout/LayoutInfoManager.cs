using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LayoutInfoManager : MonoBehaviour
{
    private LayoutELementSO data;
    [SerializeField] private TextMeshProUGUI _layoutName;
    [SerializeField] private TextMeshProUGUI _size;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _beds, _livingArea, _toilets;

    public void UpdateData(LayoutELementSO data)
    {
        Debug.Log("Update Data");
        this.data = data;
        _layoutName.text = data._name;
        _size.text = data._size;
        _description.text = data._description;
        _beds.text = data._bedNumber.ToString() + " Beds";
        _livingArea.text = "Living area: " + data._livingArea.ToString() + "m2";
        _toilets.text = data._toiletNumber.ToString() + " Toilets";
    }
}
