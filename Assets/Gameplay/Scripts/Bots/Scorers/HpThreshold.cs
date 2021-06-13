using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.AI;
using UnityEngine;

namespace TestGame.Bots.Scorers
{
    /// <summary>
    /// Fixed scorer. Just returns fixed score.
    /// </summary>
     [CreateAssetMenu(fileName = "HpThreshold.scorer.asset", menuName = "AI/Scorers/HpThreshold")]
public class HpThreshold : AIScorer
    {
        //
        // Fixed score value.
        //
        public float ScoreIfMore = 0.0F;
        public float ScoreIfLess = 100.0F;
        public float Threshold = 0.5F;
        public bool UsePercent=true;
        public override float Score(IAIContext context)
        {
            var bot = context as BotCharacter;
            return (UsePercent?(bot.Energy/bot.EnergyMax):bot.Energy)>Threshold?ScoreIfMore:ScoreIfLess;
        }
    }
}
