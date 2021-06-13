using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TestGame.Gameplay
{
    public class PauseMenuController : MonoBehaviour
    {
        public GameObject ResumeButton;
        public GameObject GameOverObj;

        public void OnEnable()
        {
            var instance = GameController.Instance;
            if (instance != null)
            {
                if (instance.Player.Character.IsAlive)
                {
                    this.ResumeButton.SetActive(true);
                    this.GameOverObj.SetActive(false);
                }
                else {
                    this.ResumeButton.SetActive(false);
                    this.GameOverObj.SetActive(true);
                }

            }
        }

        public void ResumeGame()
        {
            Time.timeScale = 1.0F;
            this.gameObject.SetActive(false);
        }

        public void RestartGame()
        {
            Time.timeScale = 1.0F;
            this.SaveHighScore();
            SceneManager.LoadScene("Gameplay/Scenes/World");
        }

        public void ExitGame()
        {
            Time.timeScale = 1.0F;
            this.SaveHighScore();
            SceneManager.LoadScene("Menu/Scenes/MainMenu");
        }

        private void SaveHighScore()
        {
            //
            // Notify career about highscore.
            //
            Career.HighScore.Instance.TopKills = WaveController.Instance.TotalEnemiesDown;
            Career.HighScore.Instance.TopScore = WaveController.Instance.Score;
        }
    }
}
