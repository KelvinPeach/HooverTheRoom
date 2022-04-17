using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hoover
{
    public class ScorePickup : Pickup
    {
        static readonly float maxPlaceDistance = 20f;

        [SerializeField] int value = 1;

        // Events
        public delegate void OnPickedUp();
        public static event OnPickedUp onPickedUp;

        void Start()
        {
            // Place randomly on the NavMesh (somewhere players can actually reach)
            // Source - https://answers.unity.com/questions/475066/how-to-get-a-random-point-on-navmesh.html
            Vector3 randomDirection = Random.insideUnitSphere * maxPlaceDistance;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomDirection, out hit, maxPlaceDistance, 1))
            {
                transform.position = hit.position;
            }
            // If the randomly chosen position was invalid then place at world origin as a failsafe
            else
            {
                transform.position = Vector3.zero;
                Debug.LogWarning("NavMesh SamplePosition failed for " + name);
            }
        }

        protected override void Use(GameObject other)
        {
            PlayerScore playerScore = other.GetComponent<PlayerScore>();

            if (playerScore)
            {
                playerScore.AddScore(value);
            }

            if (onPickedUp != null)
                onPickedUp();
        }
    }
}