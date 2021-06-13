using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestGame.Core;
using UnityEngine;

namespace TestGame.Weapons
{
    /// <summary>
    /// Implements bullet object logic.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]
    public class Bullet : MonoBehaviour
    {
        //
        // Damage caused by this bullet object.
        //
        public float Damage;

        //
        // Bullet pushing force.
        //
        public float Force;

        public float explosionRadius = 0f;
        public bool isEMP = false;
        public GameObject EMPPrefab;
        public GameObject explosionPrefab;

        //
        // A rigid body to push.
        //
        private Rigidbody m_RigidBody;

        //
        // A tracer object.
        //
        public GameObject Tracer;

        private void Start()
        {
            //
            // Capture rigid body and push it.
            //
            this.m_RigidBody = this.GetComponent<Rigidbody>();

            this.m_RigidBody.AddRelativeForce(0.0F, 0.0F, this.Force);
        }

        private void OnDestroy()
        {
            //
            // When destroyed, destroy tracer too.
            //
            if (this.Tracer != null)
            {
                this.Tracer.SetActive(false);
                Destroy(this.Tracer);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "bullet") 
                return;
            
            if (explosionRadius <= 0) {
                var character = collision.gameObject.GetComponent<CharacterBase>();
                if (character != null)
                {
                    //
                    // Cause damage to character.
                    //
                    character.TakeDamage(this.Damage);
                }
            } else {
                if (isEMP) {
                    //spawn an area that slows enemies down
                    Instantiate(EMPPrefab, collision.GetContact(0).point, EMPPrefab.transform.rotation);
                } else {
                    Instantiate(explosionPrefab, collision.GetContact(0).point, explosionPrefab.transform.rotation);
                    //get all objects in the radius of the explosion
                    Collider[] inRadius = Physics.OverlapSphere(collision.GetContact(0).point, explosionRadius);
                    foreach(Collider touched in inRadius) {
                        //if the current object is a character, deal damage
                        var character = touched.gameObject.GetComponent<CharacterBase>();
                        if (character != null) {    
                            character.TakeDamage(this.Damage);
                        }
                    }
                }
            }

            Destroy(this.gameObject);
        }
    }
}
