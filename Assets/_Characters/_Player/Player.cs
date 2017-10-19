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
		public float pickupDistance{get{return _pickupDistance;}}
        CameraRaycaster _cameraRaycaster;

		void Awake()
		{
			AddAnimatorComponent();
			AddRigidBodyComponent();
			AddCapsuleCollider();
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
    }
}

