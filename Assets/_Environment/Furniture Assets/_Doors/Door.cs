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
		
		void Start()
        {
			PerformInteractableObjectAssertions();
			SetupInteractableItemVariables();
		}

        void Update()
        {
			if (_doorLock.isLocked == false && PlayerIsFarAwayFromDoor())
			{
				CloseDoor();
			}
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
