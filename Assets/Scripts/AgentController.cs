using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TrafficSystem
{
    public abstract class AgentController : MonoBehaviour
    {
        public UnityEvent<AgentController> OnDestinationReached;
        public bool DestinationReached { get => _destinationReached; }
        [SerializeField] protected bool _destinationReached;
        public bool IsMoving { get => _isMoving; }
        [SerializeField] protected bool _isMoving;
        
        public abstract void SetDestination(Vector3 direction);
        public abstract void StopMovement();
        public abstract void ResumeMovement();
        public abstract void SetSpeed(float newSpeed);

    }
}
