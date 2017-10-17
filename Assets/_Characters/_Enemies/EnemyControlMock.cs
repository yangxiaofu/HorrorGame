using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
    public class EnemyControlMock : IEnemyControl
    {
		private readonly Transform _target;
		private readonly CharacterControl.AnimationState _animationState;
		public EnemyControlMock(Transform target, CharacterControl.AnimationState animationState){
			_target = target;
			_animationState = animationState;
		}
        public Transform target {get{return _target;}}
        public CharacterControl.AnimationState animationState {
			get{return _animationState;}
		}
    }
}

