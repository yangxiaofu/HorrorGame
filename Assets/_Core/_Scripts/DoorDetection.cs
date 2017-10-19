using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters;
using Game.Environment;

namespace Game.Core{
	public class DoorDetection {

		private readonly IDoor _door;
		private readonly IPlayer _player;

		public enum DoorSide{
			Front, Back
		}

		DoorSide _doorSide;

		public DoorDetection(IDoor door, IPlayer player)
		{
			_door = door;
			_player = player;
		}
		
		public DoorSide PlayerSideOn()
		{
			_doorSide = DoorSide.Front;
			Vector3 forward = _door.GetForwardDirection();
			Vector3 toOther = _player.GetPosition() - _door.GetPosition();
			
			if (Vector3.Dot(forward, toOther) < 0){
				return DoorDetection.DoorSide.Back;
			} else {
				return DoorDetection.DoorSide.Front;
			}
		}

	}

}
