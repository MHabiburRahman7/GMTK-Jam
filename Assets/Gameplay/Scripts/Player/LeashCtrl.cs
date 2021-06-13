using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TestGame.Player
{
    //Leash Control here
    [RequireComponent(typeof(LineRenderer))]
    public class LeashCtrl : MonoBehaviour
    {
        [Range(0, 50)]
        public int segments = 50;
        LineRenderer line;

        //asdasdsad
        public float minRange;
        //[Range(0, 100)]
        //public float playerHealth, botHealth;
        [Range(0, 100)]
        public float p_healtIncrease, p_healthDecrease, e_healthIncrease, e_healthDecrease;


        public GameObject fetchedEnemy, nextFetchedEnemy;

        public GameObject[] EnemyList;
        public PlayerCharacter m_playerChar;
        public Bots.BotCharacter m_botChar;

        //public RopeJointController m_ropeJointController;
        //public RopeControllerSimple m_ropeControllerSimple;

        // Start is called before the first frame update
        void Start()
        {
            line = gameObject.GetComponent<LineRenderer>();
            line.positionCount = 0;
            line.useWorldSpace = false;
            m_playerChar = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerCharacter>();
        }

        // Update is called once per frame
        void Update()
        {
            //
            //If the rope is released
            //
            if (Input.GetMouseButton(1))
            {
                nextFetchedEnemy = null;
                //Debug.Log("leash released");
                CreatePoints();
            }
            else if (Input.GetMouseButtonUp(1))
            {
                if (EnemyFetched())
                {
                    CheckWeaponUnlock();
                    AdjustThePlayer(true, true);
                }
                else
                {
                    AdjustThePlayer(false, true);
                }
            }

            //Regenerate because it is fetched
            if (fetchedEnemy != null)
            {
                if (IsStillInsideTheRange())
                {
                    DrawRope();
                    AdjustThePlayer(true, false);
                }
                else
                {
                    AdjustThePlayer(false, true);
                    deleteLine();
                }
            }
            //Degenerate becasue it is not fetched
            else
            {
                ////Debug.Log("Degenerate !!!");
                AdjustThePlayer(false, false);
            }

            GetEnemies();

            //Debug.Log("line world bool: " + line.useWorldSpace);
        }

        void CreatePoints()
        {
            line.positionCount = (segments + 1);

            float x;
            //float y;
            float z;

            float angle = 20f;

            for (int i = 0; i < (segments + 1); i++)
            {
                x = Mathf.Sin(Mathf.Deg2Rad * angle) * minRange;
                z = Mathf.Cos(Mathf.Deg2Rad * angle) * minRange;

                line.SetPosition(i, new Vector3(x, 0, z));

                angle += (360f / segments);
            }
        }


        public void deleteLine()
        {
            Debug.Log("delete called");
            line.useWorldSpace = false;
            line.positionCount = 0;

            fetchedEnemy = null;
        }

        private bool EnemyFetched()
        {
            // make a plane in the world level to the player
            Plane plane = new Plane(transform.up, gameObject.transform.position);

            // create a ray from your mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Vector3 mousePos = Vector3.zero;
            // cast the ray from mouse to the plane
            if (plane.Raycast(ray, out float distance))
            {
                // get mouse position from where it hit in the world
                mousePos = ray.GetPoint(distance);
            }

            //get 3 closest characters (to referencePos)
            var nClosest = EnemyList.OrderBy(t => (t.transform.position - mousePos).sqrMagnitude)
                                       .FirstOrDefault();

            //if the fetched enemy is ded enemy
            if (nClosest.gameObject.GetComponent<Bots.BotCharacter>().Energy < 1)
                return false;

            //check whether inside the range of the player
            if((transform.position - nClosest.gameObject.transform.position).sqrMagnitude <= minRange*10)
            {
                fetchedEnemy = nClosest.gameObject;
                nextFetchedEnemy = fetchedEnemy;

                //Debug.Log("distance: " + (transform.position - fetchedEnemy.transform.position).sqrMagnitude);
               // Debug.Log("the mouse input: " + mousePos);
                line.useWorldSpace = true;

                //m_ropeJointController.StartInit(gameObject, fetchedEnemy.gameObject);
                //InitRopeJoint();

                return true;
            }
            else
            {
                //Debug.Log("Enemy is outta here False");
                //DestroyRopeJoint();
                //m_ropeJointController.DestroyRope();

                //m_ropeJointController.DestroyRope();
                deleteLine();
                return false;
            }
        }

        public void AdjustThePlayer(bool isAttached, bool change) 
        {

            if (change)
            {
                if(fetchedEnemy != null && fetchedEnemy.GetComponent<Bots.BotCharacter>().Energy > 1)
                    fetchedEnemy.GetComponent<Bots.BotController>().tether(isAttached);
                change = false;
            }

            if (isAttached)
            {
                if(m_playerChar.Energy < m_playerChar.EnergyMax)
                    m_playerChar.AddHealth(p_healtIncrease * Time.deltaTime);

                if (fetchedEnemy.GetComponent<Bots.BotCharacter>().Energy > 0)
                {
                    fetchedEnemy.GetComponent<Bots.BotCharacter>().TakeDamage(e_healthDecrease * Time.deltaTime);
                }
                else // if the enemy is dead, update this function again
                {
                    if (fetchedEnemy.GetComponent<Bots.BotCharacter>().Energy > 0)
                    {
                        AdjustThePlayer(false, true);
                        //DestroyRopeJoint();
                        //m_ropeJointController.DestroyRope();
                    }
                    else
                    {
                        deleteLine();
                        //DestroyRopeJoint();
                        //m_ropeJointController.DestroyRope();
                        AdjustThePlayer(false, false);
                    }
                }
            }
            else
            {
                //GAME OVER
                m_playerChar.TakeDamage(p_healthDecrease * Time.deltaTime);
            }
        }

        public void DrawRope()
        {
            //Attach the rope
            line.positionCount = 2;

            line.SetPosition(0, transform.position);
            line.SetPosition(1, fetchedEnemy.transform.position);
        }

        //public void InitRopeJoint()
        //{
        //    m_ropeControllerSimple.whatTheRopeIsConnectedTo = m_playerChar.gameObject.transform;
        //    m_ropeControllerSimple.whatIsHangingFromTheRope = fetchedEnemy.gameObject.transform;
        //}

        //public void DestroyRopeJoint()
        //{
        //    m_ropeControllerSimple.whatTheRopeIsConnectedTo = null;
        //    m_ropeControllerSimple.whatIsHangingFromTheRope = null;
        //}

        public bool IsStillInsideTheRange()
        {
            if ((transform.position - fetchedEnemy.transform.position).sqrMagnitude <= minRange * 10)
                return true;
            else
                return false;
        }

        public void GetEnemies()
        {
            EnemyList =  GameObject.FindGameObjectsWithTag("Enemy");
        }

        public void CheckWeaponUnlock() {
            Bots.BotCharacter bot = fetchedEnemy.GetComponent<Bots.BotCharacter>();
            if (bot != null && bot.Weapon != null) {
                Debug.Log("call");
                foreach(Weapons.Weapon weapon in m_playerChar.Controller.WeaponSlots) {
                    if (weapon.name == bot.Weapon.name){
                        Debug.Log("already equipped: " + bot.Weapon.name);
                        return;
                    }
                }
                Debug.Log(bot.Weapon.AvatarImage.name);
                m_playerChar.Controller.WeaponSlots.Add(bot.Weapon);
            }
        }

    }
}
