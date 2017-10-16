using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Game.Characters.Tests{
	[Category("Player Energy Tests")]
	[TestFixture]
	public class PlayerEnergyTests{

		[Test] 
		public void T01PlayerEnergyController_ReduceEnergy_ReturnsNewReducedEnergy()
		{
			var energyToReduce = 50f;
			var currentEnergy = 100f;
			var startingEnergy = 100f;

			var mock = new PlayerEnergyMock(currentEnergy, startingEnergy);
			var sut = new PlayerEnergyController(mock);

			sut.ReduceEnergy(energyToReduce);
			Assert.AreEqual(energyToReduce, mock.currentEnergy);
		}

		[Test] 
		public void T02PlayerEnergyController_ReduceEnergy_ReturnsZero()
		{
			var energyToReduce = 150f;
			var currentEnergy = 100f;
			var startingEnergy = 100f;
			var minimumEnergyAfterReduction = 0;

			var mock = new PlayerEnergyMock(currentEnergy, startingEnergy);
			var sut = new PlayerEnergyController(mock);

			sut.ReduceEnergy(energyToReduce);
			Assert.AreEqual(minimumEnergyAfterReduction, mock.currentEnergy);
		}


		[Test] 
		public void T03PlayerEnergyController_IncreaseEnergy_ReturnsNewCurrentEnergy()
		{
			var energyToAdd = 50f;
			var currentEnergy = 5f;
			var startingEnergy = 100f;

			var mock = new PlayerEnergyMock(currentEnergy, startingEnergy);
			var sut = new PlayerEnergyController(mock);

			sut.IncreaseEnergy(energyToAdd);
			Assert.AreEqual(energyToAdd + currentEnergy, mock.currentEnergy);
		}

		[Test] 
		public void T04PlayerEnergyController_IncreaseEnergy_ReturnsNoMoreThanMaxEnergy()
		{
			var energyToAdd = 200f;
			var currentEnergy = 5f;
			var startingEnergy = 100f;
			var maxEnergyLevel = 100f;

			var mock = new PlayerEnergyMock(currentEnergy, startingEnergy);
			var sut = new PlayerEnergyController(mock);

			sut.IncreaseEnergy(energyToAdd);
			Assert.AreEqual(maxEnergyLevel, mock.currentEnergy);
		}
	}
}

