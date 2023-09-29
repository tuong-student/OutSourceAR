using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI; 

namespace Game
{
	public class UITest24 : NoodUI 
	{
		public static UITest24 Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UITest24>("Prefab/UITest24"), parent);
		}
	}
}
