using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Enemy Control and animation. 
namespace Game.Characters{
	public class EnemyControl : CharacterControl, IEnemyControl 
	{
		const string DEFAULT_ATTACK = "DEFAULT_ATTACK";
		const string ANIMATION_STATE_ATTACK = "Attack";
		NavMeshAgent _agent;
		Transform _target;
		public Transform target{get{return _target;}}
		EnemyAnimationController _enemyAnimationController;
		
		void Awake()
        {
            AddNavMeshAgentComponent();
        }
		void Start()
		{
			_enemyAnimationController = new EnemyAnimationController(this);
			GetAnimatorComponent();
			GetRigidBodyComponent();
		}

        public bool TargetIsDead()
		{
			if (_target == null) return false;

			return _target.GetComponent<Player>().isDead;
		}

		void FixedUpdate()
		{
			MoveBodyPosition();
		}

        public void UpdateEnemyMovementAnimation()
        {
			_enemyAnimationController.UpdateAnimationState();

            if (_enemyAnimationController.animationState == CharacterControl.AnimationState.ATTACK)
            {
                PlayAttackAnimation();
            }
            else if (_enemyAnimationController.animationState == CharacterControl.AnimationState.FORWARD)
            {
                PlayWalkForwardAnimation();
            }
            else if (_enemyAnimationController.animationState == CharacterControl.AnimationState.IDLE)
            {
                PlayIdleAnimation();
            }
        }

        private void PlayIdleAnimation()
        {
            _agent.isStopped = true;
            _anim.SetBool(IS_IDLE, true);
        }

        private void PlayWalkForwardAnimation()
        {
            _anim.Play(ANIMATION_STATE_FORWARD);
            _anim.SetBool(IS_IDLE, false);
            _agent.isStopped = false;
            _agent.SetDestination(_target.position);
        }

        private void PlayAttackAnimation()
        {
            _anim.Play(ANIMATION_STATE_ATTACK);
            _anim.SetBool(IS_IDLE, false);
            _agent.isStopped = true;
            float delay = GetComponent<Enemy>().animOC[DEFAULT_ATTACK].length;
            StartCoroutine(SetBacktoForwardState(delay));
        }

        private void AddNavMeshAgentComponent()
        {
            _agent = this.gameObject.AddComponent<NavMeshAgent>();
            _agent.speed = _forwardSpeed;
        }

		IEnumerator SetBacktoForwardState(float delay)
		{
			yield return new WaitForSeconds(delay);
			_animationState = AnimationState.FORWARD;
			_agent.isStopped = false;
			yield return null;
		}
		public void SetState(AnimationState animationState)
		{
			_animationState = animationState;
		}

		public void SetTarget(Transform target)
        {
            _target = target;
            
        }
	}
}

