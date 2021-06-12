using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.AI;
using UnityEngine;

namespace TestGame.Bots.Scorers
{
    /// <summary>
    /// This scorer promotes action that uses hit-and-run.
    /// </summary>
     [CreateAssetMenu(fileName ="MeleeHitAndRun.scorer.asset", menuName ="AI/Scorers/Melee Hit And Run")]
    public class MeleeHitAndRun : AIScorer
    {
        /// <summary>
        /// Expected hit-and-run cooloff timer treshold.
        /// </summary>
        public float Treshold;

        /// <summary>
        /// Score returned when hit-and-run cooloff timer is below treshold.
        /// </summary>
        public float PositiveScore;

        /// <summary>
        /// Score returned when hit-and-run cooloff timer is above treshold.
        /// </summary>
        public float NegativeScore;

        public override float Score(IAIContext context)
        {
            var bot = context as BotCharacter;

            //
            // Bot must support this feature.
            if (bot.MeleeHitAndRun)
            {
                if (bot.HitAndRunTimer < this.Treshold)
                {
                    //
                    // Cooloff timer is below treshold.
                    //
                    return this.PositiveScore;
                }

                return this.NegativeScore;
            }

            return 0.0F;
        }
    }
}
