using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// For CPU movement
namespace Hoover
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerAi : MonoBehaviour
    {
        // Cache
        NavMeshAgent agent;

        void Awake()
        {
            // Cache
            agent = GetComponent<NavMeshAgent>();
        }

        void Start()
        {
            agent.destination = FindNearestPickup();
        }

        void Update()
        {
            // Have we reached the nearest pickup?
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                // Find next pickup
                agent.destination = FindNearestPickup();
            }
        }

        Vector3 FindNearestPickup()
        {
            // Get pickups
            // This needs to be done dynamically with FindGameObjects because pickups will be constantly being destroyed
            GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");

            // Find which one is closest
            GameObject closest = null;
            float bestDistance = 100000; // Start high otherwise at 0 nothing will ever be found

            foreach (var pickup in pickups)
            {
                // Is it closer than the current best?
                float distance = Vector3.Distance(transform.position, pickup.transform.position);

                if (distance < bestDistance)
                {
                    closest = pickup;
                    bestDistance = distance;
                }
            }

            // If we found a pickup return its position
            if (closest != null)
            {
                return closest.transform.position;
            }
            // Otherwise return world origin (null)
            // TODO find a better way to show null / failure that can't be confused with a pickup at world origin
            else
            {
                return Vector3.zero;
            }
        }
    }
}