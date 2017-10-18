using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	[System.Serializable]
	public abstract class Lock {
		[SerializeField] protected string _passCode;
		public string passCode{get{return _passCode;}}
		public abstract void UnlockDoor(string keyCode);
		public abstract void LockDoor(string keyCode);
	}

}
