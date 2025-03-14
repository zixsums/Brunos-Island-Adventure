using UnityEngine;

namespace RPG.Character
{
    public class AIPatrolState : AIBaseState
    {
        private Vector3 currentPosition;

        public override void EnterState(EnemyController enemy)
        {
            enemy.patrolCmp.ResetTimers();
        }

        public override void UpdateState(EnemyController enemy)
        {
            if (enemy.distancefromPlayer < enemy.chaseRange)
            {
                enemy.SwitchState(enemy.chaseState);
                return;
            }

            Vector3 oldPosition = enemy.patrolCmp.GetNextPatrolPoint();

            enemy.patrolCmp.CalculateNextPatrolPoint();

            Vector3 currentPosition = enemy.transform.position;
            Vector3 newPosition = enemy.patrolCmp.GetNextPatrolPoint();
            Vector3 offset = newPosition - currentPosition;

            enemy.movementCmp.MoveAgentByOffset(offset);
            Vector3 fartherOutPosition = enemy.patrolCmp.GetFartherOutPosition();
            Vector3 newForwardVector = fartherOutPosition - currentPosition;
            newForwardVector.y = 0;
            enemy.movementCmp.Rotate(newForwardVector);

            if (oldPosition == newPosition)
            {
                enemy.movementCmp.isMoving = false;
            }
        }
    }
}