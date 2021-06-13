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
     [CreateAssetMenu(fileName ="RandomScorer.scorer.asset", menuName = "AI/Scorers/Random Scorer")]
public class RandomScorer : AIScorer
    {
        //
        // Fixed score value.
        //
        public float MinScore = 0.0F;
        public float MaxScore = 100.0F;

        public override float Score(IAIContext context)
        {
            
            return UnityEngine.Random.Range(MinScore, MaxScore);
        }
    }
}
