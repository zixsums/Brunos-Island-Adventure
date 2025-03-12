using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.AI;
using RPG.Utility;
using System;
using UnityEditor;
using Unity.Mathematics;

namespace RPG.Character
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour
    {
        [NonSerialized] public Vector3 originalForwardVector;
        [NonSerialized] public bool isMoving = false;
        private Vector3 movementVector;
        private bool clampAnimatorSpeedAgain = true;
        private NavMeshAgent agent;
        private Animator animatorCmp;
        private Vector3 offset;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animatorCmp = GetComponentInChildren<Animator>();

            originalForwardVector = transform.forward;
        }

        private void Start()
        {
            agent.updateRotation = false;
        }
        private void Update()
        {
            Moveplayer();
            MovementAnimation();
            if (CompareTag(Constants.PLAYER_TAG)) Rotate(movementVector);
        }

        private void Moveplayer()
        {
            Vector3 offset = movementVector * Time.deltaTime * agent.speed;
            agent.Move(offset);
        }
        public void HandleMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            movementVector = new Vector3(input.x, 0, input.y);
            if (context.performed) isMoving = true;
            if (context.canceled) isMoving = false;

        }

        public void Rotate(Vector3 newForwardVector)
        {
            if (newForwardVector == Vector3.zero) return;

            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.LookRotation(newForwardVector);

            transform.rotation = Quaternion.Lerp(
                startRotation,
                endRotation,
                Time.deltaTime * agent.angularSpeed
            );

        }

        public void MoveAgentByDestination(Vector3 destination)
        {
            agent.SetDestination(destination);
            isMoving = true;
        }

        public void StopMovingAgent()
        {
            agent.ResetPath();
            isMoving = false;
        }

        public bool ReachedDestination()
        {
            if (agent.pathPending) return false;

            if (agent.remainingDistance > agent.stoppingDistance) return false;

            if (agent.hasPath || agent.velocity.sqrMagnitude != 0f) return false;

            return true;
        }

        public void MoveAgentByOffset(Vector3 offset)
        {
            agent.Move(offset);
            isMoving = true;
        }

        public void UpdateAgentSpeed(float newspeed, bool shouldClampSpeed)
        {
            agent.speed = newspeed;
            clampAnimatorSpeedAgain = shouldClampSpeed;

        }

        private void MovementAnimation()
        {
            float speed = animatorCmp.GetFloat(Constants.SPEED_ANIMATOR_PARAM);
            float smoothening = Time.deltaTime * agent.acceleration;
            if (isMoving)
            {
                speed += smoothening;
            }
            else
            {
                speed -= smoothening;
            }
            speed = Mathf.Clamp01(speed);
            if (CompareTag(Constants.ENEMY_TAG) && clampAnimatorSpeedAgain)
            {
                speed = Mathf.Clamp(speed, 0, 0.5f);
            }
            animatorCmp.SetFloat(Constants.SPEED_ANIMATOR_PARAM, speed);
        }
    }

}


