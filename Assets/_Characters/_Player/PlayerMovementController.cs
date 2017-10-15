using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	public class PlayerMovementController {
		private IPlayerControl _playerControl;
		const string HORIZONTAL_AXIS = "Horizontal";
		const string VERTICAL_AXIS = "Vertical";
		const string WALK_FORWARD = "Walk_Forward";
		const string WALK_BACKWARD = "Walk_Backward";
		public PlayerMovementController(IPlayerControl playerControl)
		{
			_playerControl = playerControl;
		}

		public void ScanForDirectionInput()
		{
			_playerControl.inputs = new Vector3(
				Input.GetAxis(HORIZONTAL_AXIS), 
				0, 
				Input.GetAxis(VERTICAL_AXIS)
			);
			//TODO: Scan for straffing and walking backwards.  Detect the forward osition and get the engle to determine the animation that it needs. 
			if (_playerControl.inputs != Vector3.zero)
            {
                float angleOfForwardWalking = 45f;
                float angleofBackwardWalking = 135f;
                float anyNumberAboveZero = 10f;
                float energyFactor = GetEnergyFactor();

                if (_playerControl.angleFromSightPosition < angleOfForwardWalking)
                {	
                    _playerControl.anim.SetFloat(WALK_FORWARD, anyNumberAboveZero);
                    _playerControl.speed = _playerControl.forwardSpeed * energyFactor;
					_playerControl.anim.speed = _playerControl.speed / 5; //Walk animation speed.
                }
                else if (_playerControl.angleFromSightPosition > angleofBackwardWalking)
                {
                    _playerControl.anim.SetFloat(WALK_BACKWARD, anyNumberAboveZero);
                    _playerControl.speed = _playerControl.backwardSpeed * energyFactor;
                }
            }
            else if (_playerControl.inputs == Vector3.zero){
				float stoppingValue = 0f;
				_playerControl.anim.SetFloat(WALK_FORWARD, stoppingValue);
				_playerControl.anim.SetFloat(WALK_BACKWARD, stoppingValue);
				_playerControl.anim.speed = 1;
			}
		}

		

		private float GetEnergyFactor()
        {
            var energyLevel = _playerControl.GetPlayerEnergyComponent();
            var energyFactor = energyLevel.energyAsPercentage;
			
			float maximumEnergyFactor = 1;

			energyFactor = Mathf.Clamp(
				energyFactor, 
				_playerControl.minimumEnergyFactor, 
				maximumEnergyFactor
			);

            return energyFactor;
        }
	}

}
