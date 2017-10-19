using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;
using Game.Characters;
using Game.Items;
using Game.Core;

namespace Game.Environment{
    [SelectionBase]
	public class Door : InteractableItem, IPointerClickHandler, IDoor{
		[SerializeField]  DoorLock _doorLock;
        BoxCollider _boxCollider;
        Vector3 _frontOfDoor
        {
            get{return transform.right;}
        }

		void Start()
        {
			PerformInteractableObjectAssertions();
			SetupInteractableItemVariables();

            _boxCollider = GetComponentInChildren<BoxCollider>();

            _player = FindObjectOfType<Player>();
            Assert.IsNotNull(_player, "You are missing the player from the game scene.");

		}

        void Update()
        {
			if (_doorLock.isLockedOnBothSides == false && PlayerIsFarAwayFromDoor())
			{
                _boxCollider.enabled = true;
				CloseDoor();
			}
        }

        private void Unlock(string keyCode)
		{
			_doorLock.UnlockDoor(keyCode);
		}

        public void OnPointerClick(PointerEventData eventData)
        {
            if (IsWithinInteractionRangeOfPlayer())
            {
                _boxCollider.enabled = false;
                PerformDoorInteraction();
            } else {
				print("You are too far away from " + name);
			}   
        }

        private DoorDetection.DoorSide GetPlayerSideOn(){
            var dd = new DoorDetection(this, _player);
            return dd.PlayerSideOn() == DoorDetection.DoorSide.Front ? DoorDetection.DoorSide.Front : DoorDetection.DoorSide.Back;
        }

        protected override void PerformDoorInteraction() //TODO: Unit Testing for this should occur. 
        {
            if (_doorLock.isLockedOnBothSides)
            { 
                AttemptUnlock();
            }
            else 
            {   
                if (GetPlayerSideOn()== DoorDetection.DoorSide.Front && !_doorLock.frontLocked)
                {
                    OpenDoor();
                } 
                else if (GetPlayerSideOn() == DoorDetection.DoorSide.Back && !_doorLock.backLocked)
                {
                    OpenDoor();
                } 
                else 
                {
                    AttemptUnlock();
                }
            }
        }

        private void AttemptUnlock()
        {
            var inventory = _player.GetComponent<Inventory>();

            Assert.IsNotNull(
                inventory,
                "You need to attach an inventory component to the player."
            );

            var key = inventory.FindKey(_doorLock.passCode);

            if (key == null)
            {
                print("You do not have the keys to this door.");
                //Player the door locking sound.
                //TODO: Do some type of UI that tells the player that you do not have the keys. 
            }
            else
            {
                Unlock(key.passCode);
                OpenDoor();
            }
        }

        public Vector3 GetForwardDirectionOfDoor()
        {
            return this.transform.right;
        }

        public Vector3 GetPosition()
        {
            return this.transform.position;
        }

        void OnDrawGizmos()
        {
            DrawGizmos();
            //Black Ray is the forward direction of the door. 
            Gizmos.color = Color.black;
            Gizmos.DrawRay(this.transform.position, this.transform.right * 100f); 
            
        }
    }

}
