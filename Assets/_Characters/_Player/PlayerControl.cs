using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Characters;
using Game.Items;

namespace Game.Core{

	public class PlayerControl : CharacterControl{

		[Header("Player Specific")]
		[SerializeField] float _backwardSpeed = 2f;
        [SerializeField] float _strafeSpeed = 1f;	

		[Tooltip("Adjusting this impacts the minimum energy level that impacts the player movement.")]
		[Range(0, 1)]
		[SerializeField] float _minimumEnergyFactor = 0.1f;

		[Header("Movement Angles")]
        [Tooltip("The angle from the sight view from the front from where the animation changes from a walking animation to a strafing animation.")]

		[SerializeField] float _angleOfForwardWalking = 45f;
        [Tooltip("The angle from the sight view at the front where the animation changes from strafing to a backward animation.")]
        [SerializeField] float _angleOfBackwardWalking = 110f;

		Transform _groundChecker;
        CameraRaycaster _cameraRaycaster;
		Flashlight _flashlight;
		PlayerMovementController _controller;

        public delegate void HealthKeyDown();
        public event HealthKeyDown OnHealthKeyDown;
        
		
		void Awake()
        {
            AddAnimatorComponent();
			AddRigidBodyComponent();
			AddCapsuleCollider();
        }

        void Start()
        {
            SetupComponentVariables(); //Player specific
            RegisterToNotifiers();  //Player Specific
            SetupPlayerMovementController(); //Player Specific
        }

        private void SetupPlayerMovementController()//Player Specific
        {
            _controller = new PlayerMovementController(this,
				new WalkAngleRestrictionArgs(
					_angleOfForwardWalking,
					_angleOfBackwardWalking
				)
			);
        }

        private void SetupComponentVariables()//Player specific
        {
            _groundChecker = GetComponentInChildren<GroundChecker>().transform;

            Assert.IsNotNull(
                _groundChecker,
                "You need to add a ground checker in the transform of your character"
            );

            _flashlight = GetComponentInChildren<Flashlight>();

            Assert.IsNotNull(
                _flashlight, 
                "You must carry a flashlight as a child of the player game object."
            );
        }

        private void RegisterToNotifiers()//Player Specific
        {
            _cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            Assert.IsNotNull(_cameraRaycaster);
            _cameraRaycaster.OnMouseOverGround += ApplyFaceDirectionToPlayer;
        }

        void Update()
        {
            if ((GetComponent(typeof(Character)) as Character).isDead)
            {
                return;
            }

            UpdateControllerInput();
            _controller.UpdateMovementDirection(GetAngleFromSightPosition());
            UpdateMovementAnimation();//Player Specific
            ScanKeyButtonPresses();//Player Specific
        }

        public void Reset()
        {
            _anim.Play("Idle");
        }

        private void ScanKeyButtonPresses()//Player Specific
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (OnHealthKeyDown != null) 
                    OnHealthKeyDown();
            }
        }
        private void UpdateControllerInput()//Player Specific
        {
            const string HORIZONTAL_AXIS = "Horizontal";
		    const string VERTICAL_AXIS = "Vertical";

            _inputs = new Vector3(Input.GetAxis(HORIZONTAL_AXIS), 0, Input.GetAxis(VERTICAL_AXIS));
        }
        void FixedUpdate()
        {
            MoveBodyPosition(); //CharacterControl
        }
        void OnAnimatorIK(int layerIndex)//Player Specific
        {
            _anim.SetIKPosition(AvatarIKGoal.RightHand, _cameraRaycaster.mousePosition);
            _anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);    
        }

        private void UpdateMovementAnimation()//Player Specific
        {
            if (_controller.movementDirection == PlayerMovementController.MovementDirection.FORWARD && _animationState != AnimationState.FORWARD)
            {
                _anim.SetBool(IS_IDLE, false);
                _animationState = AnimationState.FORWARD;
                _anim.Play(ANIMATION_STATE_FORWARD);
                _speed = _forwardSpeed * GetEnergyFactor();
            }
            else if (_controller.movementDirection == PlayerMovementController.MovementDirection.BACKWARD && _animationState != AnimationState.BACKWARD)
            {
                _anim.SetBool(IS_IDLE, false);
                _animationState = AnimationState.BACKWARD;
                _anim.Play(ANIMATION_STATE_BACKWARD);
                _speed = _backwardSpeed;
            }
            else if (_controller.movementDirection == PlayerMovementController.MovementDirection.IDLE && _animationState != AnimationState.IDLE)
            {
                SetIdleAnimation();
            } 
            else if (_controller.movementDirection == PlayerMovementController.MovementDirection.RIGHT && _animationState != AnimationState.RIGHT)
            {
                _anim.SetBool(IS_IDLE, false);
                _animationState = AnimationState.RIGHT;
                _anim.Play(ANIMATION_STATE_STRAFE_RIGHT);
                _speed = _strafeSpeed;
			} 
            else if (_controller.movementDirection == PlayerMovementController.MovementDirection.LEFT && _animationState != AnimationState.LEFT)
            {
                _anim.SetBool(IS_IDLE, false);
                _anim.Play(ANIMATION_STATE_STRAFE_LEFT);
                _animationState = AnimationState.LEFT;
                _speed = _strafeSpeed;
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
    }
}