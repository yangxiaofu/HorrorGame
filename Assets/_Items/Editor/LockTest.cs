using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Game.Environment;

namespace Game.Core.LockTests{
	
	[TestFixture]	
	public class LockTest {

		[Test]
		public void T01UnlockDoor_ReturnsDoorUnlocked()
		{
			string doorPassCode = "1234";
			string keyPassCode = "1234";

			Lock myLock = new Lock(doorPassCode, true);
			myLock.UnlockDoor(keyPassCode);

			Assert.AreEqual(false, myLock.isLocked);
		}

		[Test]
		public void T02UnLockDoor_ReturnsDoorLocked()
		{
			string doorPassCode = "1234";
			string keyPassCode = "123";

			Lock myLock = new Lock(doorPassCode, true);
			myLock.UnlockDoor(keyPassCode);

			Assert.AreEqual(true, myLock.isLocked);
		}

		[Test]
		public void T03LockDoor_ReturnsDoorLocked()
		{
			string doorPassCode = "1234";
			string keyPassCode = "1234";

			Lock myLock = new Lock(doorPassCode, false);
			myLock.LockDoor(keyPassCode);

			Assert.AreEqual(true, myLock.isLocked);
		}

		[Test]
		public void T04LockDoor_ReturnsDoorUnLocked()
		{
			string doorPassCode = "1234";
			string keyPassCode = "134";

			Lock myLock = new Lock(doorPassCode, false);
			myLock.LockDoor(keyPassCode);

			Assert.AreEqual(false, myLock.isLocked);
		}


	}

}

