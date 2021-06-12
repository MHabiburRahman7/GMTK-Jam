using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.AI;
using UnityEngine;
using UnityEngine.AI;

namespace TestGame
{
    [Serializable]
    [CreateAssetMenu(fileName ="controller.behavior.asset", menuName ="AI/Controller")]
    public class Controller : ScriptableObject
    {
        public float Range1;
        public float Range2; 
        [SerializeField]
        public AIAction[] Actions ;//= new AIAction[]{};
        public virtual void OnEnable(){
            if (Actions==null)
                Actions=new AIAction[]{};
        }
        //public virtual AIAction[] getActions(){return new AIAction[]{};}
    }
}
