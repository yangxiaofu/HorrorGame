using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Characters;

namespace Game.Core	{
	public class CameraArm : MonoBehaviour {
		GameObject _player;
		void Start(){
			_player = GameObject.FindGameObjectWithTag("Player");
			Assert.IsNotNull(_player);
		}

		void Update(){
			this.transform.position = _player.transform.position;
		}
	}

}
