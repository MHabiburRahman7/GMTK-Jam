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
        public Rigidbody2D rigidBody;
        public float angleChangingSpeed;

        public int collisionMask=1<<6;
        private Vector2 prevPos;
        private float distance=0.0F;
        
        void FixedUpdate()
        {
            if (homing)
            {

                Vector2 direction = (Vector2)target.position - rigidBody.position;
                direction.Normalize();
                float rotateAmount = Vector3.Cross(direction, transform.up).z;
                rigidBody.angularVelocity = -angleChangingSpeed * rotateAmount;
            }

            rigidBody.velocity = transform.up * speed;
            
            var distanceDuringUpdate=  Vector2.Distance(rigidBody.position, prevPos);
            distance += distanceDuringUpdate;

            RaycastHit hit;
            if (Physics.Raycast(prevPos, rigidBody.position - prevPos,out hit, distanceDuringUpdate, collisionMask))
            {
                OnCollide(hit.collider);
            }
            prevPos = rigidBody.position;

            if (distance>=range)
            {
                OnExpire();
            }
            
        }
        void OnExpire() { }
        void OnCollide(Collider collider) { }
        
    }
}
