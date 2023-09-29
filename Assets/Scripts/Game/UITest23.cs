using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI; 

namespace Game
{
	public class UITest23 : NoodUI 
	{
		public static UITest23 Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UITest23>("Prefab/UITest23"), parent);
		}
	}
}
