using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	public class Normalizer{
		public readonly float _value;
		public readonly float _minVal;
		public readonly float _maxVal;
		public Normalizer(float value, float minimumValue, float maximumValue){
			_value = value;
			_minVal = minimumValue;
			_maxVal = maximumValue;
		}

		public float NormalizedValue()
		{
			return _value / _maxVal;
		}

		public float InverseNormalizedValue()
		{

			return 1 - (_value / _maxVal);
		}
	}
}

