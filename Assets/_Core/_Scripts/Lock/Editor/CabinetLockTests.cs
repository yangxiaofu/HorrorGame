using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Game.Core.LockTests{

	[TestFixture]
	public class CabinetLockTests{

		[Test]
		public void T01UnlockCabinet_ReturnsUnlockedTrue(){
			string passCode = "0000";
			bool cabinetLocked = true;
			var sut = new CabinetLock(passCode, cabinetLocked);

			sut.UnlockDoor(passCode);
			Assert.AreEqual(false, sut.isLocked);
		}

		[Test]
		public void T02UnlockCabinet_ReturnLockedTrue(){
			string passCode = "0000";
			bool cabinetLocked = true;

			var sut = new CabinetLock(passCode, cabinetLocked);
			sut.LockDoor(passCode);
			Assert.AreEqual(true, sut.isLocked);
		}
	}

}
