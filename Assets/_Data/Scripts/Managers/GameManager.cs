using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hoover
{
    public class GameManager : MonoBehaviour
    {
        public static GameState GameState { get; private set; }

        // Events
        public delegate void OnLevelComplete();
        public static event OnLevelComplete onLevelComplete;

        void Awake()
        {
            // Subscribe to events
            PlayerManager.onPlayersEnabled += StartLevel;
            PickupManager.onAllCollected += LevelComplete;

            SetState(GameState.INTRO);
        }

        void SetState(GameState newState)
        {
            GameState = newState;

            switch (newState)
            {
                case GameState.INTRO:

                    Time.timeScale = 0;

                    break;
                case GameState.PLAYING:

                    Time.timeScale = 1;

                    break;
                case GameState.LEVEL_COMPLETE:

                    Time.timeScale = 0;

                    if (onLevelComplete != null)
                        onLevelComplete();

                    break;
                default:
                    break;
            }
        }

        void StartLevel()
        {
            SetState(GameState.PLAYING);
        }

        void LevelComplete()
        {
            SetState(GameState.LEVEL_COMPLETE);
        }

        void OnDestroy()
        {
            // Unsubscribe from events
            PlayerManager.onPlayersEnabled -= StartLevel;
            PickupManager.onAllCollected -= LevelComplete;
        }
    }

    public enum GameState { INTRO, PLAYING, LEVEL_COMPLETE }
}