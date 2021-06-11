using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TestGame.Core
{
    /// <summary>
    /// Base character class.
    /// </summary>
    public class CharacterBase : MonoBehaviour
    {
        [Header("Configuration")]
        public float Energy;
        public float EnergyMax;

        public bool IsAlive = true;

        public virtual void TakeDamage(float damage)
        {
            if (this.IsAlive)
            {
                //
                // Compute damage on armor and health.
                //
                var healthDamage = Mathf.Min(this.Energy, damage);

                //
                // Update proper states.
                //
                this.Energy -= healthDamage;
                
                if (this.Energy <= 0.0F)
                {
                    //
                    // Notify that character died. Additionally, latch to not die again.
                    //
                    this.IsAlive = false;
                    this.OnCharacterDied();
                }
            }
        }

        /// <summary>
        /// This method is called when character dies.
        /// </summary>
        protected virtual void OnCharacterDied()
        {
        }

        public void AddHealth(float points)
        {
            this.Energy = this.Energy + points;
        }
    }
}
