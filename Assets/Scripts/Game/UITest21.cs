using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI; 

namespace Game
{
	public class UITest21 : NoodUI 
	{
		public static UITest21 Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UITest21>("Prefab/UITest21"), parent);
		}
	}
}
