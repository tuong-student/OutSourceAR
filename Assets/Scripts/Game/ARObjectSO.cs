using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public enum ObjectKind
    {
        House, // This is water pile, electric wire, air...
        LivingRoom,
        WorkingArea,
        Kitchen
    }

    [CreateAssetMenu(fileName = "ARObjectSO")]
    public class ARObjectSO : ScriptableObject
    {
        public ObjectKind _objectKind;
        public GameObject _pref;
        public Sprite _iconSprite;
        public string _name;
        [TextArea(5, 5)]
        public string _description;
    }
}
