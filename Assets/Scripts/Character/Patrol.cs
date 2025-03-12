using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.AI;
using System;

namespace RPG.Character
{
    public class Patrol : MonoBehaviour
    {
        [SerializeField] private GameObject splineGameObject;
        [SerializeField] private float walkDuration = 3f;
        [SerializeField] private float pauseDuration = 2f;

        private SplineContainer splineCmp;
        private NavMeshAgent agentCmp;

        private float splinePosition = 0f;
        private float splineLength = 0f;
        private float lengthWalked = 0f;
        private float pauseTime = 0f;
        private float walkTime = 0f;
        private bool isWalking = true;

        private void Awake()
        {
            if (splineGameObject == null)
            {
                Debug.LogWarning($"{name} does not have a spline");
                return;
            }
            splineCmp = splineGameObject.GetComponent<SplineContainer>();
            splineLength = splineCmp.CalculateLength();
            agentCmp = GetComponent<NavMeshAgent>();


        }

        public Vector3 GetNextPatrolPoint()
        {
            return splineCmp.EvaluatePosition(splinePosition);
        }

        public void CalculateNextPatrolPoint()
        {
            walkTime += Time.deltaTime;

            if (walkTime > walkDuration)
            {
                isWalking = false;
            }

            if (!isWalking)
            {
                pauseTime += Time.deltaTime;

                if (pauseTime < pauseDuration)
                {
                    return;
                }

                ResetTimers();
            }

            lengthWalked += Time.deltaTime * agentCmp.speed;

            if (lengthWalked >= splineLength)
            {
                lengthWalked = 0f;
            }
            splinePosition = Mathf.Clamp01(lengthWalked / splineLength);
        }

        public void ResetTimers()
        {
            pauseTime = 0f;
            walkTime = 0f;
            isWalking = true;
        }

        public Vector3 GetFartherOutPosition()
        {
            float tempSplinePosition = splinePosition + 0.02f;
            if (tempSplinePosition >= 1)
            {   // TempSplinePosition = TempSplinePosition - 1;
                tempSplinePosition -= 1;
            }
            return splineCmp.EvaluatePosition(tempSplinePosition);
        }

        internal Vector3 GetCurrentPosition()
        {
            throw new NotImplementedException();
        }
    }

}