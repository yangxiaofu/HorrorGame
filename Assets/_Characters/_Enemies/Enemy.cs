using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Characters{
	public class Enemy : Character{
		EnemySight _sight;
		CharacterMovement _characterMovement;
		Player _player;
		void Start(){

			_player = FindObjectOfType<Player>();
			Assert.IsNotNull(_player);

			_sight = GetComponentInChildren<EnemySight>();
			Assert.IsNotNull(_sight);
			_sight.Setup(this.transform);

			_characterMovement = GetComponent<CharacterMovement>();
			Assert.IsNotNull(_characterMovement);

			_sight.OnPlayerSeen += OnPlayerSeen;
		}

		void Update()
        {
            ScanForPlayerWithinSightRadius();
        }

        private void ScanForPlayerWithinSightRadius()
        {
            var distanceFromPlayer = Vector3.Distance(this.transform.position, _player.transform.position);

            if (distanceFromPlayer > _sight.sightDistance)
            {
                _characterMovement.SetTarget(null);
            }
        }

        void OnPlayerSeen(Player player)
		{
			_characterMovement.SetTarget(player.transform);
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;

			Gizmos.DrawWireSphere(
				this.transform.position, 
				GetComponentInChildren<EnemySight>().sightDistance
			);
		}
	}

}
