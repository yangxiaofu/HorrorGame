using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
    public class MeleeWeaponBehaviour : WeaponBehaviour
    {
        public override void UseWeapon()
        {
            print("Use the melee weapon");
        }
    }

}
