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

		[Header("Capsule Collider")]
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
        protected AnimationState _animationState;
		public AnimationState animationState{
			get{return _animationState;}
		}

		protected void SetIdleAnimation()
        {
            _anim.SetBool(IS_IDLE, true);
            _animationState = AnimationState.IDLE;
        }

		protected void MoveBodyPosition()
        {
            _body.MovePosition(_body.position + _inputs * _speed * Time.fixedDeltaTime);
        }

		protected void GetRigidBodyComponent()
        {
            _body = GetComponent<Rigidbody>();
        }

		protected void GetAnimatorComponent()
        {
			_anim = GetComponent<Animator>();
        }

       
	}
}

