﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Core;
using System;
using Game.Characters;

namespace Game.Items{
	public class WeaponSystem : MonoBehaviour {
		[SerializeField] WeaponConfig _equippedWeapon; //Used for testing and debugging pursposes. 
		ItemGrip _equippedWeaponGrip;
		CameraRaycaster _cameraRaycaster;
		Animator _anim;
		AudioSource _audioSource;
		const string DEFAULT_ATTACK = "DEFAULT_ATTACK";
		const string ANIMATION_STATE_ATTACK = "Attack";

		void Start()
		{
			_cameraRaycaster = FindObjectOfType<CameraRaycaster>();
			Assert.IsNotNull(
				_cameraRaycaster, 
				"Camera Raycaster is no longer in the game scene."
			);

			_anim = GetComponent<Animator>();
			Assert.IsNotNull(
				_anim, 
				"There is no animation that starts up on run-time."
			);		

			_audioSource = GetComponent<AudioSource>();
			Assert.IsNotNull(_audioSource);
		}

		void Update()
		{
			DestroyItemsInWeaponGrip();
			AddEquippedWeaponToWeaponGrip();
			ScanForCharacterAttack();	
		}

		public void SetEquippedWeapon(WeaponConfig weapon)
		{
			_equippedWeapon = weapon;
			_equippedWeapon.AddComponentTo(this.gameObject);
		}

        private void ScanForCharacterAttack()
        {
			
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (_equippedWeapon == null) return;
				
				if (_equippedWeapon is RangeWeapon)
                {
                    GetComponent<Player>().animOC[DEFAULT_ATTACK] = _equippedWeapon.GetAnimation();
                    _equippedWeapon.UseWeapon();
                    _anim.Play(ANIMATION_STATE_ATTACK);
                    PlayEquippedWeaponGunShotSound();
                }
                else if (_equippedWeapon is MeleeWeapon)
				{
					print("Do a melee attack'");
				}
			}
        }

        private void PlayEquippedWeaponGunShotSound()
        {
            var audioClip = (_equippedWeapon as RangeWeapon).GetShotAudioClip();
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }

        private void AddEquippedWeaponToWeaponGrip()
		{
			if (_equippedWeapon == null) return;

			var weaponPrefab = _equippedWeapon.GetItemPrefab();
			var weaponObj = Instantiate(weaponPrefab) as GameObject;
			weaponObj.transform.SetParent(_equippedWeaponGrip.transform);
			weaponObj.transform.localPosition = _equippedWeapon.weaponGripTransform.position;
			weaponObj.transform.localRotation = _equippedWeapon.weaponGripTransform.rotation;
		}

		private void DestroyItemsInWeaponGrip()
		{
			_equippedWeaponGrip = GetComponentInChildren<ItemGrip>();
			Assert.IsNotNull(_equippedWeaponGrip, "You must attach an item grip component to the child of the gripping game object on the character. " + this.name);

			foreach (Transform child in _equippedWeaponGrip.transform)
			{
				DestroyImmediate(child.gameObject);
			}
		}
	}
}

