using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Game.Core;

namespace Game.Characters.Tests{
	[TestFixture]
	public class PlayerMovementControllerTests{
		WalkAngleRestrictionArgs walkRestrictionArgs;
		float forwardWalkRestrictionAngle = 45;
		float backwardWalkRestrictinAngle = 135;

		[SetUp]
		public void Setup()
		{
			walkRestrictionArgs = new WalkAngleRestrictionArgs(
				forwardWalkRestrictionAngle, 
				backwardWalkRestrictinAngle
			);
		}

		[Test]
		public void T01UpdateMovementDirection_ReturnsIdle(){
			var inputs = new Vector3(0, 0, 0);
			var sut = new PlayerMovementController(
				new PlayerControlMock(inputs), walkRestrictionArgs
			);

			sut.UpdateMovementDirection(0);

			Assert.AreEqual(
				PlayerMovementController.MovementDirection.IDLE, sut.movementDirection
			);
		}

		[Test]
		public void T02aUpdateMovementDirection_ReturnsForward(){
			var angleOfMovementDirectionFromSightDirection = 0;

			var inputs = new Vector3(1, 1, 1); //Any Vector that's not zero means it's moving.

			
			var mock = new PlayerControlMock(inputs);
			var sut = new PlayerMovementController(
				mock, walkRestrictionArgs
			);

			sut.UpdateMovementDirection(angleOfMovementDirectionFromSightDirection);

			Assert.AreEqual(
				PlayerMovementController.MovementDirection.FORWARD, sut.movementDirection
			);
		}

		[Test]
		public void T02bUpdateMovementDirection_ReturnsForward(){
			var angleOfMovementDirectionFromSightDirection = forwardWalkRestrictionAngle;

			var inputs = new Vector3(1, 1, 1); //Any Vector that's not zero means it's moving.

			
			var mock = new PlayerControlMock(inputs);
			var sut = new PlayerMovementController(
				mock, walkRestrictionArgs
			);

			sut.UpdateMovementDirection(angleOfMovementDirectionFromSightDirection);

			Assert.AreEqual(
				PlayerMovementController.MovementDirection.FORWARD, sut.movementDirection
			);
		}


		[Test]
		public void T03aUpdateMovementDirection_ReturnsBackward(){
			var inputs = new Vector3(1, 1, 1); //Any Vector that's not zeromeans it's moving.
			var angleOfMovementDirectionFromSightDirection = 180f;

			var mock = new PlayerControlMock(inputs);
			var sut = new PlayerMovementController(
				mock, walkRestrictionArgs
			);

			sut.UpdateMovementDirection(angleOfMovementDirectionFromSightDirection);

			Assert.AreEqual(
				PlayerMovementController.MovementDirection.BACKWARD, sut.movementDirection
			);
		}

		[Test]
		public void T03bUpdateMovementDirection_ReturnsBackward(){
			var inputs = new Vector3(1, 1, 1); //Any Vector that's not zeromeans it's moving.
			var angleOfMovementDirectionFromSightDirection = backwardWalkRestrictinAngle;

			var mock = new PlayerControlMock(inputs);
			var sut = new PlayerMovementController(
				mock, walkRestrictionArgs
			);

			sut.UpdateMovementDirection(angleOfMovementDirectionFromSightDirection);

			Assert.AreEqual(
				PlayerMovementController.MovementDirection.BACKWARD, sut.movementDirection
			);
		}

		[Test]
		public void T04aUpdateMovementDirection_ReturnsLeft(){
			var inputs = new Vector3(1, 1, 1); //Any Vector that's not zeromeans it's moving.
			var angleOfMovementDirectionFromSightDirection = -90f;

			var mock = new PlayerControlMock(inputs);
			var sut = new PlayerMovementController(
				mock, walkRestrictionArgs
			);

			sut.UpdateMovementDirection(angleOfMovementDirectionFromSightDirection);

			Assert.AreEqual(
				PlayerMovementController.MovementDirection.LEFT, sut.movementDirection
			);
		}


		[Test]
		public void T04bUpdateMovementDirection_ReturnsLeft(){
			var inputs = new Vector3(1, 1, 1); //Any Vector that's not zeromeans it's moving.
			var angleOfMovementDirectionFromSightDirection = -forwardWalkRestrictionAngle - 1;

			var mock = new PlayerControlMock(inputs);
			var sut = new PlayerMovementController(
				mock, walkRestrictionArgs
			);

			sut.UpdateMovementDirection(angleOfMovementDirectionFromSightDirection);

			Assert.AreEqual(
				PlayerMovementController.MovementDirection.LEFT, sut.movementDirection
			);
		}

		[Test]
		public void T05aUpdateMovementDirection_ReturnsRight(){
			var inputs = new Vector3(1, 1, 1); //Any Vector that's not zeromeans it's moving.
			var angleOfMovementDirectionFromSightDirection = 90f;

			var mock = new PlayerControlMock(inputs);
			var sut = new PlayerMovementController(
				mock, walkRestrictionArgs
			);

			sut.UpdateMovementDirection(angleOfMovementDirectionFromSightDirection);

			Assert.AreEqual(
				PlayerMovementController.MovementDirection.RIGHT, sut.movementDirection
			);
		}

		[Test]
		public void T05bUpdateMovementDirection_ReturnsRight(){
			var inputs = new Vector3(1, 1, 1); //Any Vector that's not zeromeans it's moving.
			var angleOfMovementDirectionFromSightDirection = forwardWalkRestrictionAngle + 10;

			var mock = new PlayerControlMock(inputs);
			var sut = new PlayerMovementController(
				mock, walkRestrictionArgs
			);

			sut.UpdateMovementDirection(angleOfMovementDirectionFromSightDirection);

			Assert.AreEqual(
				PlayerMovementController.MovementDirection.RIGHT, sut.movementDirection
			);
		}

	}
}

