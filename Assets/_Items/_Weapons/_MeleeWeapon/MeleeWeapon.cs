using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
    [CreateAssetMenu(menuName = "Game/Items/Melee Weapon")]
    public class MeleeWeapon : WeaponConfig
    {
        public override void AddComponentTo(GameObject gameObjectToAddTo)
        {
            _behaviour = gameObjectToAddTo.AddComponent<MeleeWeaponBehaviour>();
        }

        public override void UseWeapon()
        {
            _behaviour.UseWeapon();
        }
    }
}

