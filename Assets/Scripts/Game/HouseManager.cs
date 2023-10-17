using System.Collections;
using System.Collections.Generic;
using NOOD;
using UnityEngine;

namespace Game
{
    public enum ObjectType
    {
        Electricity,
        AC,
        Concrete,
        Furniture
    }

    public class HouseManager : MonoBehaviorInstance<HouseManager>
    {
        [SerializeField] private GameObject[] _electricityAndWater;
        [SerializeField] private GameObject[] _concrete;
        [SerializeField] private GameObject[] _airPipe;
        [SerializeField] private GameObject[] _furniture;

        public void ShowObject(ObjectType objectType)
        {
            switch (objectType)
            {
                case ObjectType.Electricity:
                    foreach(var item in _electricityAndWater)
                    {
                        item.SetActive(true);
                    }
                    break;
                case ObjectType.AC:
                    foreach(var item in _concrete)
                    {
                        item.SetActive(true);
                    }
                    break;
                case ObjectType.Concrete:
                    foreach(var item in _airPipe)
                    {
                        item.SetActive(true);
                    }
                    break;
                case ObjectType.Furniture:
                    foreach(var item in _furniture)
                    {
                        item.SetActive(true);
                    }
                    break;
            }
        }
    }
}
