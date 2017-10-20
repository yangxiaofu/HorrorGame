using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Characters{
	public class EnemySight : MonoBehaviour {
		float _angleOfSight = 45f;
		float _sightDistance = 6f;
		Transform _parent;
		const string PLAYER = "Player";
		Player _target;
		Vector3 _targetDirection;

		public delegate void PlayerSeen(Player player);
		public event PlayerSeen OnPlayerSeen;
		
		// Use this for initialization
		void Start () 
		{
			_target = FindObjectOfType<Player>();
			Assert.IsNotNull(_target, "Player is not in the game scene.");
		}

		public void SetupPlayerSight(float sightDistance, float angleOfSight)
		{
			_sightDistance = sightDistance;
			_angleOfSight = angleOfSight;
		}
		
		public void Setup(Transform parent)
		{
			_parent = parent;
		}
		
		void FixedUpdate()
		{
			_targetDirection = _target.transform.position - _parent.transform.position;

			float angleOfPlayer = Vector3.Angle(
				_targetDirection, 
				_parent.transform.forward
			);

			if (PlayerInLineOfSight(angleOfPlayer))
            {
                RaycastForPlayer();
            }
        }

        private void RaycastForPlayer()
        {
            RaycastHit hit;
			
            if (Physics.Raycast(
				transform.position, _targetDirection, out hit, _sightDistance))

            {
                if (!hit.transform.CompareTag(PLAYER)) return;
		
				if (OnPlayerSeen != null) OnPlayerSeen(_target);
            }
        }

        private bool PlayerInLineOfSight(float angleOfPlayer)
		{
			return angleOfPlayer < _angleOfSight;
		}
	}
}

