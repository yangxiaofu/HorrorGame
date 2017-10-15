using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	public class PlayerEnergyController {
		IPlayerEnergy _playerEnergy;
		public PlayerEnergyController(IPlayerEnergy playerEnergy)
		{
			_playerEnergy = playerEnergy;
		}

		public void ReduceEnergy(float energyToReduce){
			_playerEnergy.currentEnergy -= energyToReduce;

			_playerEnergy.currentEnergy = Mathf.Clamp(
				_playerEnergy.currentEnergy, 
				_playerEnergy.minimumEnergyLevel, 
				_playerEnergy.startingEnergy
			);
		}

		public void IncreaseEnergy(float energyToAdd){
			_playerEnergy.currentEnergy += energyToAdd;
			_playerEnergy.currentEnergy = Mathf.Clamp(
				_playerEnergy.currentEnergy, 
				_playerEnergy.minimumEnergyLevel, 
				_playerEnergy.startingEnergy
			);
		}

	}

}

