using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;

namespace Game.Items{
	public class Projectile : MonoBehaviour {
		[SerializeField] float _speed = 20f;
		public float speed{get{return _speed;}}
		[SerializeField] float _damage = 50f;
		public float damage{get{return _damage;}}
		[SerializeField] float _destroyTime = 2f;

		void Start()
		{
			StartCoroutine(DestroyProjectile());
		}

		IEnumerator DestroyProjectile()
		{
			yield return new WaitForSeconds(_destroyTime);

			Destroy(this.gameObject);

		}
		void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.GetComponent<Enemy>()){
				var enemy = other.gameObject.GetComponent<CharacterHealth>();
				enemy.TakeDamage(_damage);
			}			
		}
	}
}

