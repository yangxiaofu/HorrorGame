using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
    public class PlayerEnergyMock : IPlayerEnergy
    {
		private float _currentEnergy;
		private readonly float _startingEnergy;
		private readonly float _minimumEnergyLevel = 0;
        public PlayerEnergyMock(float currentEnergy, float startingEnergy)
        {
			_currentEnergy = currentEnergy;
			_startingEnergy = startingEnergy;
        }

        public float currentEnergy { 
			get {return _currentEnergy;} 
			set{_currentEnergy = value;}
		}

        public float startingEnergy {
			get{return _startingEnergy;}
		}

        public float minimumEnergyLevel {
			get{return _minimumEnergyLevel;}
		}
    }
}

