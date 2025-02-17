﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Core;
using TestGame.Player;
using TestGame.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace TestGame.Gameplay
{
    /// <summary>
    /// Provides way of updating gameplay HUD.
    /// </summary>
    public class HudController : MonoBehaviour
    {
        #region Component Properties
        [Header("Player")]
        public PlayerCharacter Player;
        public PlayerController PlayerController;

        [Header("GameController")]
        public GameController GameController;

        [Header("Energy Panel")]
        public Image EnergyBar;
        public Text EnergyValue;

        [Header("Current Weapon Panel")]
        public Image WeaponImage;

        [Header("Wave Panel")]
        public Text WaveCurrent;
        public Text WaveBotsCount;

        [Header("Wave Countdown")]
        public GameObject WaveCountdownBackground;
        public Text WaveCountdownText;

        [Header("Score Panel")]
        public Text ScoreValue;
        public Text ScoreTotalDown;

        #endregion

        #region Gametone
        private static HudController s_Instance = null;
        public static HudController Instance
        {
            get
            {
                return HudController.s_Instance;
            }
        }

        private void Awake()
        {
            Debug.Assert(HudController.s_Instance == null);
            HudController.s_Instance = this;
        }

        private void OnDestroy()
        {
            Debug.Assert(HudController.s_Instance == this);
            HudController.s_Instance = null;
        }
        #endregion

        private bool m_UpdateHealth = true;
        private bool m_UpdateWave = true;

        public void UpdateHealth(CharacterBase character)
        {
            //
            // Update health progress bar and values.
            //
            {
                var progress = character.Energy / character.EnergyMax;
                var fillAmount = Mathf.Clamp01(progress);

                if (fillAmount <= 0.3f)
                    this.EnergyBar.color = new Color(255f, 0f, 0f, 255f);
                else
                    this.EnergyBar.color = new Color(48f, 237f, 240f, 255f);

                this.EnergyBar.fillAmount = fillAmount;

                //
                // Update health value.
                //

                this.EnergyValue.text = String.Format("{0}/{1}", (int)character.Energy, (int)character.EnergyMax);
            }
        }

        public void UpdateWave(int currentWave, int enemiesLeft, int enemiesTotal)
        {
            var waveController = WaveController.Instance;

            this.WaveCurrent.text = String.Format("Wave {0}", waveController.Wave);
            this.WaveBotsCount.text = String.Format("{0}/{1}", waveController.EnemiesAlive, waveController.Enemies);
            this.ScoreValue.text = String.Format("Score: {0}", waveController.Score);
            this.ScoreTotalDown.text = String.Format("Total: {0}", waveController.TotalEnemiesDown);
        }

        public void UpdateWeapon(Weapon weapon)
        {
           this.WeaponImage.sprite = weapon.AvatarImage;
        }

        public void UpdateWaveCountdown(bool show, bool waveNumber, int value)
        {
            this.WaveCountdownBackground.SetActive(show);
            if (show)
            {
                this.WaveCountdownText.text = String.Format(
                    waveNumber
                    ? "Wave {0}"
                    : "{0}", value);
            }
        }

        public void NotifyUpdateHealth()
        {
            this.m_UpdateHealth = true;
        }

        public void NotifyUpdateWave()
        {
            this.m_UpdateWave = true;
        }

        private void LateUpdate()
        {
            if (this.m_UpdateHealth)
            {
                this.UpdateHealth(this.Player);
            }

            if (this.PlayerController.CurrentWeapon != null)
                this.UpdateWeapon(this.PlayerController.CurrentWeapon);

            if (this.m_UpdateWave)
            {
                this.m_UpdateWave = false;

                var waveController = WaveController.Instance;

                this.UpdateWave(
                    waveController.Wave,
                    waveController.EnemiesAlive,
                    waveController.Enemies);
            }
        }
    }
}
