using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Weapons
{
    //
    // Considered weapons:
    //
    //                   | dmg       | firerate      | reload    
    //  1. Pistol        | 15        | 120/m         | 2.0s
    //  2. SMG           | 25        | 850/m         | 1.5s
    //  3. Assault Rifle | 35        | 500/m         | 3.0s
    //  4. Sniper Rifle  | 90-150    | 30/m          | 4.5s
    //

    //
    // Weapon types.
    //
    public enum WeaponType
    {
        Pistol,
        Submachine,
        Assault,
        Sniper,
    }

    /// <summary>
    /// Implementes weapon logic.
    /// </summary>
    public class Weapon : MonoBehaviour
    {
        [Header("Weapon Stats")]
        public float energyPerShot;
        public WeaponType WeaponType;
        public float Spread;
        public float Damage;
        public float FireInterval;

        [Header("Weapon Avatar")]
        public Sprite AvatarImage;

        [Header("Prefabs")]
        public ParticleSystem MuzzleFlash;
        public GameObject BulletTracer;
        public Bullet Bullet;

        [Header("Sockets")]
        public Transform Ejector;

        public void SpawnAmmo(float spread)
        {
            //
            // Spawn bullet and tracer
            //
            {
                var projectile = Instantiate(this.Bullet, this.Ejector.transform.position, this.Ejector.transform.rotation);
                var bullet = projectile.GetComponent<Bullet>();

                var randomSpread = UnityEngine.Random.Range(-spread, spread);

                var debugbefore = projectile.transform.forward.normalized;
                
                projectile.transform.RotateAround(projectile.transform.position, Vector3.up, randomSpread);

                //
                // Initialize bullet.
                //
                bullet.Force = 1000.0F;
                bullet.Damage = this.Damage;

                //
                // And spawn tracer.
                //
                var tracer = Instantiate(this.BulletTracer, this.Ejector.transform.position, this.Ejector.transform.rotation);
                tracer.transform.RotateAround(projectile.transform.position, Vector3.up, randomSpread);
                bullet.Tracer = tracer;
            }

            //
            // Make some effects
            //
            if (this.MuzzleFlash != null)
            {
                GameObject.Instantiate(this.MuzzleFlash, this.Ejector.transform.position, this.Ejector.transform.rotation);
            }
        }

        //
        // Inter-shoot timeout timer.
        //
        private float m_Timer;
        private TestGame.Player.PlayerCharacter player;

        private void OnEnable()
        {
            this.m_Timer = 0.0F;
            player = FindObjectOfType<TestGame.Player.PlayerCharacter>();
        }

        private void Update()
        {
            this.m_Timer += Time.deltaTime;
        }

        public void Shoot()
        {
            if (this.m_Timer >= this.FireInterval)
            {
                //
                // Weapon can shoot when it's in normal state (not emtpy or not reloading) and some time passed since last shoot.
                //
                this.m_Timer = 0.0F;

                //
                // Spawn bullet.
                //
                this.SpawnAmmo(this.Spread);
                player.Energy -= energyPerShot;
            }
        }
    }
}