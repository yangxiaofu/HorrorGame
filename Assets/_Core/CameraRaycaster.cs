﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Characters;
using Game.Environment;

namespace Game.Core{
	public class CameraRaycaster : MonoBehaviour {
		float _rayCastDistance = 50f;
		const string ENEMY_LAYER = "Enemy";
		const  int INTERACTABLE_ITEM_BIT = 8;
		const int ENEMY_BIT = 9;
		const int GROUND_BIT = 10;

		Vector3 _mousePosition;
		public Vector3 mousePosition {get{return _mousePosition;}}

		public delegate void MouseOverEnemy(Enemy enemy);
		public event MouseOverEnemy OnMouseOverEnemy;

		public delegate void MouseOverGround(Vector3 mousePositionOnGround);
		public event MouseOverGround OnMouseOverGround;

		void FixedUpdate()
		{
			RaycastHit hit; 
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(ray, out hit, _rayCastDistance, (1<<INTERACTABLE_ITEM_BIT|1<<ENEMY_BIT|1<<GROUND_BIT)))
            {
                RaycastForEnemy(hit);
                RaycastForGround(hit);
                UpdateMousePosition(hit);
            }
        }

        private void UpdateMousePosition(RaycastHit hit)
        {
            _mousePosition = hit.point;
        }

        private void RaycastForGround(RaycastHit hit)
        {
			var groundPosition = new Vector3(hit.point.x, 0, hit.point.z);
            OnMouseOverGround(groundPosition);
        }

        private void RaycastForEnemy(RaycastHit hit)
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer(ENEMY_LAYER))
			{
				var enemy = hit.transform.gameObject.GetComponent<Enemy>();
				Assert.IsNotNull(enemy, "The game object that you are click on may not have an enemy script on top of it. " + hit.transform.gameObject.name);
				OnMouseOverEnemy(enemy);
			}
        }
	}

}
