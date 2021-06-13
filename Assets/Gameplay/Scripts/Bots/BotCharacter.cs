using System.Linq;
using TestGame.AI;
using TestGame.Core;
using TestGame.Gameplay;
using TestGame.Weapons;
using UnityEngine;
using UnityEngine.AI;

namespace TestGame.Bots
{
    public class BotCharacter : CharacterBase, IAIContext
    {
        [Header("Configuration")]
        //
        // Speed of bot.
        //
        public float Speed;

        //
        // Melee attack.
        //
        public float MeleeAttack;
        public float MeleeAttackRange;
        public float MeleeAttackInterval;
        public AudioClip[] deathSounds;

        public bool MeleeHitAndRun;

        //
        // Weapon
        //
        public Weapon Weapon;

        //
        // A target to attack.
        //
        public Transform Target;

        [Header("Shared State")]
        public BotController Controller;
        public float MeleeAttackTimer = 0.0F;
        public float HitAndRunTimer = 0.0F;

        private AudioSource botAudio;

        private void Awake()
        {
            this.Controller = this.GetComponent<BotController>();
            botAudio = GetComponent<AudioSource>();
        }

        protected override void OnCharacterDied()
        {
            base.OnCharacterDied();

            int index = Random.Range(0, deathSounds.Count() - 1);
            botAudio.PlayOneShot(deathSounds[index]);

            //
            // Notify wave controller that this bot died :(
            //
            WaveController.Instance.RaiseEnemyDied(this);

            //
            // Bot stops moving when dies.
            //
            this.Controller.NavMeshAgent.isStopped = true;

            //
            // Remove bot's gun.
            //
            if (this.Weapon != null)
            {
                //GameObject.Destroy(this.Weapon);
            }

            //
            // "Break" bot :)
            //
            GameObject.Destroy(this.Controller.NavMeshAgent);
            GameObject.Destroy(this.Controller.GetComponent<Collider>());
            GameObject.Destroy(this.Controller);
            GameObject.Destroy(this.gameObject, 5.0F);
        }

        private void Update()
        {
            this.HitAndRunTimer += Time.deltaTime;

            if (!this.IsAlive)
            {
                //
                // Everybody do the FLOP.
                //
                var targetRotation = Quaternion.AngleAxis(-90.0F, Vector3.right);
                this.transform.localRotation = Quaternion.RotateTowards(this.transform.localRotation, targetRotation, 90.0F * Time.deltaTime);
            }
        }

        public void Shoot()
        {
            if (this.Weapon != null)
            {
                //
                // Shoot weapon :)
                //
                this.Weapon.Shoot();
            }
        }

        public bool isCharging;
    }
}
