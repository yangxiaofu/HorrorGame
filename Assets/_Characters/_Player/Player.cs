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
		public float pickupDistance{get{return _pickupDistance;}}
        CameraRaycaster _cameraRaycaster;
        PlayerControl _playerControl;
        Flashlight _flashlight;
        public Flashlight flashlight{get{return _flashlight;}}

        public delegate void EnergyKeyDown(float energyToIncrease);
        public event EnergyKeyDown OnEnergyKeyDown;
		
		void Awake()
        {
            AddAnimatorComponent();
            AddRigidBodyComponent();
            AddCapsuleCollider();
            AddAudioSource();
            
            _flashlight = GetComponentInChildren<Flashlight>(); 
            Assert.IsNotNull(_flashlight, "There are not flashlights in the child of your player");
            //Needs to be early so that enemies can register to it in their start methods.
        }

        void Start()
		{
			_cameraRaycaster = FindObjectOfType<CameraRaycaster>();

			Assert.IsNotNull(
				_cameraRaycaster, 
				"Camera Raycaster is not available."
			);

            _playerControl = GetComponent<PlayerControl>();
		}

        void Update()
        {
            if (PlayerIsDead()) return;

            _playerControl.UpdateMovementDirection();
            ScanForFoodButtonPress();
            ScanForFlashLightPress();
        }

        private void ScanForFlashLightPress()
        {
            if (Input.GetMouseButtonDown(1))
            {
                var isOn = _flashlight.isOn ? false : true;
                _flashlight.ToggleFlashlight(isOn);
            }
        }

        private void ScanForFoodButtonPress()//Player Specific
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (OnEnergyKeyDown != null) 
                {
                    var food = GetComponent<Inventory>().GetFood();

                    if (food != null)
                    {
                        OnEnergyKeyDown(food.energyBoost);
                    }
                }
            }
        }

         private bool PlayerIsDead()
        {
            if (_isDead){
                GetComponent<PlayerControl>().speed = 0;
                return true;
            } else {
                return false;
            }
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



    }
}

