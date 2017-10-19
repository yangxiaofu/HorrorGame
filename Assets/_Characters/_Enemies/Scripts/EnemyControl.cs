using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Characters{
	public class EnemyControl : CharacterControl, IEnemyControl 
	{
		const string DEFAULT_ATTACK = "DEFAULT_ATTACK";
		const string ANIMATION_STATE_ATTACK = "Attack";
		NavMeshAgent _agent;
		Transform _target;
		public Transform target{get{return _target;}}
		EnemyAnimationController _enemyAnimationController;
        Player _player;
		void Awake()
        {
            AddNavMeshAgentComponent();
        }
		void Start()
		{
			_enemyAnimationController = new EnemyAnimationController(this);
			_anim = GetComponent<Animator>();
			_body = GetComponent<Rigidbody>();
            _player = FindObjectOfType<Player>();
		}
        
        void Update ()
        {
            if (EnemyIsDead())
            {
                _agent.isStopped = true;
                return;
            }

            if (_target != null && TargetIsDead()) {
                return;
            }
				
            _enemyAnimationController.UpdateAnimationState();
            UpdateAnimation();
        }

        public bool TargetIsDead()
		{
			if (_target != null)
			{
				return (_target.GetComponent(typeof(Character)) as Character).isDead;
			} 
			else 
			{
				return false;
			}
		}

		void FixedUpdate()
		{
			MoveBodyPosition();
		}

        private void UpdateAnimation()
        {
            if (_enemyAnimationController.animationState == CharacterControl.AnimationState.ATTACK)
            {
                _anim.Play(ANIMATION_STATE_ATTACK);
				_anim.SetBool(IS_IDLE, false);
                _agent.isStopped = true;
                float delay = GetComponent<Enemy>().animOC[DEFAULT_ATTACK].length;
                StartCoroutine(SetBacktoForwardState(delay));
            }
            else if (_enemyAnimationController.animationState == CharacterControl.AnimationState.FORWARD)
            {
                _anim.Play(ANIMATION_STATE_FORWARD);
                _anim.SetBool(IS_IDLE, false);
                _agent.isStopped = false;
                _agent.SetDestination(_target.position);
            }
            else if (_enemyAnimationController.animationState == CharacterControl.AnimationState.IDLE)
            {
                _anim.SetBool(IS_IDLE, true);
            }
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

        public bool EnemyIsDead(){
			return (GetComponent(typeof(Character)) as Character).isDead;
		}

	}
}

