using System; 
using System.Collections.Generic; 
using System.Collections; 
using UnityEngine; 
using NOOD; 
using NOOD.UI;
using UnityEngine.UI;

namespace Game.UI
{
	public class UISelector : NoodUI 
	{
		[SerializeField] private Image _aim;
		public Vector2 AimPosition
		{
			get => _aim.rectTransform.position;
		}

		public static UISelector Create(Transform parent = null)
		{
			return Instantiate(Resources.Load<UISelector>("Prefab/Game/UI/UISelector"), parent);
		}
	}
}
