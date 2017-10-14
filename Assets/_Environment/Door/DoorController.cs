using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
	[System.Serializable]
	public class DoorController {
		Door _door;

		public void SetDoor(Door door)
		{
			_door = door;
		}

		
	}
}

