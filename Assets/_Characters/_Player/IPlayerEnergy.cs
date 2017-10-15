using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	public interface IPlayerEnergy{
		float currentEnergy{get;set;}
		float startingEnergy{get;}
		float minimumEnergyLevel{get;}
	}
}
