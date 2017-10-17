using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
	
	public abstract class WeaponConfig : ItemConfig {

		[SerializeField] Transform _weaponGripTransform;
		[SerializeField] AnimationClip _animation;

		protected WeaponBehaviour _behaviour;

		public Transform weaponGripTransform{
			get{return _weaponGripTransform;}
		}
		public override void AddToInventory(Inventory inventory)
		{
			inventory.AddItem(this);
		}

        public AnimationClip GetAnimation()
        {
            return _animation;
        }

		public abstract void AddComponentTo(GameObject gameObjectToAddTo);
		public abstract void UseWeapon();
    }

}
