using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{

    public class PlayerControlMock : IPlayerControl
    {
		Vector3 _inputs;
        public Vector3 inputs { 
			get {return _inputs;} 
			set{_inputs = value;}
		}

		public PlayerControlMock(Vector3 inputs){
			_inputs = inputs;
		}
    }

}