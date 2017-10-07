using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	public class CameraRaycaster : MonoBehaviour {

		float _rayCastDistance = 50f;
		const string INTERACTABLE_ITEM = "Interactable Item";
		const string ENEMY_LAYER = "Enemy";
		const int INTERACTABLE_ITEM_BIT = 8;
		const int ENEMY_BIT = 9;

		/// <summary>
		/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
		/// </summary>
		void FixedUpdate()
		{
			RaycastHit hit; 
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, _rayCastDistance, (1<<INTERACTABLE_ITEM_BIT|1<<ENEMY_BIT))){
				RaycastForInteractableItem(hit);
				RaycastForEnemy(hit);
			}
		}

        private void RaycastForEnemy(RaycastHit hit)
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer(ENEMY_LAYER)){
				print("Hit an enemy");
			}
        }

        private void RaycastForInteractableItem(RaycastHit hit){
			if (hit.transform.gameObject.layer == LayerMask.NameToLayer(INTERACTABLE_ITEM)){
				print("Hit an interactable item");
			}
		}
	}

}
