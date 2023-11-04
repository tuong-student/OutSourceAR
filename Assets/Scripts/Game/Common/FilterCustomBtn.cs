using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game
{
    public class FilterCustomBtn : CustomBtn
    {
        [SerializeField] private TextMeshProUGUI _filterName;
        public override void SetChosen(bool value)
        {
            // Set on UI
            if(value)
                _filterName.color = _choseColor;
            else
                _filterName.color = _normalColor;
        }
    }
}
