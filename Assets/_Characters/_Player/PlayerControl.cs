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
		[SerializeField] float _backwardSpeed = 2f;	
		[SerializeField] float _groundDistance = 0.2f;

		[Tooltip("This is the layer mask for which the camera raycater will hit. ")]
		[SerializeField] LayerMask _ground;

		[Tooltip("Adjusting this impacts the minimum energy level that impacts the player movement.")]
		[Range(0, 1)]
		[SerializeField] float _minimumEnergyFactor = 0.1f;

		[Header("Animator Variables")]
		[SerializeField] AnimatorOverrideController _animOC;
		[SerializeField] Avatar _avatar;

		[Header("Capsule Collider")]
		[SerializeField] Vector3 _center = new Vector3(0, 0.8f, 0);
		[SerializeField] float _radius = 0.3f;
		[SerializeField] float _height = 1.6f;

		[Header("Movement Angles")]
		[SerializeField] float _angleOfForwardWalking = 45f;
        [SerializeField] float _angleOfBackwardWalking = 135f;
		float _speed = 0f;
		Rigidbody _body;
		Animator _anim;
		Vector3 _inputs = Vector3.zero;
		public Vector3 inputs{
			get{return _inputs;}
			set{_inputs = value;}
		}

		bool _isGrounded = true;
		Transform _groundChecker;
        CameraRaycaster _cameraRaycaster;
		Flashlight _flashlight;
		PlayerMovementController _controller;

		const string WALK_FORWARD = "Walk_Forward";
		const string WALK_BACKWARD = "Walk_Backward";
		const string HORIZONTAL_AXIS = "Horizontal";
		const string VERTICAL_AXIS = "Vertical";
		
		void Awake()
        {
            AddAnimatorComponent();
			AddRigidBodyComponent();
			AddCapsuleCollider();
        }

        void Start()
        {
            SetupComponentVariables();
            RegisterToNotifiers();
            SetupPlayerMovementController();
        }

        private void SetupPlayerMovementController()
        {
            _controller = new PlayerMovementController(this,
				new WalkAngleRestrictionArgs(
					_angleOfForwardWalking,
					_angleOfBackwardWalking
				)
			);
        }

        private void SetupComponentVariables()
        {
            _groundChecker = GetComponentInChildren<GroundChecker>().transform;

            Assert.IsNotNull(
                _groundChecker,
                "You need to add a ground checker in the transform of your character"
            );

            _flashlight = GetComponentInChildren<Flashlight>();
            Assert.IsNotNull(_flashlight, "You must carry a flashlight as a child of the player game object.");
            //TODO: Set the location of the flashlight. 
        }

        private void RegisterToNotifiers()
        {
            _cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            Assert.IsNotNull(_cameraRaycaster);
            _cameraRaycaster.OnMouseOverGround += ApplyFaceDirectionToPlayer;
        }

        void Update()
        {
            CheckIfGrounded();
            UpdateControllerInput();
            _controller.UpdateMovementDirection(GetAngleFromSightPosition());
            UpdateMovementAnimation();
        }

        private void UpdateControllerInput()
        {
            _inputs = new Vector3(Input.GetAxis(HORIZONTAL_AXIS), 0, Input.GetAxis(VERTICAL_AXIS));
        }

        void FixedUpdate()
		{
			_body.MovePosition(_body.position + _inputs * _speed * Time.fixedDeltaTime);
		}

        private void UpdateMovementAnimation()
        {
            float anyNumberAboveZero = 10f;
            if (_controller.movementDirection == PlayerMovementController.MovementDirection.FORWARD)
            {
                _anim.SetFloat(WALK_FORWARD, anyNumberAboveZero);
                _speed = _forwardSpeed * GetEnergyFactor();
                _anim.speed = _speed / 5; //Walk 
            }
            else if (_controller.movementDirection == PlayerMovementController.MovementDirection.BACKWARD)
            {
                _anim.SetFloat(WALK_BACKWARD, anyNumberAboveZero);
                _speed = _backwardSpeed * GetEnergyFactor();
            }
            else if (_controller.movementDirection == PlayerMovementController.MovementDirection.IDLE)
            {
                float stoppingValue = 0f;
                _anim.SetFloat(WALK_FORWARD, stoppingValue);
                _anim.SetFloat(WALK_BACKWARD, stoppingValue);
                _anim.speed = 1;
            } else if (_controller.movementDirection == PlayerMovementController.MovementDirection.RIGHT){
				//TODO: Strafe Right Animations.
                Debug.LogWarning("Need to write the strafe Right animation");
			} else if (_controller.movementDirection == PlayerMovementController.MovementDirection.LEFT){
                Debug.LogWarning("Need to write the strafe left animation");
			} else {
				Debug.LogError("You need to create the movement direction in the PlayerMovementController.");
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

        private float GetAngleFromSightPosition()
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
			return vectorCalculator.GetAngle() * vectorCalculator.GetSign();
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
    }
}

