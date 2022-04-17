using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hoover
{
    public class LevelCompleteUI : Panel
    {
        [SerializeField] Text[] scoreText; // 0 = 1st place, 1 = 2nd place etc

        void Awake()
        {
            // Subscribe to events
            GameManager.onLevelComplete += UpdateUI;
            GameManager.onLevelComplete += Show;
        }

        void UpdateUI()
        {
            // Find every player with scoring capability
            // TODO this should be gotten from a cache somewhere
            PlayerScore[] playerScores = FindObjectsOfType<PlayerScore>();

            // Sort player scores into order (largest to smallest)
            // Source - https://forum.unity.com/threads/how-to-reorder-lists.991499/
            Array.Sort<PlayerScore>(playerScores, (a, b) => { return b.CurrentScore.CompareTo(a.CurrentScore); });

            // Update player scores UI with the new sorted order
            for (int i = 0; i < scoreText.Length; i++)
            {
                scoreText[i].text = playerScores[i].PlayerName + ": " + playerScores[i].CurrentScore;
            }
        }

        void OnDestroy()
        {
            // Unsubscribe from events
            GameManager.onLevelComplete -= UpdateUI;
            GameManager.onLevelComplete -= Show;
        }
    }
}