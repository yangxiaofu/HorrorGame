using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Core;

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
		
		[Tooltip("Adjusting this impacts the minimum energy level that impacts the player movement.")]
		[Range(0, 1)]
		[SerializeField] float _minimumEnergyFactor = 0.1f;
		float _minimumEnergyLevel = 0f;
		public float minimumEnergyLevel{get{return _minimumEnergyLevel;}}
		PlayerEnergyController _controller;
		PlayerControl _playerControl;
		public float energyAsPercentage{
			get{return _currentEnergy / _startingEnergy;}
		}

        void Start(){
			_controller = new PlayerEnergyController(this);

			_playerControl = GetComponent<PlayerControl>();
			Assert.IsNotNull(_playerControl, "Player Control is not on the player game object.");
			
			_playerControl.OnEnergyKeyDown += IncreaseEnergy;

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

		public void IncreaseEnergy(float energyToAdd)
		{
			_controller.IncreaseEnergy(energyToAdd);
		}

		public float GetEnergyFactor()
        {
            var energyLevel = GetComponent<PlayerEnergy>();
            var energyFactor = energyLevel.energyAsPercentage;
			
			float maximumEnergyFactor = 1;

			energyFactor = Mathf.Clamp(
				energyFactor, 
				_minimumEnergyFactor, 
				maximumEnergyFactor
			);

            return energyFactor;
        }
	
	}

}
