using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame
{
    public class RopeJoint : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            gameObject.GetComponent<CharacterJoint>().connectedBody = gameObject.transform.parent.GetComponent<Rigidbody>();
        }

    }
}
