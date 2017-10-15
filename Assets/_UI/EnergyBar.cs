using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Characters;

namespace Game.UI{
	[RequireComponent(typeof(RawImage))]
	public class EnergyBar : Bar{

		void Start()
        {
            SetupBarVariables();
        }

		void Update()
		{
			float percentage = _player.GetComponent<PlayerEnergy>().energyAsPercentage;
			UpdateUIBar(percentage);
		}

       

    }
}

