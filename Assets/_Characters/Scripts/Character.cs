using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Characters{
	public abstract class Character : MonoBehaviour {
		[Header("Character General")]
		[SerializeField] protected float _meleeAttackRadius = 2f;
		[SerializeField] AnimationClip _deathAnimationClip;

		[Header("Animator Variables")]
		[SerializeField] protected AnimatorOverrideController _animOC;
        public AnimatorOverrideController animOC{get{return _animOC;}}
		[SerializeField] protected Avatar _avatar;

		[Header("Capsule Collider")]
		[SerializeField] protected Vector3 _center = new Vector3(0, 0.8f, 0);
		[SerializeField] protected float _radius = 0.3f;
		[SerializeField] protected float _height = 1.6f;

		[Header("Audio")]
		[SerializeField] AudioClip _footstepsAudio;
		[Range(0, 1)] [SerializeField] float _audioVolume = 1;
		protected AudioSource _audioSource;
		protected bool _isDead = false;
		public bool isDead{get{return _isDead;}}
		const string DEATH_TRIGGER = "death";

		void OnDrawGizmos()
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(this.transform.position, _meleeAttackRadius);
		}

        void StepAudio()
        {
            _audioSource.clip = _footstepsAudio;
            _audioSource.Play();
        }

		protected void AddAudioSource()
        {
            _audioSource = this.gameObject.AddComponent<AudioSource>();
            _audioSource.loop = false;
            _audioSource.playOnAwake = false;
			_audioSource.volume = _audioVolume;
        }

		public IEnumerator KillCharacter(float delay)
		{
			_isDead = true;
			//Do death animation.
			GetComponent<Animator>().SetTrigger(DEATH_TRIGGER);
			GetComponent<Rigidbody>().Sleep();

			yield return new WaitForSeconds(delay);

			ResetCharacter();

			yield return null;
		}

		protected void AddAnimatorComponent()
        {
            Assert.IsNotNull(
				_animOC,
				"Please add the player animation Override Controller referenced in the PlayerControl component"
			);

            Assert.IsNotNull(
                _avatar,
                "Add the player avatar into PlayerControl component on Player"
            );

            var anim = gameObject.AddComponent<Animator>();
            anim.runtimeAnimatorController = _animOC;
            anim.avatar = _avatar;
        }

		protected void AddRigidBodyComponent()
        {
            var body = gameObject.AddComponent<Rigidbody>();
			body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

		protected void AddCapsuleCollider()
        {
			var cc = gameObject.AddComponent<CapsuleCollider>();
			cc.center = _center;
			cc.radius = _radius;
			cc.height = _height;
        }

		public abstract void ResetCharacter();
		
	}
}

