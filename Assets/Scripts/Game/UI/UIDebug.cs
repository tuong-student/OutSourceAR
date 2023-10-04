using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI;
using TMPro;

namespace Game.UI
{
	public class UIDebug : NoodUI 
	{
		[SerializeField] private TextMeshProUGUI _debugText;

		public static UIDebug Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UIDebug>("Prefab/Game/UI/UIDebug"), parent);
		}

		public void SetText(string text)
		{
			_debugText.text = text;
		}
	}
}
