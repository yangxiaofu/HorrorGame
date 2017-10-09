using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using Game.Characters;

namespace Game.Environment{
	public class InteractableItem : MonoBehaviour, IPointerDownHandler{
		[SerializeField] float _maxDistanceFromPlayerForInteraction = 2f;

		Player _player;
		Animator _anim;
		const string OPEN = "Open";
		void Start(){
			_anim = GetComponent<Animator>();
			Assert.IsNotNull(_anim);

			_player = FindObjectOfType<Player>();
			Assert.IsNotNull(_player);
		}

        public void OnPointerDown(PointerEventData eventData)
        {
			var distanceFromPlayer = Vector3.Distance(this.transform.position, _player.transform.position);

			if (distanceFromPlayer < _maxDistanceFromPlayerForInteraction)
			{
				_anim.SetTrigger(OPEN);
			} else {
				print("You are too far away from the item");
			}
        }

		void OnDrawGizmos(){
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(transform.position, _maxDistanceFromPlayerForInteraction);
		}
    }
}

