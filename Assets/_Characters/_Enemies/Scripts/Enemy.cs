using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Characters{
	[SelectionBase]
	[RequireComponent(typeof(EnemyControl))]
	public class Enemy : Character{
		[Range(0, 1)]
		[SerializeField] float _hitSuccessPercentage = .50f;
		[SerializeField] float _hitDamage = 50f;
		EnemySight _sight;
		EnemyControl _enemyControl;
		Player _player;
		
		void Awake()
		{
			AddCapsuleCollider();
            AddRigidBodyComponent();
			AddAnimatorComponent();			
		}
		void Start(){

			_player = FindObjectOfType<Player>();
			Assert.IsNotNull(_player);

			_sight = GetComponentInChildren<EnemySight>();
			
			Assert.IsNotNull(
				_sight, 
				"You need to add the player sight into the transform of the player."
			);

			_sight.Setup(this.transform);

			_sight.OnPlayerSeen += OnPlayerSeen;

			_enemyControl = GetComponent<EnemyControl>();
			Assert.IsNotNull(_enemyControl, "There is no enemy control scrip on the game object of " + name);
		}

		void LateUpdate()
		{
			if (_enemyControl.TargetIsDead()) return;

			ScanForPlayerWithinSightRadius();
			ScanForPlayerInAttackRadius();				
		}
		void Hit() //Callback furnction from the animatior.
		{
			
			//Calculate Hit Percentage on teh player.
			if (UnityEngine.Random.Range(0f, 1f) < _hitSuccessPercentage)
			{
				if (_enemyControl.target == null) return;
				_enemyControl.target.GetComponent<CharacterHealth>().TakeDamage(_hitDamage);//TODO: Refactor the magic number out. 
			} 
			else 
			{
				//TODO: Do animation where you miss
				//TODO: Do sounds where you miss the player.
				print("MISSED THE PLAYER");
			}
			
		}

		private void ScanForPlayerInAttackRadius()
        {
			var distanceFromPlayer = Vector3.Distance(_player.transform.position, this.transform.position);
			if (distanceFromPlayer < _meleeAttackRadius)
			{
				_enemyControl.SetState(CharacterControl.AnimationState.ATTACK);	
			}
        }
		

        private void ScanForPlayerWithinSightRadius()
        {
            var distanceFromPlayer = Vector3.Distance(this.transform.position, _player.transform.position);

            if (distanceFromPlayer > _sight.sightDistance)
            {
                _enemyControl.SetTarget(null);
            }
        }

        void OnPlayerSeen(Player player)
		{
			_enemyControl.SetTarget(player.transform);
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;

			Gizmos.DrawWireSphere(
				this.transform.position, 
				GetComponentInChildren<EnemySight>().sightDistance
			);
		}

        public override void ResetCharacter()
        {
            Destroy(this.gameObject);
        }
    }

}
