using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.AI;
using UnityEngine;
using UnityEngine.AI;

namespace TestGame.Bots
{
    public class BotController : AIAgent
    {
        public Controller regular_controller;
        public Controller tethered_controller;
 [HideInInspector]
        public Controller current_controller;

        public NavMeshAgent NavMeshAgent;

        private BotCharacter m_Character;

        public int PathEvaluationCounter = 0;

        protected override IAIContext ProvideContext()
        {
            return this.m_Character;
        }

        public void tether(bool isTethered=true){
            current_controller=isTethered?tethered_controller:regular_controller;
            this.SetActions(current_controller.Actions);
        }

        protected override void Start()
        {
            this.NavMeshAgent = this.GetComponent<NavMeshAgent>();
            this.m_Character = this.GetComponent<BotCharacter>();
            this.m_Character.Target = GameObject.FindGameObjectWithTag("Player").transform;
            tether(false);

            //
            //  BUG:
            //      Agent jumps large distance when spawned.
            //
            //      This solves that problem.
            // 
            //  More:
            //      http://answers.unity3d.com/questions/771908/navmesh-issue-with-spawning-players.html
            //
            this.NavMeshAgent.enabled = true;
            this.NavMeshAgent.speed = this.m_Character.Speed;

            base.Start();
        }
    }
}
