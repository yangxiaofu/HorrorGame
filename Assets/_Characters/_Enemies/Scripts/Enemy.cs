using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using Game.Core;

//AI logic in the enemy base. 
namespace Game.Characters{
	[SelectionBase]
	[RequireComponent(typeof(EnemyControl))]
	public class Enemy : Character{
		[Range(0, 1)]
		[SerializeField] float _hitSuccessPercentage = .50f;
		[SerializeField] float _hitDamage = 50f;
        [SerializeField] float _distanceToStartAudio = 5f;

        [Header("Enemy Vision")]
        [SerializeField] float _sightDistance = 5f;
        [SerializeField] float _angleOfSight = 45f;
		EnemySight _sight;
		EnemyControl _enemyControl;
		Player _player;
		
		void Awake()
		{
			AddCapsuleCollider();
            AddRigidBodyComponent();
			AddAnimatorComponent();
            AddAudioSource();
        }
		void Start()
        {
            FindPlayer();
            SetupEnemyVision();
            SetupEnergyControl();
        }

		void Update()
		{
			if (PlayerOrEnemyIsDead())
            {
				GetComponent<NavMeshAgent>().isStopped = true;
                return;
            }

            UpdateEnemySoundVolume();
            ScanForPlayerWithinSightRadius();			
			ScanForPlayerInAttackRadius();	
            _enemyControl.UpdateEnemyMovementAnimation();
			
		}
        private void UpdateEnemySoundVolume()
        {
            var distanceFromPlayer = Vector3.Distance(this.transform.position, _player.transform.position);
            if (distanceFromPlayer <= _distanceToStartAudio)
            {
                var normalizer = new Normalizer(distanceFromPlayer, 0, _distanceToStartAudio);
                var volume = normalizer.InverseNormalizedValue();
                _audioSource.volume = volume;
            } else {
                _audioSource.volume = 0f;
            }
            
        }

		private bool PlayerOrEnemyIsDead(){
			return (_isDead || (_enemyControl.target != null && _enemyControl.TargetIsDead()));
		}

        private void FindPlayer()
        {
            _player = FindObjectOfType<Player>();

            Assert.IsNotNull(
                _player,
                "There is no player in the game scene."
            );
        }

        private void SetupEnergyControl()
        {
            _enemyControl = GetComponent<EnemyControl>();
            Assert.IsNotNull(_enemyControl, "There is no enemy control scrip on the game object of " + name);
        }

        private void SetupEnemyVision()
        {
            _sight = GetComponentInChildren<EnemySight>();
            Assert.IsNotNull(
                _sight,
                "You need to add the player sight into the transform of the player."
            );
            _sight.Setup(this.transform);
            _sight.SetupPlayerSight(_sightDistance, _angleOfSight);
            _sight.OnPlayerSeen += OnPlayerSeen;
        }

		void Hit() //Callback furnction from the animatior.
		{
			//Calculate Hit Percentage on teh player.
			if (UnityEngine.Random.Range(0f, 1f) >= _hitSuccessPercentage) return;
			
			if (_enemyControl.target == null) return;

			_enemyControl.target.GetComponent<CharacterHealth>().TakeDamage(_hitDamage);
			 			
		}

		private void ScanForPlayerInAttackRadius()
        {
			var distanceFromPlayer = Vector3.Distance(_player.transform.position, this.transform.position);
			
			if (distanceFromPlayer < _meleeAttackRadius)
			{
				_enemyControl.SetState(CharacterControl.AnimationState.ATTACK);	
			}
        }
		
        private void ScanForPlayerWithinSightRadius()
        {
            var distanceFromPlayer = Vector3.Distance(this.transform.position, _player.transform.position);

            if (distanceFromPlayer > _sightDistance)
            {
                _enemyControl.SetTarget(null);
            }
        }

        void OnPlayerSeen(Player player)
		{
			_enemyControl.SetTarget(player.transform);
		}

		void OnDrawGizmos()
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(
				this.transform.position, 
				_sightDistance
			);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(
                this.transform.position, 
                _distanceToStartAudio
            );
		}

        public override void ResetCharacter()
        {
            Destroy(this.gameObject);
        }
    }

}
