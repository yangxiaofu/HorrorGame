using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Game.Characters{
	public class EnemySight : MonoBehaviour {
		[SerializeField] float _angleOfSight = 45f;
		[SerializeField] float _sightDistance = 50f;
		const string PLAYER = "Player";
		Player _target;
		Vector3 _targetDirection;
		float _sightHeight;

		public delegate void PlayerSeen(Player player);
		public event PlayerSeen OnPlayerSeen;
		
		// Use this for initialization
		void Start () 
		{
			_target = FindObjectOfType<Player>();
			Assert.IsNotNull(_target, "Player is not in the game scene.");
		}
		
		void FixedUpdate()
		{
			_targetDirection = _target.transform.position - transform.position;
			float angleOfPlayer = Vector3.Angle(_targetDirection, this.transform.forward);
			
			if (PlayerInLineOfSight(angleOfPlayer))
            {
                RaycastForPlayer();
            }
        }

        private void RaycastForPlayer()
        {
            var rayCastDirection = transform.TransformDirection(new Vector3(_targetDirection.x, 0, _targetDirection.z));
            RaycastHit hit;
            if (Physics.Raycast(transform.position, rayCastDirection, out hit, _sightDistance))
            {
                if (hit.transform.CompareTag(PLAYER))
                {
					Assert.IsNotNull(hit.transform.gameObject.GetComponent<Player>());

					if (OnPlayerSeen != null) 
					{
						OnPlayerSeen(hit.transform.gameObject.GetComponent<Player>());
					}
                }
            }
        }

        private bool PlayerInLineOfSight(float angleOfPlayer)
		{
			return angleOfPlayer < _angleOfSight;
		}
	}
}

