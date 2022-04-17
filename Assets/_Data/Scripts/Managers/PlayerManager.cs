using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hoover
{
    public class PlayerManager : MonoBehaviour
    {
        public PlayerScore[] Players { get { return players; } }

        [SerializeField] PlayerScore[] players;

        // Events
        public delegate void OnPlayersEnabled();
        public static event OnPlayersEnabled onPlayersEnabled;

        public void EnablePlayers(int humanPlayers)
        {
            // Enable human players
            for (int i = 0; i < humanPlayers; i++)
            {
                // The prefab is setup for humans by default. No changes needed.
                players[i].gameObject.SetActive(true);
            }

            // Enable computer players
            for (int i = humanPlayers; i < players.Length; i++)
            {
                // Disable human input
                players[i].GetComponent<PlayerMovement>().enabled = false;

                // Enable AI
                players[i].GetComponent<PlayerAi>().enabled = true;

                players[i].gameObject.SetActive(true);
            }

            // Start the game
            if (onPlayersEnabled != null)
                onPlayersEnabled();
        }
    }
}