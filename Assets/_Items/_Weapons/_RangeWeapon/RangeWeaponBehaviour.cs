using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Core;

namespace Game.Items{
    public class RangeWeaponBehaviour : WeaponBehaviour
    {
		CameraRaycaster _cameraRaycaster;
		void Start()
		{
			_cameraRaycaster = FindObjectOfType<CameraRaycaster>();
			Assert.IsNotNull(_cameraRaycaster);
		}
        public override void UseWeapon()
        {
            ProjectileSocket projectileSocket = FindProjectileSocket();

            var prefab = (_config as RangeWeapon).GetProjectilePrefab();
            var projectileObj = Instantiate(prefab, projectileSocket.transform.position, Quaternion.identity) as GameObject;
            var rb = projectileObj.GetComponent<Rigidbody>();
            Assert.IsNotNull(rb, "The projectile you are instantiating needs a rigid body component on there.");

            var direction = (_cameraRaycaster.mousePosition - this.transform.position).normalized;
            rb.velocity = direction * projectileObj.GetComponent<Projectile>().speed;
        }

        private ProjectileSocket FindProjectileSocket()
        {
            var _socket = GetComponentInChildren<ProjectileSocket>();
            Assert.IsNotNull(
                _socket,
                "You need to add a projectile socket childed on to the game object of the range weapon that you currently have equipped"
            );
            return _socket;
        }
    }
}

