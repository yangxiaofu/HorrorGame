using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Characters{
	public class EnemyControl : CharacterControl, IEnemyControl {
		[Range(0, 1)]
		[SerializeField] float _hitSuccessPercentage = .50f;
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
			_enemyAnimationController.UpdateAnimationState();

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

		void Hit()
		{
			//Calculate Hit Percentage on teh player.
			if (UnityEngine.Random.Range(0f, 1f) < _hitSuccessPercentage)
			{
				
				_target.GetComponent<CharacterHealth>().TakeDamage(10);	
				//TODO: Do Hit Sounds.
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

