using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Characters{
	public class EnemyControl : CharacterControl {

		NavMeshAgent agent;
		Transform _target;
		// Use this for initialization

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
                _anim.SetBool(IS_IDLE, false);
            	_movementState = MovementState.FORWARD;
            	_anim.Play(ANIMATION_STATE_FORWARD);
            	agent.SetDestination(_target.position);
            }
            else {
				SetIdleAnimation();
			}
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

