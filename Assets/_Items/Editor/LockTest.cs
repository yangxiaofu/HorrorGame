using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Game.Environment;

namespace Game.Core.LockTests
{
	[Category("Door Lock")]
	[TestFixture]	
	public class DoorLockTest
	{
		[Test]
		public void T01UnlockDoor_ReturnsDoorUnlocked()
		{
			string doorPassCode = "1234";
			string keyPassCode = "1234";

			DoorLock myLock = new DoorLock(doorPassCode, true, true);
			myLock.UnlockDoor(keyPassCode);

			Assert.AreEqual(false, myLock.isLockedOnBothSides);
		}

		[Test]
		public void T02UnLockDoor_ReturnsDoorLocked()
		{
			string doorPassCode = "1234";
			string keyPassCode = "123";

			DoorLock myLock = new DoorLock(doorPassCode, true, true);
			myLock.UnlockDoor(keyPassCode);

			Assert.AreEqual(true, myLock.isLockedOnBothSides);
		}

		[Test]
		public void T03LockDoor_ReturnsDoorLocked()
		{
			string doorPassCode = "1234";
			string keyPassCode = "1234";

			DoorLock myLock = new DoorLock(doorPassCode, false, false);
			myLock.LockDoor(keyPassCode);

			Assert.AreEqual(true, myLock.isLockedOnBothSides);
		}

		[Test]
		public void T04LockDoor_ReturnsDoorUnLocked()
		{
			string doorPassCode = "1234";
			string keyPassCode = "134";

			DoorLock myLock = new DoorLock(doorPassCode, false, false);
			myLock.LockDoor(keyPassCode);

			Assert.AreEqual(false, myLock.isLockedOnBothSides);
		}
	}

}

