using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For human player movement
namespace Hoover
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 4f;

        // Cache
        CharacterController controller;
        PlayerScore playerScore; // Required for the player number

        void Awake()
        {
            // Cache
            controller = GetComponent<CharacterController>();
            playerScore = GetComponent<PlayerScore>();
        }

        void Update()
        {
            // Movement
            if (controller && playerScore)
            {
                // Normalize to prevent diagonal movement being faster. See https://www.youtube.com/watch?v=YMwwYO1naCg.
                Vector3 move = new Vector3(Input.GetAxis("Horizontal-P" + playerScore.PlayerNumber), 0f, Input.GetAxis("Vertical-P" + playerScore.PlayerNumber)).normalized;
                controller.SimpleMove(move * moveSpeed);
            }
        }
    }
}