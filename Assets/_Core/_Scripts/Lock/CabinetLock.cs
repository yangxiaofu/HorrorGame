using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	
	[System.Serializable]
    public class CabinetLock : Lock
    {
		[SerializeField] bool _locked = false;
		public bool isLocked{get{return _locked;}}

		public CabinetLock(string passCode, bool locked)
		{
			_passCode = passCode;
			_locked = locked;
		}

        public override void LockDoor(string passCode)
        {
            if (_passCode == passCode){
				_locked = true;
			}
        }

        public override void UnlockDoor(string passCode)
        {
             if (_passCode == passCode){
				_locked = false;
			}
        }
    }

}
