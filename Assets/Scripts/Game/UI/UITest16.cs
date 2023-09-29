using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI; 

namespace Game.UI
{
	public class UITest16 : NoodUI 
	{
		public static UITest16 Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UITest16>("Prefab/Game/UI/UITest16"), parent);
		}
	}
}
