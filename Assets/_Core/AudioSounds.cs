using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Game.Characters;

namespace Game.Core{
	//Attach this to the object being walked on.  Then add a colider on there.  
	public class AudioSounds : MonoBehaviour {

		[SerializeField] AudioClip _footstepAudio;

		void Start(){
			Assert.IsNotNull(_footstepAudio, "Please add a sound clip to ground object.");
		}

		void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.GetComponent<Player>())
			{
				AudioSource source = other.gameObject.GetComponent<Player>().GetComponent<AudioSource>();
				source.clip = _footstepAudio;
				source.Play();
			}
		}
	}

}
