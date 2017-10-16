using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	public class CharacterHealth : MonoBehaviour {
		[SerializeField] float _currentHealth = 100f;
		[SerializeField] float _startingHealth = 100f;
		[SerializeField] float _secondsBeforeDeathDisappear = 2f;
		public float healthAsPercentage{
			get{return _currentHealth / _startingHealth;}
		}

		public void TakeDamage(float damage){
			_currentHealth -= damage;
			_currentHealth = Mathf.Clamp(_currentHealth, 0, _startingHealth);

			if (_currentHealth <= 0){
				StartCoroutine(KillCharacter(_secondsBeforeDeathDisappear));
			}
		}

		IEnumerator KillCharacter(float delay){
			yield return new WaitForSeconds(delay);

			Destroy(this.gameObject);

			yield return null;
		}

		protected void Heal(float heal){
			_currentHealth += heal;
			Mathf.Clamp(_currentHealth, 0, _startingHealth);
		}
	}
}

