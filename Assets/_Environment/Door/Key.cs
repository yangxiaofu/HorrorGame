using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Items{
	[System.Serializable]
	public class Key{
		[SerializeField] string passCode;
		public string passcode{get{return this.passCode;}}
	}
}

