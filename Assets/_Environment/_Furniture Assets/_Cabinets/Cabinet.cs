using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using Game.Items;
using Game.Core;
using System;

namespace Game.Environment{
	public class Cabinet : InteractableItem, IPointerClickHandler {

		[Header("Cabinet Specific")]
		[SerializeField] ItemConfig _itemInCabinet;
		[SerializeField] CabinetLock _doorLock;
		
		private bool cabinetContainsItem
		{
			get{ return _itemInCabinet != null;}
		}

		void Start()
		{
			PerformInteractableObjectAssertions();	
			SetupInteractableItemVariables();

			if (_itemInCabinet){
				PlaceItemInCabinet();
			}
		}

        private void PlaceItemInCabinet()
        {
			var itemPlacementParent = GetComponentInChildren<ItemPlacementParent>();

			Assert.IsNotNull(
				itemPlacementParent, 
				"You do not have an itemPlacementParent on a gameobject where the item will be.  Add this item Placement parent to the parent of where the item will be placed."
			);

			var itemPrefab = _itemInCabinet.GetItemPrefab();
			var itemObject = Instantiate(itemPrefab) as GameObject;			
			itemObject.transform.SetParent(itemPlacementParent.transform);
			itemObject.transform.localPosition = Vector3.zero;	
        }

        public void OnPointerClick(PointerEventData eventData)
        {
			if (IsWithinInteractionRangeOfPlayer())
            {
				
                PerformDoorInteraction();
				float _addItemDelay = 2f; //TODO: Consider making this changeable for designer.  Or on click on the actual item.
                ScanForItemInCabinet(_addItemDelay);
				DestroyItemInCabinet();
            }
            else {
				//TODO: Do some type of UI if you too far away from the player.
				print("You are too far away from " + name);
			}
        }

		protected void Unlock(string keyCode)
		{
			_doorLock.UnlockDoor(keyCode);
		}

        private void ScanForItemInCabinet(float delay)
        {
            if (cabinetContainsItem)
            {
				StartCoroutine(AddItem(delay)); 
            } else {
				print("Does not contain an itme");
			}
        }

		private IEnumerator AddItem(float delay)
        {
            yield return new WaitForSeconds(delay);
            _itemInCabinet.AddToInventory(_player.GetComponent<Inventory>());
            yield return null;
        }

        private void DestroyItemInCabinet()
        {
            var itemPlacementParent = GetComponentInChildren<ItemPlacementParent>();
            foreach (Transform child in itemPlacementParent.transform)
            {
                Destroy(child.gameObject);
            }
        }

        void OnDrawGizmos()
		{
			DrawGizmos();			
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
    }

}
