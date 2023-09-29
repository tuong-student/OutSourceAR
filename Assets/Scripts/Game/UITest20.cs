using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI; 

namespace Game
{
	public class UITest20 : NoodUI 
	{
		public static UITest20 Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UITest20>("Prefab/UITest20"), parent);
		}
	}
}
