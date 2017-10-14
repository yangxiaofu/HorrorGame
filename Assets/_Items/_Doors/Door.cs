using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;
using Game.Characters;
using Game.Items;

namespace Game.Core{
	public class Door : MonoBehaviour, IPointerClickHandler{
		[SerializeField] Lock _doorLock;
		[SerializeField] float _closeDistanceFromPlayer = 2f;

		[Header("Box Collider")]
		[SerializeField] Vector3 _boxColliderSize;

		[Header("Door Animations")]
		[SerializeField] AnimatorOverrideController _animOC;

		const string OPEN_DOOR = "OpenDoor";
		Animator _anim;
		Player _player;
		
		
		void Start(){
			_player = GameObject.FindObjectOfType<Player>();
			Assert.IsNotNull(_player);
			var boxCollider = gameObject.AddComponent<BoxCollider>();
			boxCollider.size = _boxColliderSize;

			_anim = gameObject.AddComponent<Animator>();
			Assert.IsNotNull(_animOC);
			_anim.runtimeAnimatorController = _animOC;
		}

		void Update(){
			if (_doorLock.isLocked == false && PlayerIsFarAwayFromDoor())
			{
				CloseDoor();
			}
		}

		bool PlayerIsFarAwayFromDoor()
		{
			var distanceFromPlayer = Vector3.Distance(_player.transform.position, this.transform.position);
			return distanceFromPlayer >= _closeDistanceFromPlayer;
		}

		public void Unlock(string keyCode)
		{
			_doorLock.UnlockDoor(keyCode);
		}

		public void LockDoor(string keyCode)
		{
			_doorLock.LockDoor(keyCode);
		}

        public void OnPointerClick(PointerEventData eventData)
        {
			
            if (!_doorLock.isLocked)
			{
				//Animate the door open. 
				OpenDoor();
			} else {
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
				} else {

					Unlock(key.passCode);
					OpenDoor();
				}
			}
        }

		private void OpenDoor()
		{
			_anim.SetBool(OPEN_DOOR, true);
			//TODO: Player the door opening sound.
		}

		private void CloseDoor()
		{
			_anim.SetBool(OPEN_DOOR, false);
			//TODO; Opending and closing door sounds. 	
		}

		void OnDrawGizmos(){
			Gizmos.color = Color.grey;
			Gizmos.DrawWireSphere(this.transform.position, _closeDistanceFromPlayer);
		}
    }

}
