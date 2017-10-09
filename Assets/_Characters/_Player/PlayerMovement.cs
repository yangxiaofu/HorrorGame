using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Game.Characters
{
    public class PlayerMovement : CharacterMovement{
		[SerializeField] float m_MoveSpeedMultiplier = 1f;
		Transform mainCamera;          
		Vector3 m_CamForward;             // The current forward direction of the camera
        Vector3 m_Move;

		void Start()
        {
            GetCharacterMovementComponents();
            SetupCapsuleCollider();
            SetupRigidBodyVariables();
            FindMainCamera();
        }

        private void SetupCapsuleCollider()
        {
            m_CapsuleHeight = m_Capsule.height;
            m_CapsuleCenter = m_Capsule.center;
        }

        private void SetupRigidBodyVariables()
        {
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            m_OrigGroundCheckDistance = m_GroundCheckDistance;
        }

        private void FindMainCamera()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                mainCamera = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }
        }

        void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (mainCamera != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(mainCamera.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*mainCamera.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            Move(m_Move, crouch, false);

        }

		void OnAnimatorMove()
		{
			// we implement this function to override the default root motion.
			// this allows us to modify the positional speed before it's applied.
			if (m_IsGrounded && Time.deltaTime > 0)
			{
				Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

				// we preserve the existing y part of the current velocity.
				v.y = m_Rigidbody.velocity.y;
				m_Rigidbody.velocity = v;
			}
		}

	
	}
}

