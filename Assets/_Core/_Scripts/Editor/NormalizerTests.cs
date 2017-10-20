using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Game.Core.Tests;
using NUnit.Framework;

namespace Game.Core.Tests{
	[TestFixture]
	public class NormalizerTests {

		[Test]
		public void T01aNormalizedValue_ReturnsOne()
		{
			float valueUnderTest = 100;
			float minVal = 0;
			float maxVal = 100;

			var sut = new Normalizer(valueUnderTest, minVal, maxVal);
			sut.NormalizedValue();

			Assert.AreEqual(1, sut.NormalizedValue());
		}

		[Test]
		public void T01bNormalizedValue_ReturnsOne()
		{
			float valueUnderTest = 5;
			float minVal = 0;
			float maxVal = 5;

			var sut = new Normalizer(valueUnderTest, minVal, maxVal);
			sut.NormalizedValue();

			Assert.AreEqual(1, sut.NormalizedValue());
		}

		[Test]
		public void T01cNormalizedValue_ReturnsOneHalf()
		{
			float valueUnderTest = 2.5f;
			float minVal = 0;
			float maxVal = 5;

			var sut = new Normalizer(valueUnderTest, minVal, maxVal);
			sut.NormalizedValue();

			Assert.AreEqual(0.5f, sut.NormalizedValue());
		}

		[Test]
		public void T02aInverseNormalizedValue_ReturnsOne()
		{
			float valueUnderTest = 100;
			float minVal = 0;
			float maxVal = 100;

			var sut = new Normalizer(valueUnderTest, minVal, maxVal);
			

			Assert.AreEqual(0, sut.InverseNormalizedValue());
		}

		[Test]
		public void T02bInverseNormalizedValue_ReturnsOne()
		{
			float valueUnderTest = 5;
			float minVal = 0;
			float maxVal = 5;

			var sut = new Normalizer(valueUnderTest, minVal, maxVal);
			

			Assert.AreEqual(0, sut.InverseNormalizedValue());
		}

		[Test]
		public void T02cInverseNormalizedValue_ReturnsOneHalf()
		{
			float valueUnderTest = 2.5f;
			float minVal = 0;
			float maxVal = 5;

			var sut = new Normalizer(valueUnderTest, minVal, maxVal);

			Assert.AreEqual(0.5f, sut.InverseNormalizedValue());
		}
	}

}
