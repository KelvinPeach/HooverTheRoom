using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hoover
{
    public abstract class Pickup : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Use(other.gameObject);

                Destroy(gameObject);
            }
        }

        protected abstract void Use(GameObject other);
    }
}