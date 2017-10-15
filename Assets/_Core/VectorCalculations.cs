using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	public class VectorCalculations {
		private readonly Vector3 vec1;
		private readonly Vector3 vec2;
		public VectorCalculations(Vector3 vec1, Vector3 vec2){
			this.vec1 = vec1;
			this.vec2 = vec2;
		}

		public float GetAngle()
		{
			return Vector3.Angle(vec1, vec2);
		}

		public float GetSign()
		{
			var angle = GetAngle();
			return Mathf.Sign(Vector3.Cross(vec2, vec1).z);
		}
	}
}

