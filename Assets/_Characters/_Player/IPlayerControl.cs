using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	public interface IPlayerControl{
		Vector3 inputs{get;set;}
		float angleFromSightPosition{get;}
		Animator anim{get;}
		float forwardSpeed{get;set;}
		float backwardSpeed{get;set;}
		float minimumEnergyFactor{get;}
		float speed{get;set;}
		PlayerEnergy GetPlayerEnergyComponent();
	}
}

