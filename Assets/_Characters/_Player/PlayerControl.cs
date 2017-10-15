﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Characters;

namespace Game.Core{
	public class PlayerControl : MonoBehaviour {
		[SerializeField] float _forwardSpeed = 3f;
		[SerializeField] float _backwardSpeed = 2f;	
		[SerializeField] float _groundDistance = 0.2f;
		[SerializeField] LayerMask _ground;

		[Tooltip("Adjusting this impacts the minimum energy level that impacts the player movement.")]
		[Range(0, 1)]
		[SerializeField] float _minimumEnergyFactor = 0.1f;
		float _speed = 0f;
		Rigidbody _body;
		Animator _anim;
		Vector3 _inputs = Vector3.zero;
		bool _isGrounded = true;
		Transform _groundChecker;
		float _angleFromSightPosition;
		CameraRaycaster _cameraRaycaster;
		Flashlight _flashlight;
		const string HORIZONTAL_AXIS = "Horizontal";
		const string VERTICAL_AXIS = "Vertical";
		const string WALK_FORWARD = "Walk";
		const string WALK_BACKWARD = "Walk_Backward";
		void Start(){
			_body = GetComponent<Rigidbody>();
			Assert.IsNotNull(_body);

			_groundChecker = GetComponentInChildren<GroundChecker>().transform;
			Assert.IsNotNull(_groundChecker, "You need to add a ground checker in the transform of your character");

			_cameraRaycaster = FindObjectOfType<CameraRaycaster>();
			Assert.IsNotNull(_cameraRaycaster);
			_cameraRaycaster.OnMouseOverGround += ApplyFaceDirectionToPlayer;

			_flashlight = GetComponentInChildren<Flashlight>();
			Assert.IsNotNull(_flashlight);

			_anim = GetComponent<Animator>();
			Assert.IsNotNull(_anim);
		}

		void Update(){
            CheckIfGrounded();
			UpdateAngleFromForwardPosition();
            ScanForDirectionInputs();
        }

        private void UpdateAngleFromForwardPosition()
        {
         	var forwardPosition = transform.forward;
			var valueToKeepPlayerOnPlane = 0;
			Vector3 movementDirection = new Vector3(
				_inputs.x, 
				valueToKeepPlayerOnPlane, 
				_inputs.z
			);

			_angleFromSightPosition = Vector3.Angle(
				movementDirection, 
				forwardPosition
			);
        }

        void FixedUpdate(){
			_body.MovePosition(_body.position + _inputs * _speed * Time.fixedDeltaTime);
		}

        private void ApplyFaceDirectionToPlayer(Vector3 mousePosOnGround)
        {
			transform.forward = mousePosOnGround - transform.position;
        }

		private void CheckIfGrounded(){
            _isGrounded = Physics.CheckSphere(
				_groundChecker.position, 
				_groundDistance, 
				_ground, 
				QueryTriggerInteraction.Ignore
			);
        }

        private void ScanForDirectionInputs(){
            _inputs = Vector3.zero;
            _inputs.x = Input.GetAxis(HORIZONTAL_AXIS);
            _inputs.z = Input.GetAxis(VERTICAL_AXIS);

			//TODO: Scan for straffing and walking backwards.  Detect the forward osition and get the engle to determine the animation that it needs. 
			if (_inputs != Vector3.zero)
            {
                float angleOfForwardWalking = 45f;
                float angleofBackwardWalking = 135f;
                float anyNumberAboveZero = 10f;
                float energyFactor = GetEnergyFactor();

                if (_angleFromSightPosition < angleOfForwardWalking)
                {
                    _anim.SetFloat(WALK_FORWARD, anyNumberAboveZero);
                    _speed = _forwardSpeed * energyFactor;
                }
                else if (_angleFromSightPosition > angleofBackwardWalking)
                {
                    _anim.SetFloat(WALK_BACKWARD, anyNumberAboveZero);
                    _speed = _backwardSpeed * energyFactor;
                }
            }
            else if (_inputs == Vector3.zero){
				float stoppingValue = 0f;
				_anim.SetFloat(WALK_FORWARD, stoppingValue);
				_anim.SetFloat(WALK_BACKWARD, stoppingValue);
			}
        }

        private float GetEnergyFactor()
        {
            var energyLevel = GetComponent<PlayerEnergy>();
            var energyFactor = energyLevel.energyAsPercentage;
			
			float maximumEnergyFactor = 1;

			energyFactor = Mathf.Clamp(
				energyFactor, 
				_minimumEnergyFactor, 
				maximumEnergyFactor
			);

            return energyFactor;
        }
    }
}

