using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TrafficSystem
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavmeshAgentController : AgentController
    {
        [SerializeField] NavMeshAgent _navMeshAgent;

        public void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void Update()
        {
            destinationCheck();
        }

        void destinationCheck()
        {
            bool reachedDestination = _navMeshAgent.GetPathRemainingDistance() <= _navMeshAgent.stoppingDistance;
            if (reachedDestination != _destinationReached)
            {
                if (reachedDestination)
                {
                    _destinationReached = true;
                    OnDestinationReached.Invoke(this);
                }
                else
                {
                    _destinationReached = false;
                }
            }
        }

        public override void ResumeMovement()
        {
            _navMeshAgent.Resume();
            _isMoving = true;
        }

        public override void SetDestination(Vector3 direction)
        {
            _navMeshAgent.SetDestination(direction);
            _isMoving = true;
        }

        public override void StopMovement()
        {
            _navMeshAgent.Stop();
            _isMoving = false;
        }
    }
}
