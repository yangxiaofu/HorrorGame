using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	public class CameraRaycaster : MonoBehaviour {

		float _rayCastDistance = 50f;
		const string INTERACTABLE_ITEM = "Interactable Item";
		const int INTERACTABLE_ITEM_BIT = 8;

		/// <summary>
		/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
		/// </summary>
		void FixedUpdate()
		{
			RaycastHit hit; 
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, _rayCastDistance, (1<<INTERACTABLE_ITEM_BIT))){
				RaycastForInteractableItem(hit);
			}
		}

		private void RaycastForInteractableItem(RaycastHit hit){
			if (hit.transform.gameObject.layer == LayerMask.NameToLayer(INTERACTABLE_ITEM)){
				print("Hit an interactable item");
			}
		}
	}

}
