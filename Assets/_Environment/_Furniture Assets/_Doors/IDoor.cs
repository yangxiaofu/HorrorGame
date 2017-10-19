using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Environment{
	public interface IDoor {
		Vector3 GetForwardDirectionOfDoor();
		Vector3 GetPosition();
	}

}
