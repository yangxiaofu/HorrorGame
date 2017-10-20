using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core{
	public class Flashlight : MonoBehaviour {

		[SerializeField] bool _isOn = true;
		public bool isOn{get{return _isOn;}}
		Light[] _lights;
		public void ToggleFlashlight(bool isOn)
		{
			_isOn = isOn;

			for(int i = 0; i < _lights.Length; i++)
			{
				_lights[i].enabled = _isOn;
			}
			if (OnFlashLightToggled != null) OnFlashLightToggled(isOn);
		}

		public delegate void FlashLightToggled(bool isOn);
		public event FlashLightToggled OnFlashLightToggled;

		void Start()
		{
			_lights = GetComponentsInChildren<Light>();
		}
	}
}
