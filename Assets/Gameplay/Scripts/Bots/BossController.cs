using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.AI;
using UnityEngine;
using UnityEngine.AI;

namespace TestGame.Bots
{
    public class BossController : AIAgent
    {
        public Controller controller;

        public NavMeshAgent NavMeshAgent;

        private BotCharacter m_Character;

        public int PathEvaluationCounter = 0;

        protected override IAIContext ProvideContext()
        {
            return this.m_Character;
        }


        protected override void Start()
        {
            this.NavMeshAgent = this.GetComponent<NavMeshAgent>();
            this.m_Character = this.GetComponent<BotCharacter>();
            this.m_Character.Target = GameObject.FindGameObjectWithTag("Player").transform;

            this.SetActions(controller.Actions);
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
