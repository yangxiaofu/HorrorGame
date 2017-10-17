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
                agent.SetDestination(_target.position);
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

