using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	public class CharacterHealth : MonoBehaviour {
		[SerializeField] float _currentHealth = 100f;
		[SerializeField] float _startingHealth = 100f;
		[SerializeField] float _secondsBeforeDeathDisappear = 2f;
		public float healthAsPercentage
		{
			get{return _currentHealth / _startingHealth;}
		}

		public void TakeDamage(float damage)
		{
			_currentHealth -= damage;
			_currentHealth = Mathf.Clamp(_currentHealth, 0, _startingHealth);

			if (_currentHealth <= 0)
			{
				var character = GetComponent(typeof(Character)) as Character;
				StartCoroutine(character.KillCharacter(_secondsBeforeDeathDisappear));
			}
		}

		public void ResetHealth()
		{
			_currentHealth = _startingHealth;
		}
		protected void Heal(float heal){
			_currentHealth += heal;
			Mathf.Clamp(_currentHealth, 0, _startingHealth);
		}
	}
}

