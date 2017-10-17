using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	public interface IEnemyControl  {	
		Transform target{get;}
		CharacterControl.AnimationState animationState{get;}
	}

}
