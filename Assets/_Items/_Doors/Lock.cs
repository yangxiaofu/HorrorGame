using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	[System.Serializable]
	public class Lock {
		[SerializeField] string _keyCode;
		public string passCode{get{return _keyCode;}}
		[SerializeField] bool _locked = false;

		public bool isLocked{get{return _locked;}}
		public Lock(string keyCode, bool locked){
			_keyCode = keyCode;
			_locked = locked;
		}

		public void UnlockDoor(string keyCode){
			if (_keyCode == keyCode){
				_locked = false;
			}
		}

		public void LockDoor(string keyCode){
			if (_keyCode == keyCode){
				_locked = true;
			}
		}
	}

}
