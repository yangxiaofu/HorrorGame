using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Environment{
	public class WallWithDoor : MonoBehaviour {
		[SerializeField] Vector3 _boxCollider1Center = new Vector3(-0.03397616f, 0.0149f, -0.000508f);
		[SerializeField] Vector3 _boxCollider1Size = new Vector3(0.0097665f, 0.03f, 0.0026588f);
		[SerializeField] Vector3 _boxCollider2Center = new Vector3(-0.006684258f, 0.0149f, -0.000508f);

		// Use this for initialization
		void Start () {
			var collider1 = gameObject.AddComponent<BoxCollider>();
			collider1.size = _boxCollider1Size;
			collider1.center = _boxCollider1Center;

			var collider2 = gameObject.AddComponent<BoxCollider>();
			collider2.size = _boxCollider1Size;
			collider2.center = _boxCollider2Center;
		}
		
	
	}

}
