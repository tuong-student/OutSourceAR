using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class FurnitureManager : MonoBehaviour
    {
        [SerializeField] private List<ARObjectSO> _chosenObject = new List<ARObjectSO>();

        public void ShowObject()
        {
            foreach(var aRObject in _chosenObject)
            {
                
            }
        }
    }
}
