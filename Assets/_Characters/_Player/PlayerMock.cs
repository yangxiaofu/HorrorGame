using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters{
	public class PlayerMock : IPlayer {
		Vector3 _position;
		public PlayerMock(Vector3 position)
		{
			_position = position;
		}

        public Vector3 GetPosition()
        {
            return _position;
        }
    }
}

