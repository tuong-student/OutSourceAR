using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI; 

namespace Game
{
	public class UITest18 : NoodUI 
	{
		public static UITest18 Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UITest18>("Prefab/UITest18"), parent);
		}
	}
}
