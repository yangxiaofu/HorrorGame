using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Characters;

namespace Game.Core{
	public class PlayerControl : MonoBehaviour, IPlayerControl {

		[Header("Movement Control")]
		[SerializeField] float _forwardSpeed = 3f;
		public float forwardSpeed{
			get{return _forwardSpeed;}
			set{_forwardSpeed = value;}
		}
		[SerializeField] float _backwardSpeed = 2f;	
		public float backwardSpeed{
			get{return _backwardSpeed;}
			set{_backwardSpeed = value;}
		}
		[SerializeField] float _groundDistance = 0.2f;

		[Tooltip("This is the layer mask for which the camera raycater will hit. ")]
		[SerializeField] LayerMask _ground;

		[Tooltip("Adjusting this impacts the minimum energy level that impacts the player movement.")]
		[Range(0, 1)]
		[SerializeField] float _minimumEnergyFactor = 0.1f;
		public float minimumEnergyFactor{get{return _minimumEnergyFactor;}}
		[Header("Animator Variables")]
		[SerializeField] AnimatorOverrideController _animOC;
		[SerializeField] Avatar _avatar;

		[Header("Capsule Collider")]
		[SerializeField] Vector3 _center = new Vector3(0, 0.8f, 0);
		[SerializeField] float _radius = 0.3f;
		[SerializeField] float _height = 1.6f;
		float _speed = 0f;
		public float speed{
			get{return _speed;}
			set{_speed = value;}
		}
		Rigidbody _body;
		Animator _anim;

		public Animator anim{
			get{return _anim;}
		}
		Vector3 _inputs = Vector3.zero;
		public Vector3 inputs{
			get{return _inputs;}
			set{_inputs = value;}
		}

		bool _isGrounded = true;
		Transform _groundChecker;
		float _angleFromSightPosition;
		public float angleFromSightPosition{
			get{return _angleFromSightPosition;}
		}
        CameraRaycaster _cameraRaycaster;
		Flashlight _flashlight;
		PlayerMovementController _controller;
		
		void Awake()
        {
            AddAnimatorComponent();
			AddRigidBodyComponent();
			AddCapsuleCollider();
        }

        void Start(){

			_groundChecker = GetComponentInChildren<GroundChecker>().transform;
			Assert.IsNotNull(_groundChecker, "You need to add a ground checker in the transform of your character");

			_cameraRaycaster = FindObjectOfType<CameraRaycaster>();
			Assert.IsNotNull(_cameraRaycaster);
			_cameraRaycaster.OnMouseOverGround += ApplyFaceDirectionToPlayer;

			_flashlight = GetComponentInChildren<Flashlight>();
			Assert.IsNotNull(_flashlight);

			_controller = new PlayerMovementController(this);
		}

		void Update()
		{
            CheckIfGrounded();
			UpdateAngleFromForwardPosition();
            ScanForDirectionInputs();
        }

        private void UpdateAngleFromForwardPosition()
        {
         	var forwardDirection = transform.forward;
			var valueToKeepPlayerOnPlane = 0;
			Vector3 movementDirection = new Vector3(
				_inputs.x, 
				valueToKeepPlayerOnPlane, 
				_inputs.z
			);

			var vectorCalculator = new VectorCalculations(
				movementDirection, forwardDirection
			);

			_angleFromSightPosition = vectorCalculator.GetAngle() * vectorCalculator.GetSign();
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
			_controller.ScanForDirectionInput();   
        }

        private void AddRigidBodyComponent()
        {
            _body = gameObject.AddComponent<Rigidbody>();
			_body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
			
        }

        private void AddAnimatorComponent()
        {
            Assert.IsNotNull(
                            _animOC,
                            "Please add the player animation Override Controller referenced in the PlayerControl component"
                        );

            Assert.IsNotNull(
                _avatar,
                "Add the player avatar into PlayerControl component on Player"
            );

            _anim = gameObject.AddComponent<Animator>();
            _anim.runtimeAnimatorController = _animOC;
            _anim.avatar = _avatar;
        }


        private void AddCapsuleCollider()
        {
			var cc = gameObject.AddComponent<CapsuleCollider>();
			cc.center = _center;
			cc.radius = _radius;
			cc.height = _height;
        }

        public PlayerEnergy GetPlayerEnergyComponent()
        {
			return GetComponent<PlayerEnergy>();
        }
    }
}

