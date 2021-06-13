using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestGame.AI;

namespace TestGame.Bots.Scorers
{

    [CreateAssetMenu(fileName ="Cooldown.scorer.asset", menuName ="AI/Scorers/Cooldown")]
        public class Cooldown : AIScorer
    {
        /// <summary>
        /// Duration in seconds.
        /// </summary>
        public float Duration;

        /// <summary>
        /// Score for distance below treshold.
        /// </summary>
        public float PositiveScore;

        /// <summary>
        /// Score for distance above reshold.
        /// </summary>
        public float NegativeScore;

       // public float CurrentDuration=0;
       // public float lastTime = 0;
        public override float Score(IAIContext context)
        {

            var bot = context as BotCharacter;

            if (bot.Cd_CurrentDuration>0){
                bot.Cd_CurrentDuration -= Time.time- bot.Cd_lastTime;
                bot.Cd_lastTime = Time.time;
                return NegativeScore;
            }
            else{
                bot.Cd_CurrentDuration = 0;
                return PositiveScore;
            }
        }

        public override void OnAction(IAIContext context){

            var bot = context as BotCharacter;
            bot.Cd_CurrentDuration = Duration;
            bot.Cd_lastTime = Time.time;
        }

    }
}
