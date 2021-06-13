using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestGame.Core;
namespace TestGame
{
    public class DamageEffect : Effect
    {
        float amount;
        public override void Apply(GameObject target)
        {
            var character = (target.GetComponent("CharacterBase") as CharacterBase);
            character.TakeDamage(amount);
        }

    }
}
