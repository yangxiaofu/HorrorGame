using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	public class Character : MonoBehaviour {
		[Header("Character General")]
		[SerializeField] protected float _meleeAttackRadius = 2f;

		void OnDrawGizmos(){
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(this.transform.position, _meleeAttackRadius);
		}
	}
}

