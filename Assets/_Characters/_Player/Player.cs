﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Core;
using Game.Items;
using System;

namespace Game.Characters{
	[SelectionBase]
	public class Player : Character{
		[SerializeField] float _meleeWeaponDamage = 10f;
		[SerializeField] float _pickupDistance = 2f;
		public float pickupDistance{get{return _pickupDistance;}}
		CameraRaycaster _cameraRaycaster;

		void Start(){
			_cameraRaycaster = FindObjectOfType<CameraRaycaster>();
			Assert.IsNotNull(
				_cameraRaycaster, 
				"Camera Raycaster is not available."
			);

			_cameraRaycaster.OnMouseOverEnemy += OnMouseOverEnemy;

			
		}

        private void OnMouseOverEnemy(Enemy enemy)
        {
			if (Input.GetMouseButtonDown(0)){
				float distanceFromEnemy = Vector3.Distance(transform.position, enemy.transform.position);

				if (distanceFromEnemy <= _meleeAttackRadius)
				{
					//TODO: Perform the attack animation.
					enemy.GetComponent<CharacterHealth>().TakeDamage(_meleeWeaponDamage);
				} else {
					//TODO: Shoot the enemy.
					
				}
				
			}

			
        }
    }
}

