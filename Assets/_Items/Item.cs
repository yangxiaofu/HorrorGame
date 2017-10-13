using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Game.Characters;
using Game.Core;


namespace Game.Items{
    public class Item : MonoBehaviour, IPointerClickHandler
    {
		Player _player;

		void Start()
		{
			_player = FindObjectOfType<Player>();

		}
        public void OnPointerClick(PointerEventData eventData)
        {
			//Determine distance form Player.
			var distanceFromPlayer = Vector3.Distance(this.transform.position, _player.transform.position);

			if (distanceFromPlayer <= _player.pickupDistance)
			{	

				_player.GetComponent<Inventory>().AddItem(this);
				Destroy(this.gameObject);
				
			} else {
				print("The player is too far from the itme to pick up.");
			}

            
        }
    }
}

