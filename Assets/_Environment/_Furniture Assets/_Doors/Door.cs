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

        protected void Unlock(string keyCode)
		{
			_doorLock.UnlockDoor(keyCode);
		}

        void Update()
        {
			if (_doorLock.isLocked == false && PlayerIsFarAwayFromDoor())
			{
                _boxCollider.enabled = true;
				CloseDoor();
			}
		}

        public void DetectPlayer(){

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

        void OnDrawGizmos()
        {
            DrawGizmos();

            Gizmos.color = Color.black;
            Gizmos.DrawRay(this.transform.position, this.transform.right * 100f);
        }

        protected override void PerformDoorInteraction()
        {
             if (!_doorLock.isLocked)
            {
                //Animate the door open. 
				
                OpenDoor();
            }
            else
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
        }

        public Vector3 GetForwardDirection()
        {
            return this.transform.forward;
        }

        public Vector3 GetPosition()
        {
            return this.transform.position;
        }
    }

}
