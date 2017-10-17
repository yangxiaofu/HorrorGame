using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Characters{
	public class CharacterControl : MonoBehaviour, IPlayerControl {

		[Header("Character General")]
		[SerializeField] protected float _forwardSpeed = 3f;

        protected Vector3 _inputs = Vector3.zero;
		public Vector3 inputs{
			get{return _inputs;}
			set{_inputs = value;}
		}

		[Header("Animator Variables")]
		[SerializeField] protected AnimatorOverrideController _animOC;
        public AnimatorOverrideController animOC{get{return _animOC;}}
		[SerializeField] protected Avatar _avatar;

		[Header("Capsule Collider")]
		[SerializeField] protected Vector3 _center = new Vector3(0, 0.8f, 0);
		[SerializeField] protected float _radius = 0.3f;
		[SerializeField] protected float _height = 1.6f;

		protected float _speed = 0f;
		protected Rigidbody _body;
		protected Animator _anim;

		protected const string IS_IDLE = "isIdle";
        protected const string ANIMATION_STATE_FORWARD = "WalkForward";
    	protected const string ANIMATION_STATE_BACKWARD = "WalkBackward";
        protected const string ANIMATION_STATE_STRAFE_LEFT = "Strafe Left";
        protected const string ANIMATION_STATE_STRAFE_RIGHT = "Strafe Right";

		public enum AnimationState {
            FORWARD, BACKWARD, LEFT, RIGHT, IDLE, ATTACK
        }
        public AnimationState _animationState;
		public AnimationState animationState{
			get{return _animationState;}
			set{_animationState = value;}
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

            _anim = gameObject.AddComponent<Animator>();
            _anim.runtimeAnimatorController = _animOC;
            _anim.avatar = _avatar;
        }

		protected void SetIdleAnimation()
        {
            _anim.SetBool(IS_IDLE, true);
            _animationState = AnimationState.IDLE;
        }

		protected void AddCapsuleCollider()
        {
			var cc = gameObject.AddComponent<CapsuleCollider>();
			cc.center = _center;
			cc.radius = _radius;
			cc.height = _height;
        }

		protected void AddRigidBodyComponent()
        {
            _body = gameObject.AddComponent<Rigidbody>();
			_body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
			
        }

		protected void MoveBodyPosition()
        {
            _body.MovePosition(_body.position + _inputs * _speed * Time.fixedDeltaTime);
        }
       
	}
}

