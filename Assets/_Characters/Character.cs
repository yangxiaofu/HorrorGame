using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	public abstract class Character : MonoBehaviour {
		[Header("Character General")]
		[SerializeField] protected float _meleeAttackRadius = 2f;
		[SerializeField] AnimationClip _deathAnimationClip;
		protected bool _isDead = false;
		public bool isDead{get{return _isDead;}}
		const string DEATH_TRIGGER = "death";
		void OnDrawGizmos()
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(this.transform.position, _meleeAttackRadius);
		}

		public IEnumerator KillCharacter(float delay)
		{
			_isDead = true;
			//Do death animation.
			GetComponent<Animator>().SetTrigger(DEATH_TRIGGER);

			yield return new WaitForSeconds(delay);

			RemoveCharacter();

			yield return null;
		}

		public abstract void RemoveCharacter();
		
	}
}

