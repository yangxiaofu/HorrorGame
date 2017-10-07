using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Characters{
	public class Enemy : Character{
		EnemySight _sight;
		CharacterMovement _characterMovement;

		void Start(){
			_sight = GetComponentInChildren<EnemySight>();
			Assert.IsNotNull(_sight);

			_characterMovement = GetComponent<CharacterMovement>();
			Assert.IsNotNull(_characterMovement);

			_sight.OnPlayerSeen += OnPlayerSeen;
		}

		void OnPlayerSeen(Player player)
		{
			_characterMovement.SetTarget(player.transform);
		}

	}

}
