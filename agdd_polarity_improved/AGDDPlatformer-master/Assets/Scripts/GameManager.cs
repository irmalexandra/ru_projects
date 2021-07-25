using System;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AGDDPlatformer
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [Header("Players")]
        public PlayerController[] players;

        [Header("Level")]
        public PlayerGoal[] playerGoals;
        public bool timeStopped;
        public bool isGameComplete;
        public bool lastLevel;

            [Header("Level Transition")]
        public GameObject startScreen;
        public GameObject endScreen;
        public GameObject gameOverScreen;
        public float startScreenTime = 1.0f;
        public float endScreenDelay = 1.0f;
        public float endScreenTime = 1.0f;

        [Header("Audio")]
        public AudioSource source;
        public AudioClip winSound;
        public AudioClip doorUnlock;
        void Awake()
        {
            instance = this;
            if (playerGoals.Length == 0)
            {
                playerGoals = FindObjectsOfType<PlayerGoal>();
            }
        }

        IEnumerator Start()
        {
            timeStopped = true;

            endScreen.SetActive(false);
            gameOverScreen.SetActive(false);

            startScreen.SetActive(true);

            yield return new WaitForSeconds(startScreenTime);

            startScreen.SetActive(false);

            timeStopped = false;
        }

        void Update()
        {
            if (isGameComplete)
            {
                if (Input.GetButtonDown("Reset"))
                {
                    ResetGame();
                }
            }

            if (Input.GetKey("1"))
            {
                SceneManager.LoadScene(0);
            }
            if (Input.GetKey("2"))
            {
                SceneManager.LoadScene(1);
            }
            if (Input.GetKey("3"))
            {
                SceneManager.LoadScene(2);
            }
            if (Input.GetKey("4"))
            {
                SceneManager.LoadScene(3);
            }
            if (Input.GetKey("5"))
            {
                SceneManager.LoadScene(4);
            }
            if (Input.GetKey("6"))
            {
                SceneManager.LoadScene(5);
            }
            if (Input.GetKey("7"))
            {
                SceneManager.LoadScene(6);
            }
            if (Input.GetKey("8"))
            {
                SceneManager.LoadScene(7);
            }

            if (timeStopped)
                return;

            /* --- Check Player Goals --- */

            bool allGoalsSatisfied = true;
            foreach (PlayerGoal playerGoal in playerGoals)
            {
                if (!playerGoal.isSatisfied)
                {
                    allGoalsSatisfied = false;
                    break;
                }
            }

            if (allGoalsSatisfied)
            {
                source.PlayOneShot(winSound);
                StartCoroutine(LevelCompleted());
            }

            if (Input.GetButtonDown("Reset"))
            {
                ResetLevel();
            }
        }

        IEnumerator LevelCompleted()
        {
            timeStopped = true;

            yield return new WaitForSeconds(endScreenDelay);

            endScreen.SetActive(true);

            yield return new WaitForSeconds(endScreenTime);

            if (!lastLevel)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            }
            else
            {
                isGameComplete = true;
                gameOverScreen.SetActive(true);
            }
        }

        void ResetGame()
        {
            SceneManager.LoadScene(0);
        }

        public void ResetLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void PlayUnlockDoorSound()
        {
            source.PlayOneShot(doorUnlock);
        }

        void ResetPlayers()
        {
            foreach (PlayerController player in players)
            {
                player.ResetPlayer();
            }
        }
    }
}
