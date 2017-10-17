using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	public class EnemyAnimationController {
		private readonly IEnemyControl _enemyControl;
		CharacterControl.AnimationState _animationState;
		public CharacterControl.AnimationState animationState{get{return _animationState;}}
		public EnemyAnimationController(IEnemyControl enemyControl)
		{
			_enemyControl = enemyControl;
		}
		public void UpdateAnimationState()
		{
			if (_enemyControl.target != null) //There's a target enemy in sight.
            {
				if (_enemyControl.animationState == CharacterControl.AnimationState.ATTACK)
				{
					_animationState = CharacterControl.AnimationState.ATTACK;
				} 
				else if (_enemyControl.animationState == CharacterControl.AnimationState.FORWARD)
				{
					_animationState = CharacterControl.AnimationState.FORWARD;
				} 
				else 
				{
					Debug.Log("Do Nothing");	
				}
            }
            else if (_enemyControl.target == null && _enemyControl.animationState != CharacterControl.AnimationState.IDLE)
			{
				_animationState = CharacterControl.AnimationState.IDLE;	
			} 
		}
	}
}

