using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.AI;
using TestGame.Core;
using UnityEngine;

namespace TestGame.Bots.Actions
{
    /// <summary>
    /// Bot tries to perform melee attack on target.
    /// </summary>
    [CreateAssetMenu(fileName ="Charge.action.asset", menuName ="AI/Actions/Charge")]

    public sealed class Charge : AIAction
    {
        public float Duration= 2F;
        //
        // Min angle to shoot.
        //
        public float MinAngle = 10.0F;

        //
        // Max rotation angle.
        //
        public float RotationAngle = 90.0F;
        public override void Execute(IAIContext context)
        {
            //
            // Extract bot character.
            //
            var bot = context as BotCharacter;

            //
            // Get target position.
            //
            var targetPosition = bot.Target.position;
            
            var agent = bot.Controller.NavMeshAgent;

            if(!bot.isCharging){
            //
            //
            //
            var lookDirection = Vector3.Normalize(targetPosition - bot.transform.position);

            //
            // Zero out UP direction.
            //
            lookDirection.y = 0.0F;

            
            //
            // Rotate bot towards target.
            //
            var lookAtTarget = Quaternion.LookRotation(lookDirection);
            bot.transform.rotation = Quaternion.RotateTowards(bot.transform.rotation, lookAtTarget, this.RotationAngle * this.Interval);

            //
            // Compute angle between target and forward.
            //
            var angleBetween = Vector3.Angle(lookDirection, bot.transform.forward);
            
            if (angleBetween <= this.MinAngle)
            {
                //
                // Shoot if target is in range.
                //
                
                bot.isCharging=true;
                agent.speed=bot.Speed*3;
                bot.Controller.m_EvaluationTimeout=Duration;
            }
            }else{
                

                agent.autoBraking = false;

                var botPosition = bot.transform.position;
                var distance = Vector3.Distance(targetPosition, botPosition);
                var inRange = distance < bot.MeleeAttackRange;

                //
                // This game gives 'soft-guarantee' that each execute action will execute at this interval...
                //
                // It's not preety at all...
                //
                bot.MeleeAttackTimer += this.Interval;

                //
                // It's not preety, but optimal.
                //
                if (inRange && bot.MeleeAttackTimer >= bot.MeleeAttackInterval)
                {
                    //
                    // We "hit" character this time. Count it as attack.
                    //
                    var player = bot.Target.gameObject.GetComponent<CharacterBase>();
                    bot.MeleeAttackTimer = -bot.MeleeAttackInterval;
                    
                    bot.Controller.m_EvaluationTimeout=-1;
                    
                    agent.speed=bot.Speed;
                    //
                    // Reset hit-and-run.
                    //
                    bot.HitAndRunTimer = 0.0F;

                    player.TakeDamage(bot.MeleeAttack);
                }
            }
            Debug.DrawRay(bot.transform.position, bot.transform.forward * 10, Color.red);
        }
    
    public override void OnEnter(IAIContext context)
        {
            base.OnEnter(context);

            var bot = context as BotCharacter;

            //
            // Bot updates rotation.
            //
            bot.Controller.NavMeshAgent.updateRotation = false;

            //
            // Reset path evaluation counter. Bot will reevalute path
            //
            bot.Controller.PathEvaluationCounter = 0;
        }

        public override void OnLeave(IAIContext context)
        {
            base.OnLeave(context);

            var bot = context as BotCharacter;

            //
            // Leave rotation update to navmesh agent.
            //
            bot.Controller.NavMeshAgent.updateRotation = true;
        }
    }
}
