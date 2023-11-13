using System.Collections;
using System.Collections.Generic;
using Game.UI;
using NOOD;
using NOOD.UI;
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
        [SerializeField] private List<MeshRenderer> _walls = new List<MeshRenderer>();
        [SerializeField] private Material _normalHouseMaterial, _transparentHouseMaterial;
        [SerializeField] public Transform _kitchenHolder, _livingRoomHolder, _workingAreaHolder;
        [SerializeField] private GameObject[] _electricityAndWater;
        [SerializeField] private GameObject[] _concrete;
        [SerializeField] private GameObject[] _airPipe;
        [SerializeField] private GameObject[] _furniture;

        private bool _showElectricity, _showConcrete, _showAirPipe, _showFurniture;

        void Start()
        {
            DeactivateAllObjects();
            UILoader.GetUI<UIMain>().OnInventoryConfirm += (list) =>
            {
                DeactivateAllObjects();
                ChangeShowObject(ObjectType.Furniture);
            };
            NormalHouse();
        }

        private void TransparentHouse()
        {
            foreach(var wall in _walls)
            {
                wall.material = _transparentHouseMaterial;
            }
        }
        private void NormalHouse()
        {
            foreach(var wall in _walls)
            {
                wall.material = _normalHouseMaterial;
            }
        }

        public void ChangeShowObject(ObjectType objectType)
        {
            switch (objectType)
            {
                case ObjectType.Electricity:
                    _showElectricity = !_showElectricity;
                    break;
                case ObjectType.AC:
                    _showAirPipe = !_showAirPipe;
                    break;
                case ObjectType.Concrete:
                    _showConcrete = !_showConcrete;
                    break;
                case ObjectType.Furniture:
                    _showFurniture = !_showFurniture;
                    break;
            }
            UpdateShowObject();
        }

        private void UpdateShowObject()
        {
            
            foreach(var item in _electricityAndWater)
            {
                item.SetActive(_showElectricity);
            }
            foreach(var item in _airPipe)
            {
                item.SetActive(_showAirPipe);
            }
            foreach(var item in _concrete)
            {
                item.SetActive(_showConcrete);
            }
            foreach(var item in _furniture)
            {
                item.SetActive(_showFurniture);
            }
            if(_showAirPipe || _showElectricity || _showConcrete)
            {
                TransparentHouse();
            }
            else
            {
                NormalHouse(); 
            }
        }

        public void DeactivateAllObjects()
        {
            foreach(var item in _electricityAndWater)
            {
                if(item.activeInHierarchy == false) continue;
                item.SetActive(false);
            }
            foreach(var item in _concrete)
            {
                if(item.activeInHierarchy == false) continue;
                item.SetActive(false);
            }
            foreach(var item in _airPipe)
            {
                if(item.activeInHierarchy == false) continue;
                item.SetActive(false);
            }
            foreach(var item in _furniture)
            {
                if(item.activeInHierarchy == false) continue;
                item.SetActive(false);
            }
            _showElectricity = false;
            _showAirPipe = false;
            _showConcrete = false;
            _showFurniture = false;
            UpdateShowObject();
        }
    }
}
