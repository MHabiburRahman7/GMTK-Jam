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
     [CreateAssetMenu(fileName = "SingleShot.scorer.asset", menuName = "AI/Scorers/SingleShot")]
public class SingleShot : AIScorer
    {
        //
        // Fixed score value.
        //
        public float ScoreBefore = 0.0F;
        public float ScoreAfter = -1000.0F;
        public bool HasTriggered = false;
        public override float Score(IAIContext context)
        {
            return HasTriggered?ScoreAfter:ScoreBefore;
            
        }
        public override void OnAction(IAIContext context)
        {
            base.OnAction(context);
            HasTriggered = true;
        }

    }
}
