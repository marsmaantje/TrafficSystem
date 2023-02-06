using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrafficSystem
{
    public class BasicAgentController : AgentController
    {
        [SerializeField] float _movementSpeed;
        [SerializeField] float _rotationSpeed;
        [SerializeField] float _stopDistance;
        [SerializeField] Vector3 _destination;

        // Update is called once per frame
        void Update()
        {
            DestinationCheck();
            if (!_destinationReached)
                UpdateMovement();
        }

        void UpdateMovement()
        {
            if (IsMoving)
            {
                transform.position = Vector3.MoveTowards(transform.position, _destination, _movementSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_destination - transform.position), _rotationSpeed * Time.deltaTime);
            }
        }

        void DestinationCheck()
        {
            bool reached = Vector3.Distance(transform.position, _destination) <= _stopDistance;
            if (reached != _destinationReached)
            {
                if (reached)
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

        public override void SetDestination(Vector3 pDestination)
        {
            _destination = pDestination;
        }

        public override void StopMovement()
        {
            _isMoving = false;
        }

        public override void ResumeMovement()
        {
            _isMoving = true;
        }
    }
}