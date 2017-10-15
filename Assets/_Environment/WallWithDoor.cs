using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Environment{
	public class WallWithDoor : MonoBehaviour {
		[SerializeField] Vector3 _boxCollider1Center;
		[SerializeField] Vector3 _boxCollider1Size;
		[SerializeField] Vector3 _boxCollider2Center;
		[SerializeField] Vector3 _boxCollider2;

		// Use this for initialization
		void Start () {
			var collider1 = gameObject.AddComponent<BoxCollider>();
			collider1.size = _boxCollider1Size;
			collider1.center = _boxCollider1Center;

			var collider2 = gameObject.AddComponent<BoxCollider>();
			collider2.size = _boxCollider2;
			collider2.center = _boxCollider2Center;
		}
		
	
	}

}
