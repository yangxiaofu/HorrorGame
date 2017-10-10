using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Core{
	public class RigidBodyCharacter : MonoBehaviour {
		[SerializeField] float Speed = 5f;
		[SerializeField] float GroundDistance = 0.2f;
		[SerializeField] LayerMask Ground;
		Rigidbody _body;
		Animator _anim;
		Vector3 _inputs = Vector3.zero;
		bool _isGrounded = true;
		Transform _groundChecker;
		CameraRaycaster _cameraRaycaster;
		Flashlight _flashlight;
		const string HORIZONTAL_AXIS = "Horizontal";
		const string VERTICAL_AXIS = "Vertical";
		const string FORWARD_ANIMATION = "Forward";
		void Start(){
			_body = GetComponent<Rigidbody>();
			_groundChecker = GetComponentInChildren<GroundChecker>().transform;
			Assert.IsNotNull(_groundChecker, "You need to add a ground checker in the transform of your character");

			_cameraRaycaster = FindObjectOfType<CameraRaycaster>();
			Assert.IsNotNull(_cameraRaycaster);

			_flashlight = GetComponentInChildren<Flashlight>();

			Assert.IsNotNull(_cameraRaycaster);
			_cameraRaycaster.OnMouseOverGround += ApplyFaceDirectionToPlayer;

			_anim = GetComponent<Animator>();
			Assert.IsNotNull(_anim);
		}

		void Update(){
            CheckIfGrounded();
            ScanForDirectionInputs();
			
        }

        void FixedUpdate(){
			_body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
		}

        private void ApplyFaceDirectionToPlayer(Vector3 mousePosOnGround)
        {
			Vector3 facingDirection = mousePosOnGround - transform.position;
			transform.forward = facingDirection;
			_flashlight.transform.forward = facingDirection;
        }

		private void CheckIfGrounded(){
            _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
        }

        private void ScanForDirectionInputs(){
            _inputs = Vector3.zero;
            _inputs.x = Input.GetAxis(HORIZONTAL_AXIS);
            _inputs.z = Input.GetAxis(VERTICAL_AXIS);

			if (_inputs != Vector3.zero)
			{
				_anim.SetFloat(FORWARD_ANIMATION, Speed);
			} else if (_inputs == Vector3.zero){

				_anim.SetFloat(FORWARD_ANIMATION, 0);
			}
        }
	}
}

