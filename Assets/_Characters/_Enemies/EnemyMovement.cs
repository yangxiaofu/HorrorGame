using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Characters{
	public class EnemyMovement : CharacterMovement{
		public NavMeshAgent agent { get; private set; }      
		void Start()
        {
            GetCharacterMovementComponents();
            SetupNavMeshVariables();
        }

        private void SetupNavMeshVariables()
        {
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            agent.updateRotation = false;
            agent.updatePosition = true;
        }

        void Update()
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
            }
                
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                Move(agent.desiredVelocity, false, false);

            } else {

                Move(Vector3.zero, false, false);
            }
        }	
	}
}