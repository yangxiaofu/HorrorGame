using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Characters{
	public class EnemyControl : CharacterControl {
		const string ANIMATION_STATE_ATTACK = "Attack";
		const string DEFAULT_ATTACK = "DEFAULT_ATTACK";
		NavMeshAgent agent;
		Transform _target;
		void Awake()
        {
            AddNavMeshAgentComponent();
			AddCapsuleCollider();
            AddRigidBodyComponent();
			AddAnimatorComponent();
        }

        private void AddNavMeshAgentComponent()
        {
            agent = this.gameObject.AddComponent<NavMeshAgent>();
            agent.speed = _forwardSpeed;
        }

        void Update () {
			if (_target != null)
            {
				if (_animationState == AnimationState.ATTACK){
					_animationState = AnimationState.ATTACK;
					_anim.Play(ANIMATION_STATE_ATTACK);
					float delay = _animOC[DEFAULT_ATTACK].length;

					StartCoroutine(SetBacktoForwardState(delay));
				} else {
					_anim.SetBool(IS_IDLE, false);
					_animationState = AnimationState.FORWARD;
					_anim.Play(ANIMATION_STATE_FORWARD);
					agent.SetDestination(_target.position);
				}
            }
            else if (_target == null && _animationState != AnimationState.IDLE)
			{
				SetIdleAnimation();
			} 
		}

		IEnumerator SetBacktoForwardState(float delay){
			yield return new WaitForSeconds(delay);
			_animationState = AnimationState.FORWARD;
			yield return null;
		}

		public void SetState(AnimationState animationState)
		{
			_animationState = animationState;
		}

        void FixedUpdate()
		{
			MoveBodyPosition();
		}

		public void SetTarget(Transform target)
        {
            _target = target;
        }

		
	}
}

