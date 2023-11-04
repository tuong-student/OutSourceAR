using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LayoutELementSO")]
public class LayoutELementSO : ScriptableObject
{
    public string _name;
    public string _size;
    public string _description;
    public Sprite _fistIcon;
    public string _firstText;
    public int _livingArea;
    public int _toiletNumber;
}