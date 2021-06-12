using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace TestGame.AI
{
    /// <summary>
    /// Represents scoring AI object.
    /// </summary>
    [Serializable]
    public abstract class AIScorer : ScriptableObject
    {
        /// <summary>
        /// Evaluates score qualifiers.
        /// </summary>
        /// <param name="context">A context.</param>
        /// <returns>Score</returns>
        public abstract float Score(IAIContext context);
        public virtual void OnAction(IAIContext context){}
    }
}
