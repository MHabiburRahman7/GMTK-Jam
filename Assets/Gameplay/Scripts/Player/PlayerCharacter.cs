using TestGame.Core;
using TestGame.Gameplay;
using UnityEngine;

namespace TestGame.Player
{
    public class PlayerCharacter : CharacterBase
    {
        public PlayerController Controller;
        public AudioClip hurtSound;
        public AudioClip deathSound;

        private AudioSource audioSource;

        public void Start()
        {
            this.Controller = this.GetComponent<PlayerController>();
            audioSource = this.GetComponent<AudioSource>();
        }
        
        public override void TakeDamage(float damage, bool ignoreSound = false)
        {
            base.TakeDamage(damage);
            if (!ignoreSound) audioSource.PlayOneShot(hurtSound);

            //
            // When player takes damage, notify HUD to update itself.
            //
            HudController.Instance.NotifyUpdateHealth();
        }

        protected override void OnCharacterDied()
        {
            audioSource.PlayOneShot(deathSound);
            //
            // Notify game character that player passe away.
            //
            GameController.Instance.PlayerPassedAway();
        }
    }
}
