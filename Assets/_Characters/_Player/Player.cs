using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Core;
using Game.Items;
using System;

namespace Game.Characters{
	[SelectionBase]
	public class Player : Character, IPlayer{
		[SerializeField] float _pickupDistance = 2f;
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
			var health = GetComponent<PlayerHealth>();
			health.Reset();
			GetComponent<PlayerControl>().Reset();
			_isDead = false;

			var startPoint = GameObject.FindObjectOfType<StartPoint>();
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

