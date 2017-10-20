using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	public class Flashlight : MonoBehaviour {

		[SerializeField] bool _isOn = true;
		Light[] _lights;
		public bool isOn{
			get{return _isOn;}
			set{_isOn = value;}
		}

		void Start()
		{
			_lights = GetComponentsInChildren<Light>();
		}

		void Update(){
			for(int i = 0; i < _lights.Length; i++)
			{
				_lights[i].enabled = _isOn;
			}
		}
	}
}
