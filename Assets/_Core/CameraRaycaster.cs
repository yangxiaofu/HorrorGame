using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Characters;

namespace Game.Core{
	public class CameraRaycaster : MonoBehaviour {

		float _rayCastDistance = 50f;
		const string INTERACTABLE_ITEM = "Interactable Item";
		const string ENEMY_LAYER = "Enemy";
		const int INTERACTABLE_ITEM_BIT = 8;
		const int ENEMY_BIT = 9;

		public delegate void MouseOverEnemy(Enemy enemy);
		public event MouseOverEnemy OnMouseOverEnemy;

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
				var enemy = hit.transform.gameObject.GetComponent<Enemy>();
				Assert.IsNotNull(enemy, "The game object that you are click on may not have an enemy script on top of it");
				OnMouseOverEnemy(enemy);
			}
        }

        private void RaycastForInteractableItem(RaycastHit hit){
			if (hit.transform.gameObject.layer == LayerMask.NameToLayer(INTERACTABLE_ITEM)){
				print("Hit an interactable item");
			}
		}
	}

}
