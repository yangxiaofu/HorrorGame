using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{

	public class PlayerMovementController {
		private readonly float _angleContraintForForwardWalking = 0;
		private readonly float _angleConstraintForBackwardWalking = 0;

		public enum MovementDirection{
			FORWARD, 
			BACKWARD,
			RIGHT, 
			LEFT,
			IDLE
		}

		public MovementDirection movementDirection;
		private readonly IPlayerControl _playerControl;
		public PlayerMovementController(IPlayerControl playerControl, WalkAngleRestrictionArgs args)
		{
			_playerControl = playerControl;	
			_angleContraintForForwardWalking = args.angleContraintForForwardWalking;
			_angleConstraintForBackwardWalking = args.angleConstraintForBackwardWalking;
		}

		public void UpdateMovementDirection(float angleFromSightPosition)
		{	
			if (_playerControl.inputs != Vector3.zero)
            {
                if (angleFromSightPosition < _angleContraintForForwardWalking && angleFromSightPosition > -_angleContraintForForwardWalking)
                {	
					movementDirection = MovementDirection.FORWARD;
                }
                else if (angleFromSightPosition >= _angleConstraintForBackwardWalking)
                {
					movementDirection = MovementDirection.BACKWARD;
                }
				else if (Mathf.Abs(angleFromSightPosition) > _angleContraintForForwardWalking && angleFromSightPosition < 0)
				{
					movementDirection = MovementDirection.LEFT;
				}
				else if (Mathf.Abs(angleFromSightPosition) > _angleContraintForForwardWalking && angleFromSightPosition > 0)
				{
					movementDirection = MovementDirection.RIGHT;
				}
            }
            else if (_playerControl.inputs == Vector3.zero){
				movementDirection = MovementDirection.IDLE;
			}
		}
	}

	public struct WalkAngleRestrictionArgs{
		public float angleContraintForForwardWalking;
		public float angleConstraintForBackwardWalking;
		public WalkAngleRestrictionArgs(float angleContraintForForwardWalking, float angleConstraintForBackwardWalking){
			this.angleContraintForForwardWalking = angleContraintForForwardWalking;
			this.angleConstraintForBackwardWalking = angleConstraintForBackwardWalking;
		}
	}

}
