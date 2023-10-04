using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App
{
    [RequireComponent(typeof(Outline))]
    public class ARObject : MonoBehaviour
    {
        Outline _outline;

        void Awake()
        {
            _outline = GetComponent<Outline>();
            _outline.enabled = false;
        }

        public void ActiveOutline(bool value)
        {
            _outline.enabled = value;
        }

        public string GetInfo()
        {
            return name;
        }
    }
}
