using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityStandardAssets.Characters.ThirdPerson;


namespace Game.Characters
{
	public class CharacterMovement : MonoBehaviour 
	{

		AICharacterControl _aiCharacterControl;

		void Start()
		{
			_aiCharacterControl = GetComponent<AICharacterControl>();
			Assert.IsNotNull(_aiCharacterControl);
		}

		public void SetTarget(Transform target)
		{
			_aiCharacterControl.SetTarget(target);
		}
	}

}
