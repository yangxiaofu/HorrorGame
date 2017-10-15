using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;
using Game.Characters;
using Game.Items;

namespace Game.Environment{
	public class Door : InteractableItem, IPointerClickHandler{
		
		[Header("Box Collider")]
		[SerializeField] Vector3 _boxColliderSize;
		void Start()
        {
			PerformInteractableObjectAssertions();
			SetupInteractableItemVariables();
			SetupDoorVariables();		
		}

        void Update()
        {
			if (_doorLock.isLocked == false && PlayerIsFarAwayFromDoor())
			{
				CloseDoor();
			}
		}

        private void SetupDoorVariables()
        {
            var boxCollider = gameObject.AddComponent<BoxCollider>();
			boxCollider.size = _boxColliderSize;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (IsWithinInteractionRangeOfPlayer()){
                PerformDoorInteraction();
            } else {
				print("You are too far away from " + name);
			}
            
        }

        void OnDrawGizmos()
        {
            DrawGizmos();
        }

    }

}
