using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hoover
{
    public class PickupManager : MonoBehaviour
    {
        static readonly int startingPickupsToSpawn = 100;

        [SerializeField] Pickup dirtPrefab;
        [SerializeField] Transform dirtFolder; // Keep things neat and tidy in the heirarchy

        int currentPickupCount;

        // Events
        public delegate void OnAllCollected();
        public static event OnAllCollected onAllCollected;

        void Awake()
        {
            // Subscribe to events
            ScorePickup.onPickedUp += ReducePickupCount;
        }

        void Start()
        {
            // Spawn dirt pickups
            for (int i = 0; i < startingPickupsToSpawn; i++)
            {
                // The dirt prefab will automatically randomly place itself (in its Start method)
                Instantiate(dirtPrefab, transform.position, Quaternion.identity, dirtFolder);
            }

            currentPickupCount = startingPickupsToSpawn;
        }

        void ReducePickupCount()
        {
            // Don't allow the level to end twice
            if (currentPickupCount <= 0) return;

            currentPickupCount--;

            // Have all the pickups been collected?
            if (currentPickupCount <= 0)
            {
                if (onAllCollected != null)
                    onAllCollected();
            }
        }

        void OnDestroy()
        {
            // Unsubscribe from events
            ScorePickup.onPickedUp -= ReducePickupCount;
        }
    }
}