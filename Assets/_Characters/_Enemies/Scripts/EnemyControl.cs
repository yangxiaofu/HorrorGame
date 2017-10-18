using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Characters{
	public class EnemyControl : CharacterControl, IEnemyControl {
		[Range(0, 1)]
		[SerializeField] float _hitSuccessPercentage = .50f;
		[SerializeField] float _hitDamage = 50f;
		const string DEFAULT_ATTACK = "DEFAULT_ATTACK";
		const string ANIMATION_STATE_ATTACK = "Attack";
		NavMeshAgent _agent;
		Transform _target;
		public Transform target{get{return _target;}}
		EnemyAnimationController _enemyAnimationController;
		void Awake()
        {
            AddNavMeshAgentComponent();
			AddCapsuleCollider();
            AddRigidBodyComponent();
			AddAnimatorComponent();
        }
		void Start()
		{
			_enemyAnimationController = new EnemyAnimationController(this);
		}
        void Update ()
        {
            if (EnemyIsDead())
            {
                _agent.isStopped = true;
                return;
            }

            if (_target != null && TargetIsDead()) return;

            _enemyAnimationController.UpdateAnimationState();
			
            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            if (_enemyAnimationController.animationState == CharacterControl.AnimationState.ATTACK)
            {
                _anim.Play(ANIMATION_STATE_ATTACK);
                _agent.isStopped = true;
                float delay = _animOC[DEFAULT_ATTACK].length;
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

        bool EnemyIsDead(){
			return (GetComponent(typeof(Character)) as Character).isDead;
		}

		bool TargetIsDead(){
			return (_target.GetComponent(typeof(Character)) as Character).isDead;
		}

		void Hit()
		{
			//Calculate Hit Percentage on teh player.
			if (UnityEngine.Random.Range(0f, 1f) < _hitSuccessPercentage)
			{
				if (_target == null) return;
				_target.GetComponent<CharacterHealth>().TakeDamage(_hitDamage);//TODO: Refactor the magic number out. 
			} 
			else 
			{
				//TODO: Do animation where you miss
				//TODO: Do sounds where you miss the player.
				print("MISSED THE PLAYER");
			}
			
		}

		void FixedUpdate()
		{
			MoveBodyPosition();
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

