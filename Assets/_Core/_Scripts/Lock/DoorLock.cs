using System;	
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	[System.Serializable]
    public class DoorLock : Lock
    {
		[SerializeField] bool _frontLocked = false;
		[SerializeField] bool _backLocked = false;

		
		public DoorLock(string passCode, bool frontLocked, bool backLocked)
		{
			_passCode = passCode;
			_frontLocked = frontLocked;
			_backLocked = backLocked;
		}

		bool _locked = false;
		public bool isLocked
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

