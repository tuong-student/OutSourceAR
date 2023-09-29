using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI; 

namespace Game.UI
{
	public class UIDebug : NoodUI 
	{
		public static UIDebug Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UIDebug>("Prefab/Game/UI/UIDebug"), parent);
		}
	}
}
