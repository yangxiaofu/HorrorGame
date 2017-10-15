using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{

	public class PlayerEnergy : MonoBehaviour, IPlayerEnergy {
		[SerializeField] float _currentEnergy = 100f;
		public float currentEnergy{
			get{return _currentEnergy;}
			set{_currentEnergy = value;}
		}

		[SerializeField] float _startingEnergy = 100f;
		public float startingEnergy{get{return _startingEnergy;}}
		[SerializeField] float _energyLostPerSecond = .1f;
		float _minimumEnergyLevel = 0f;
		public float minimumEnergyLevel{get{return _minimumEnergyLevel;}}
		PlayerEnergyController _controller;
		public float energyAsPercentage{
			get{return _currentEnergy / _startingEnergy;}
		}

        void Start(){
			_controller = new PlayerEnergyController(this);
		}
		void Update()
		{
			float energyLost = _energyLostPerSecond * Time.deltaTime;
			ReduceEnergy(energyLost);
		}

		private void ReduceEnergy(float energyToReduce)
		{
			_controller.ReduceEnergy(energyToReduce);
		}

		public void IncreaseEnergy(float energyToAdd){
			_controller.IncreaseEnergy(energyToAdd);
		}
	
	}

}
