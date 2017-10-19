using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Game.Characters;
using Game.Environment;

namespace Game.Core.DoorDetectionTests{
	[TestFixture]
	public class DoorDetectionTests 
	{
		[Test]
		public void T01aSidePlayerIsOn_ReturnsBack()
		{
			var doorDirection = new Vector3(1, 0 ,0);
			var doorPosition = new Vector3(1, 0, 0);
			var doorMock = new DoorMock(doorPosition, doorDirection);
			
			var playerPos = new Vector3(0, 0, 0);
			var playerMock = new PlayerMock(playerPos);
			var sut = new DoorDetection(doorMock, playerMock);
			var playerSideOn = sut.PlayerSideOn();
			Assert.AreEqual(DoorDetection.DoorSide.Back, playerSideOn);	
		}

		[Test]
		public void T01bSidePlayerIsOn_ReturnsBack()
		{
			var doorDirection = new Vector3(-1, 0 ,0);
			var doorPosition = new Vector3(0, 0, 0);
			var doorMock = new DoorMock(doorPosition, doorDirection);
			
			var playerPos = new Vector3(2, 0, 0);
			var playerMock = new PlayerMock(playerPos);
			var sut = new DoorDetection(doorMock, playerMock);
			var playerSideOn = sut.PlayerSideOn();
			Assert.AreEqual(DoorDetection.DoorSide.Back, playerSideOn);	
		}

		[Test]
		public void T01cSidePlayerIsOn_ReturnsBack()
		{
			var doorDirection = new Vector3(0, 0 ,1);
			var doorPosition = new Vector3(0, 0, 0);
			var doorMock = new DoorMock(doorPosition, doorDirection);
			
			var playerPos = new Vector3(0, 0, -1);
			var playerMock = new PlayerMock(playerPos);
			var sut = new DoorDetection(doorMock, playerMock);
			var playerSideOn = sut.PlayerSideOn();
			Assert.AreEqual(DoorDetection.DoorSide.Back, playerSideOn);	
		}

		[Test]
		public void T01dSidePlayerIsOn_ReturnsBack()
		{
			var doorDirection = new Vector3(0, 0 ,1);
			var doorPosition = new Vector3(0, 0, 0);
			var doorMock = new DoorMock(doorPosition, doorDirection);
			
			var playerPos = new Vector3(2, 0, -1);
			var playerMock = new PlayerMock(playerPos);
			var sut = new DoorDetection(doorMock, playerMock);
			var playerSideOn = sut.PlayerSideOn();
			Assert.AreEqual(DoorDetection.DoorSide.Back, playerSideOn);	
		}

		[Test]
		public void T02aSidePlayerIsOn_ReturnsFront()
		{
			var doorDirection = new Vector3(1, 0 ,0);
			var doorPosition = new Vector3(0, 0, 0);
			var doorMock = new DoorMock(doorPosition, doorDirection);
			
			var playerPos = new Vector3(1, 0, 0);
			var playerMock = new PlayerMock(playerPos);
			var sut = new DoorDetection(doorMock, playerMock);
			var playerSideOn = sut.PlayerSideOn();
			Assert.AreEqual(DoorDetection.DoorSide.Front, playerSideOn);	
		}

		[Test]
		public void T02bSidePlayerIsOn_ReturnsFront()
		{
			var doorDirection = new Vector3(-1, 0 ,0);
			var doorPosition = new Vector3(0, 0, 0);
			var doorMock = new DoorMock(doorPosition, doorDirection);
			
			var playerPos = new Vector3(-10, 0, 0);
			var playerMock = new PlayerMock(playerPos);
			var sut = new DoorDetection(doorMock, playerMock);
			var playerSideOn = sut.PlayerSideOn();
			Assert.AreEqual(DoorDetection.DoorSide.Front, playerSideOn);	
		}

		[Test]
		public void T02cSidePlayerIsOn_ReturnsFront()
		{
			var doorDirection = new Vector3(0, 0 ,1);
			var doorPosition = new Vector3(0, 0, 0);
			var doorMock = new DoorMock(doorPosition, doorDirection);
			
			var playerPos = new Vector3(0, 0, 1);
			var playerMock = new PlayerMock(playerPos);
			var sut = new DoorDetection(doorMock, playerMock);
			var playerSideOn = sut.PlayerSideOn();
			Assert.AreEqual(DoorDetection.DoorSide.Front, playerSideOn);	
		}

		[Test]
		public void T02dSidePlayerIsOn_ReturnsFront()
		{
			var doorDirection = new Vector3(0, 0 ,1);
			var doorPosition = new Vector3(0, 0, 0);
			var doorMock = new DoorMock(doorPosition, doorDirection);
			
			var playerPos = new Vector3(2, 0, 1);
			var playerMock = new PlayerMock(playerPos);
			var sut = new DoorDetection(doorMock, playerMock);
			var playerSideOn = sut.PlayerSideOn();
			Assert.AreEqual(DoorDetection.DoorSide.Front, playerSideOn);	
		}

	}

}

