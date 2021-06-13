using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TestGame.AI
{
    /// <summary>
    /// Represents base AI action.
    /// </summary>
    [Serializable]
    public abstract class AIAction : ScriptableObject
    {
        public AIScorer[] Scorers;

        public float Interval;

        /// <summary>
        /// Evaluates score of action for specific context.
        /// </summary>
        /// <param name="context">A context.</param>
        /// <returns>Score</returns>
        public virtual float Score(IAIContext context)
        {
            //
            // Assume initial score.
            //
            var result = 0.0F;

            for (int i = 0; i < this.Scorers.Length; ++i)
            {
                //
                // Evaulate all scorers for this action.
                //
                result += this.Scorers[i].Score(context);
            }

            //
            // And return it.
            //
            return result;
        }

        /// <summary>
        /// Executes action for context.
        /// </summary>
        /// <param name="context">A context.</param>
        /// <returns>true if scorer.OnAction should be executed</returns>
        public abstract bool Execute(IAIContext context);

        /// <summary>
        /// Called when AI enters this action.
        /// </summary>
        /// <param name="context">A context.</param>
        public virtual void OnEnter(IAIContext context)
        {
        }

        /// <summary>
        /// Called when AI leaves this action.
        /// </summary>
        /// <param name="context">A context.</param>
        public virtual void OnLeave(IAIContext context)
        {
        }
    }
}
