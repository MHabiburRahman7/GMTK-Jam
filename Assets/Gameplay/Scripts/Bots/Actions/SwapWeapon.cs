using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.AI;
using UnityEngine;

namespace TestGame.Bots.Actions
{
    /// <summary>
    /// Idle action. Bot just wanders around map.
    /// </summary>
    [CreateAssetMenu(fileName ="SwapWeapon.action.asset", menuName = "AI/Actions/SwapWeapon")]

    public class SwapWeapon : AIAction
    {
        public Weapons.Weapon weapon;
        public override bool Execute(IAIContext context)
        {
            MonoBehaviour.print("weapon swap");
            var bot = context as BotCharacter;
            bot.Weapon = weapon;
            return true;
        }
        public override float Score(IAIContext context)
        {
            var bot = context as BotCharacter;
            //do not retrigger
            return bot.Weapon == weapon? float.MinValue:base.Score(context);
        }
    }
}
