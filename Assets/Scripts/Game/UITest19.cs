using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI; 

namespace Game
{
	public class UITest19 : NoodUI 
	{
		public static UITest19 Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UITest19>("Prefab/UITest19"), parent);
		}
	}
}
