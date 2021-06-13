using System.Collections;
using System.Collections.Generic;
using TestGame.Bots;
using TestGame.Player;
using UnityEngine;

namespace TestGame
{
    public class Exploder : MonoBehaviour
    {
        public float Delay;
        private bool isExploding=false;
        public float range;
        public float damage;
        public GameObject smoke;
        public GameObject explosion;
        // Update is called once per frame
        void Update()
        {
            if (isExploding)
            {
                return;
            }

            var bot = this.gameObject.GetComponent("BotCharacter") as BotCharacter;
            if (bot == null) return;
            var player = GameObject.Find("Player"); 
            //
            // Just return distance to player. Attack action will kick in eventually.
            //
            var distance = Vector3.Distance(player.transform.position, bot.transform.position);

            //if player close --> code needed{
            if (distance<range)
            {
                isExploding = true;
                StartCoroutine(DelayedExplosion(bot,player));
                bot.Controller.DisableAI();
                //TODO: check it works
            }
            //stop AI and movement --> code needed
        }
        public IEnumerator DelayedExplosion(BotCharacter bot,GameObject player)
        {
            if(smoke!=null) Instantiate(smoke, this.transform);

            yield return new WaitForSeconds(Delay);
            if (explosion != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    Instantiate(explosion, this.transform);
                }
            }
            bot.TakeDamage(float.MaxValue);
            var playerCharacter = player.GetComponent("PlayerCharacter") as PlayerCharacter;

            var distance = Vector3.Distance(player.transform.position, bot.transform.position);
            if (playerCharacter!=null&& distance < range)
                {
                    playerCharacter.TakeDamage(damage);
                }
            
            

            //TODO: explode --> ok, its a take damage
        }
    }
}

