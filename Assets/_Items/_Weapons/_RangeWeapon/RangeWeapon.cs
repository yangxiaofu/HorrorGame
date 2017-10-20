using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
	[CreateAssetMenu(menuName = "Game/Items/Range Weapon")]
	public class RangeWeapon : WeaponConfig {
		[SerializeField] GameObject _projectilePrefab;
		[SerializeField] AudioClip _shotAudio;
		public AudioClip GetShotAudioClip()
		{
			return _shotAudio;
		}
		public GameObject GetProjectilePrefab(){
			return _projectilePrefab;
		}
		[SerializeField] Transform _projectileSocket;
		public Transform GetProjectileSocket(){
			return _projectileSocket;
		}


		
        public override void AddComponentTo(GameObject gameObjectToAddTo)
        {
            _behaviour = gameObjectToAddTo.AddComponent<RangeWeaponBehaviour>();
			_behaviour.SetupConfig(this);
        }

        public override void UseWeapon()
        {
			_behaviour.UseWeapon();
        }
    }

}
