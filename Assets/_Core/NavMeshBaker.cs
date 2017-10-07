using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace Game.Core{

	public class NavMeshBaker : MonoBehaviour {

		NavMeshSurface _surface;
		
		void Start(){
			_surface = GetComponent<NavMeshSurface>();
			Assert.IsNotNull(_surface);
			_surface.BuildNavMesh();
		}
	}
}


