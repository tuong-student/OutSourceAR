using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ARObjectSO")]
public class ARObjectSO : ScriptableObject
{
    public GameObject _pref;
    public Sprite _iconSprite;
    public string _name;
    [TextArea(5, 5)]
    public string _description;
}
