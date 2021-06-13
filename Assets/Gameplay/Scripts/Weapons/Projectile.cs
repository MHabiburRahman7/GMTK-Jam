using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame
{
    public class Projectile : MonoBehaviour
    {
        public bool homing;
        public float speed;
        public float range;


        public Transform target;
        public Rigidbody rigidBody;
        public float angleChangingSpeed;

        //public int collisionMask=1<<6;
        private Vector3 prevPos;
        private float distance = 0.0F;

        void FixedUpdate()
        {
            if (homing)
            {

                Vector3 direction = target.position - rigidBody.position;
                direction.Normalize();
                float rotateAmount = Vector3.Cross(direction, transform.up).z;
                rigidBody.angularVelocity = new Vector3(0, 0, -angleChangingSpeed * rotateAmount);
            }

            rigidBody.velocity = transform.up * speed;

            var distanceDuringUpdate = Vector3.Distance(rigidBody.position, prevPos);
            distance += distanceDuringUpdate;

            //RaycastHit hit;
            //if (Physics.Raycast(prevPos, rigidBody.position - prevPos,out hit, distanceDuringUpdate, collisionMask))
            //{
            //   OnCollide(hit.collider);
            //}
            prevPos = rigidBody.position;

            if (range > 0 && distance >= range)
            {
                OnExpire();
            }

        }


        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
                OnCollide(collision.collider);
        }
        void OnExpire() { Destroy(this); }
        void OnCollide(Collider collider)
        {

        }

    }
}
