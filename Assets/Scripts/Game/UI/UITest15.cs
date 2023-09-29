using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI; 

namespace Game.UI
{
	public class UITest15 : NoodUI 
	{
		public static UITest15 Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UITest15>("Prefab/Game/UI/UITest15"), parent);
		}
	}
}
