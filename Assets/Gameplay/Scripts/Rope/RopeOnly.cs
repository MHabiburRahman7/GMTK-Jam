using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame
{
    public class RopeOnly : MonoBehaviour
    {
        //Store RopeParent Rigibody component
        internal Rigidbody RBody;
        // Use this for initialization
        internal void Start()
        {
            //Add Rigibody component to RopeParent
            this.gameObject.AddComponent<Rigidbody>();
                 //Get the RigiParent Rigibody component and assign it to RBody
            this.RBody = this.gameObject.GetComponent<Rigidbody>();
            this.RBody.isKinematic = true;
                 //RopeParent number of sub-objects
                 // Add a Hinge Joint component to each child object
            int childcount = this.transform.childCount;
            for (int i = 0; i < childcount; i++)
            {
                Transform t = this.transform.GetChild(i);
                t.gameObject.AddComponent<HingeJoint>();

                HingeJoint hinge = t.gameObject.GetComponent<HingeJoint>();
                hinge.connectedBody = i == 0 ? this.RBody : this.transform.GetChild(i - 1).GetComponent<Rigidbody>();
                hinge.useSpring = true;
                hinge.enableCollision = true;
            }
        }

    }
}
