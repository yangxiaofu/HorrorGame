using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Environment{
	public class DoorMock : IDoor {
		
		Vector3 _position;
		Vector3 _forwardDirection;
		
		public DoorMock(Vector3 position, Vector3 forwardDirection)
		{
			_position = position;
			_forwardDirection = forwardDirection;
		}

        public Vector3 GetForwardDirectionOfDoor()
        {
            return _forwardDirection;
        }

        public Vector3 GetPosition()
        {
            return _position;
        }
    }
}

