using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI; 

namespace Game
{
	public class UITest25 : NoodUI 
	{
		public static UITest25 Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UITest25>("Prefab/UITest25"), parent);
		}
	}
}
