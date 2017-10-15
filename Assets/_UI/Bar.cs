using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using Game.Characters;

namespace Game.UI{
	public class Bar : MonoBehaviour {
		protected Player _player;
		protected RawImage _barImage;

		protected void SetupBarVariables()
        {
            _player = FindObjectOfType<Player>();

			Assert.IsNotNull(
				_player, 
				"The player does not have a Player component attached to it.  Double check the player component on the Player"
			);
            _barImage = GetComponent<RawImage>();
        }

        protected void UpdateUIBar(float percentage)
        {
            float xValue = -(percentage / 2f) - 0.5f;
            _barImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
        }
	}
}

