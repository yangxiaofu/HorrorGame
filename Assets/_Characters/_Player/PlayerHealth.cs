using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Items;
using System;

namespace Game.Characters{
	public class PlayerHealth : CharacterHealth {

		// Use this for initialization
		void Start () {
			GetComponent<PlayerControl>().OnHealthKeyDown += HealPlayer;
		}

        private void HealPlayer()
        {
			//TODO: Do something more specific to the type of food that is in ivnentory.
            Heal(10);
        }

      
	}
}

