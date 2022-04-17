using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hoover
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] PlayerScore owner;
        [SerializeField] Text nameText;
        [SerializeField] Text scoreText;

        void Awake()
        {
            // Subscribe to events
            PlayerScore.onScoreChanged += UpdateUI;
        }

        void Start()
        {
            // Set player name text and colour
            if (nameText && owner)
            {
                nameText.text = owner.PlayerName;
                nameText.color = owner.PlayerColor;
            }

            // Set score text to the player's colour
            if (scoreText && owner)
                scoreText.color = owner.PlayerColor;
        }

        void UpdateUI(PlayerScore playerWhoseScoreChanged, int newScore)
        {
            // Did our score change or was it another player?
            if (playerWhoseScoreChanged == owner)
            {
                // Update UI
                if (scoreText)
                    scoreText.text = newScore.ToString();
            }
        }

        void OnDestroy()
        {
            // Unsubscribe from events
            PlayerScore.onScoreChanged -= UpdateUI;
        }
    }
}