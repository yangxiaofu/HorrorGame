using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
	public abstract class WeaponBehaviour : MonoBehaviour {	
		protected WeaponConfig _config;
		public void SetupConfig(WeaponConfig config)
		{
			_config = config;
		}
		public abstract void UseWeapon();
	}
}

