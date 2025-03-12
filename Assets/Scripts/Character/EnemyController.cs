using UnityEngine;
using System;
using RPG.Utility;

namespace RPG.Character
{
    public class EnemyController : MonoBehaviour
    {
        [NonSerialized] public float distancefromPlayer;
        [NonSerialized] public Vector3 originalPosition;
        [NonSerialized] public Movement movementCmp;
        [NonSerialized] public GameObject player;
        [NonSerialized] public Patrol patrolCmp;
        private Health healthCmp;
        [NonSerialized] public Combat combatCmp;

        public CharaterStatsSO stats;

        public float chaseRange = 2.5f;
        public float attackRange = 0.75f;

        private AIBaseState currentState;
        public AIReturnState returnState = new AIReturnState();
        public AIChaseState chaseState = new AIChaseState();
        public AIAttackState attackState = new AIAttackState();
        public AIPatrolState patrolState = new AIPatrolState();
        public AIDefeatedState defeatedState = new AIDefeatedState();

        private void Awake()
        {
            if (stats == null)
            {
                Debug.LogWarning($"{name} does not have stats ");
            }

            currentState = returnState;

            player = GameObject.FindWithTag(Constants.PLAYER_TAG);
            movementCmp = GetComponent<Movement>();
            patrolCmp = GetComponent<Patrol>();
            healthCmp = GetComponent<Health>();
            combatCmp = GetComponent<Combat>();

            originalPosition = transform.position;
        }

        private void Start()
        {
            currentState.EnterState(this);

            healthCmp.healthPoints = stats.health;
            combatCmp.damage = stats.damage;
        }

        private void OnEnable()
        {
            healthCmp.OnStartDefeat += HandleStartDefeated;
        }

        private void OnDisable()
        {
            healthCmp.OnStartDefeat -= HandleStartDefeated;
        }

        private void Update()
        {
            calculateDistancefromPlayer();

            currentState.UpdateState(this);
        }

        public void SwitchState(AIBaseState newState)
        {
            currentState = newState;
            currentState.EnterState(this);
        }

        private void calculateDistancefromPlayer()
        {
            if (player == null) return;

            Vector3 enemyPosition = transform.position;
            Vector3 playerPosition = player.transform.position;

            distancefromPlayer = Vector3.Distance(
                enemyPosition, playerPosition
                );
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,
            chaseRange
            );
        }

        private void HandleStartDefeated()
        {
            SwitchState(defeatedState);
            currentState.EnterState(this);
        }
    }
}
