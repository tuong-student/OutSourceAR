using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI; 

namespace Game
{
	public class UITest17 : NoodUI 
	{
		public static UITest17 Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UITest17>("Prefab/UITest17"), parent);
		}
	}
}
