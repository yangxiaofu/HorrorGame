using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Core;
using System;

namespace Game.Characters{
	public class Player : Character{
		[SerializeField] float _meleeWeaponDamage = 10f;
		CameraRaycaster _cameraRaycaster;


		void Start(){
			_cameraRaycaster = FindObjectOfType<CameraRaycaster>();
			Assert.IsNotNull(_cameraRaycaster, "Camera Raycaster is not available.");

			_cameraRaycaster.OnMouseOverEnemy += OnMouseOverEnemy;
		}

        private void OnMouseOverEnemy(Enemy enemy)
        {
			if (Input.GetMouseButtonDown(0)){
				Debug.Log("Attack the enemy " + enemy);
			}
        }
    }
}

