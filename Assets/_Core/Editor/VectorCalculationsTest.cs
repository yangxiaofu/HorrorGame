using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Game.Core.Tests{
	[Category("Vector Calculations Test")]
	[TestFixture]
	public class VectorCalculationsTest{

		[Test]
		public void T01GetAngle_Return90Degrees(){
			var vec1 = new Vector3(0, 1, 0);
			var vec2 = new Vector3(1, 0, 0);

			var sut = new VectorCalculations(vec1, vec2);
			Assert.AreEqual(90, sut.GetAngle());
		}

		[Test]
		public void T02aGetSign_ReturnPositionSign(){
			var vec1 = new Vector3(0, 1, 0);
			var vec2 = new Vector3(1, 0, 0);

			var sut = new VectorCalculations(vec1, vec2);
			Assert.AreEqual(1, sut.GetSign());
		}

		[Test]
		public void T02bGetSign_ReturnPositionSign(){
			var vec1 = new Vector3(-1, 5, 0);
			var vec2 = new Vector3(0, 10, 0);

			var sut = new VectorCalculations(vec1, vec2);
			Assert.AreEqual(1, sut.GetSign());
		}

		[Test]
		public void T02cGetSign_ReturnPositionSign(){
			var vec1 = new Vector3(0, -10, 0);
			var vec2 = new Vector3(-1, -2, 0);

			var sut = new VectorCalculations(vec1, vec2);
			Assert.AreEqual(1, sut.GetSign());
		}

		[Test]
		public void T03aGetSign_ReturnNegativeSign(){
			var vec1 = new Vector3(0, 1, 0);
			var vec2 = new Vector3(-1, 0, 0);

			var sut = new VectorCalculations(vec1, vec2);
			Assert.AreEqual(-1, sut.GetSign());
		}

		[Test]
		public void T03bGetSign_ReturnNegativeSign(){
			var vec1 = new Vector3(1, 0, 0);
			var vec2 = new Vector3(1, 2, 0);

			var sut = new VectorCalculations(vec1, vec2);
			Assert.AreEqual(-1, sut.GetSign());
		}

		[Test]
		public void T03cGetSign_ReturnNegativeSign(){
			var vec1 = new Vector3(0, -10, 0);
			var vec2 = new Vector3(1, -3, 0);

			var sut = new VectorCalculations(vec1, vec2);
			Assert.AreEqual(-1, sut.GetSign());
		}
	}

}
