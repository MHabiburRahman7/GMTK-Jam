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

        private float CurrentDuration=0;
        public override float Score(IAIContext context)
        {
            if(CurrentDuration>0){
                CurrentDuration-= Time.deltaTime;
            return NegativeScore;
            }
            else{
                CurrentDuration=0;
                return PositiveScore;
            }
        }

        public override void OnAction(IAIContext context){
            CurrentDuration=Duration;
        }

    }
}
