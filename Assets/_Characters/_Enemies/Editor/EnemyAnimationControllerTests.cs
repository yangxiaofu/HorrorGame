using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Game.Characters.Tests{
	[Category("Enemy Animation Controller Tests")]
	[TestFixture]
	public class EnemyAnimationControllerTests 
	{

		[Test]
		public void T01UpdateAnimationController_ReturnsForwardDirection()
		{
			var fake = new GameObject("Fake Transform");
			var mock = new EnemyControlMock(fake.transform, CharacterControl.AnimationState.FORWARD);

			var sut = new EnemyAnimationController(mock);
			sut.UpdateAnimationState();

			Assert.AreEqual(CharacterControl.AnimationState.FORWARD, sut.animationState);
		}

		[Test]
		public void T02UpdateAnimationController_ReturnsIDLE()
		{
			var mock = new EnemyControlMock(null, CharacterControl.AnimationState.FORWARD);

			var sut = new EnemyAnimationController(mock);
			sut.UpdateAnimationState();

			Assert.AreEqual(CharacterControl.AnimationState.IDLE, sut.animationState);
		}

		[Test]
		public void T03UpdateAnimationController_ReturnsAttack()
		{
			var fake = new GameObject("Fake Transform");
			var mock = new EnemyControlMock(fake.transform, CharacterControl.AnimationState.ATTACK);

			var sut = new EnemyAnimationController(mock);
			sut.UpdateAnimationState();

			Assert.AreEqual(CharacterControl.AnimationState.ATTACK, sut.animationState);
		}
	}

}
