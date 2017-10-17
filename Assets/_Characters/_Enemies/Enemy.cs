using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Characters{
	[SelectionBase]
	[RequireComponent(typeof(EnemyControl))]
	public class Enemy : Character{
		EnemySight _sight;
		EnemyControl _enemyControl;
		Player _player;
		Animator _anim;
		
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

			_anim = GetComponent<Animator>();  //Added in the enemy Control method. //TODO: Refactor from enemy control into player later. 
		}

		void Update()
        {
            ScanForPlayerWithinSightRadius();
			ScanForPlayerInAttackRadius();
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

        public override void RemoveCharacter()
        {
            Destroy(this.gameObject);
        }
    }

}
