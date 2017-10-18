using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using Game.Characters;
using Game.Items;

namespace Game.Environment{
	public abstract class InteractableItem : MonoBehaviour
	{
		[Header("Interactable Item Parameters")]
		[SerializeField] protected float _closeDistanceFromPlayer = 2f;
		[SerializeField] float _maxInteractionDistanceFromPlayer = 2f;
		[SerializeField] protected AnimatorOverrideController _animOC;
		protected Animator _anim;
		protected const string OPEN_DOOR = "OpenDoor";
		protected Player _player;
		private const string INTERACTABLE_LAYER = "Interactable Item";
		protected void SetupInteractableItemVariables()
		{
			_player = GameObject.FindObjectOfType<Player>();
			Assert.IsNotNull(_player);

			_anim = gameObject.AddComponent<Animator>();
			Assert.IsNotNull(_anim);
			_anim.runtimeAnimatorController = _animOC;
		}

		protected bool IsWithinInteractionRangeOfPlayer()
		{
			float distanceFromPlayer = Vector3.Distance(this.transform.position, _player.transform.position);
			return (distanceFromPlayer <= _maxInteractionDistanceFromPlayer);
		}
		
		protected bool PlayerIsFarAwayFromDoor()
		{
			var distanceFromPlayer = Vector3.Distance(_player.transform.position, this.transform.position);
			return distanceFromPlayer >= _closeDistanceFromPlayer;
		}

		

		protected void OpenDoor()
		{
			_anim.SetBool(OPEN_DOOR, true);
			//TODO: Player the door opening sound.
		}

		protected void CloseDoor()
		{
			_anim.SetBool(OPEN_DOOR, false);
			//TODO; Opending and closing door sounds. 	
		}

		protected void PerformInteractableObjectAssertions()
        {
			
            Assert.IsTrue(this.gameObject.layer == LayerMask.NameToLayer(INTERACTABLE_LAYER),
				"Don't forget to set the tag of this game object as an interactable item"
			);
        }

        protected void DrawGizmos()
        {
            Gizmos.color = Color.grey;
            Gizmos.DrawWireSphere(this.transform.position, _closeDistanceFromPlayer);
        }
		protected abstract void PerformDoorInteraction();
    }
}

