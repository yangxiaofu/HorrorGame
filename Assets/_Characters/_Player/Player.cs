using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Core;
using Game.Items;
using System;

namespace Game.Characters{
	[SelectionBase]
    [RequireComponent(typeof(PlayerControl))]
    [RequireComponent(typeof(PlayerHealth))]
    [RequireComponent(typeof(PlayerEnergy))]
    
	public class Player : Character, IPlayer{
		[SerializeField] float _pickupDistance = 2f;

        [Header("Player Audio")]
		[SerializeField] AudioClip _footstepsAudio;
		public float pickupDistance{get{return _pickupDistance;}}
        CameraRaycaster _cameraRaycaster;
		AudioSource _audioSource;
		
		void Awake()
        {
            AddAnimatorComponent();
            AddRigidBodyComponent();
            AddCapsuleCollider();
            AddAudioSource();
        }

        void Start()
		{
			_cameraRaycaster = FindObjectOfType<CameraRaycaster>();

			Assert.IsNotNull(
				_cameraRaycaster, 
				"Camera Raycaster is not available."
			);
		}

        public override void ResetCharacter()
        {
            _isDead = false;
            GetComponent<PlayerHealth>().Reset();
            GetComponent<PlayerControl>().Reset();
            ResetToSpawnPoint();            
        }

        private void ResetToSpawnPoint()
        {
            var startPoint = GameObject.FindObjectOfType<StartPoint>();
            Assert.IsNotNull(
                startPoint,
                "You did not add a starting point to the game scene.  If the player dies this is where he will spawn."
            );

            this.transform.position = startPoint.transform.position;
        }

        public Vector3 GetPosition()
        {
            return this.transform.position;
        }

		private void AddAudioSource()
        {
            _audioSource = this.gameObject.AddComponent<AudioSource>();
            _audioSource.loop = false;
            _audioSource.playOnAwake = false;
        }

    }
}

