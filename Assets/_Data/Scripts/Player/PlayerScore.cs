using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hoover
{
    public class PlayerScore : MonoBehaviour
    {
        public int PlayerNumber { get { return playerNumber; } }
        public string PlayerName { get; private set; }
        public Color PlayerColor { get { return playerColor; } }
        public int CurrentScore { get; private set; }

        [SerializeField] int playerNumber;
        [SerializeField] Color playerColor;

        // Events
        public delegate void OnScoreChanged(PlayerScore playerWhoseScoreChanged, int newScore);
        public static event OnScoreChanged onScoreChanged;

        void OnEnable()
        {
            // If the AI component is active, set the name to CPU1
            if (GetComponent<PlayerAi>().enabled)
            {
                PlayerName = "CPU" + PlayerNumber;
            }
            // P1
            else
            {
                PlayerName = "P" + PlayerNumber;
            }
        }

        public void AddScore(int scoreToAdd)
        {
            CurrentScore += scoreToAdd;

            if (onScoreChanged != null)
                onScoreChanged(this, CurrentScore);
        }
    }
}