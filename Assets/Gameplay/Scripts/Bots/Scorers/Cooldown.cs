using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestGame.AI;
using System;

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

        //selects which timer to use
        public int Selector;
        public override float Score(IAIContext context)
        {

            var bot = context as BotCharacter;
            if (bot.Cd_CurrentDuration.Length <= Selector)
            {
                Array.Resize(ref bot.Cd_CurrentDuration, Selector+1);
                Array.Resize(ref bot.Cd_lastTime, Selector+1);
            }

            if (bot.Cd_CurrentDuration[Selector] > 0){
                bot.Cd_CurrentDuration[Selector] -= Time.time - bot.Cd_lastTime[Selector];
                bot.Cd_lastTime[Selector] = Time.time;
                return NegativeScore;
            }
            else{
                bot.Cd_CurrentDuration[Selector] = 0;
                return PositiveScore;
            }
        }

        public override void OnAction(IAIContext context){

            var bot = context as BotCharacter;
            bot.Cd_CurrentDuration[Selector] = Duration;
            bot.Cd_lastTime[Selector] = Time.time;
        }

    }
}
