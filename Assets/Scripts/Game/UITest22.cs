using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI; 

namespace Game
{
	public class UITest22 : NoodUI 
	{
		public static UITest22 Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UITest22>("Prefab/UITest22"), parent);
		}
	}
}
