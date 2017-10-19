using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Environment{
	public interface IDoor {
		Vector3 GetForwardDirection();
		Vector3 GetPosition();
	}

}
