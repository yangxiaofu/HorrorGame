using System;	
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	[System.Serializable]
    public class DoorLock : Lock
    {
		[SerializeField] bool _frontLocked = false;
		public bool frontLocked {get{return _frontLocked;}}
		[SerializeField] bool _backLocked = false;
		public bool backLocked {get{return _backLocked;}}
		
		public DoorLock(string passCode, bool frontLocked, bool backLocked)
		{
			_passCode = passCode;
			_frontLocked = frontLocked;
			_backLocked = backLocked;
		}

		public bool isLockedOnBothSides
		{
			get{
				return _frontLocked == true && _backLocked == true;
			}
		}

		public override void UnlockDoor(string passCode)
		{
			if (_passCode == passCode){
				_frontLocked = false;
				_backLocked = false;
			}
		}

		public override void LockDoor(string passCode)
		{
			if (_passCode == passCode){
				_frontLocked = true;
				_backLocked = true;
			}
		}

    }
}

