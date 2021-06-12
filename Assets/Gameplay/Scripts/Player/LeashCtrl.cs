using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TestGame.Player
{
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
        [Range(0, 10)]
        public float p_healtIncrease, p_healthDecrease, e_healthIncrease, e_healthDecrease;


        public GameObject fetchedEnemy, nextFetchedEnemy;

        public GameObject[] EnemyList;
        public PlayerCharacter m_playerChar;
        public Bots.BotCharacter m_botChar;

        // Start is called before the first frame update
        void Start()
        {
            line = gameObject.GetComponent<LineRenderer>();
            line.useWorldSpace = false;
            m_playerChar = this.gameObject.GetComponent<PlayerCharacter>();
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
                    AdjustThePlayer(true, true);
                }
            }

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

            //check whether inside the range
            if((transform.position - nClosest.gameObject.transform.position).sqrMagnitude <= minRange*10)
            {
                fetchedEnemy = nClosest.gameObject;
                nextFetchedEnemy = fetchedEnemy;

                Debug.Log("distance: " + (transform.position - fetchedEnemy.transform.position).sqrMagnitude);
                Debug.Log("the mouse input: " + mousePos);
                line.useWorldSpace = true;

                return true;
            }
            else
            {
                Debug.Log("Enemy is outta here False");
                deleteLine();
                return false;
            }
        }

        public void AdjustThePlayer(bool isAttached, bool change) 
        {

            if (change)
            {
                fetchedEnemy.GetComponent<Bots.BotController>().tether(isAttached);
                change = false;
            }

            if (isAttached)
            {
                if(m_playerChar.Health < 100)
                    m_playerChar.AddHealth(p_healtIncrease);

                if (fetchedEnemy.GetComponent<Bots.BotCharacter>().Health > 0)
                {
                    fetchedEnemy.GetComponent<Bots.BotCharacter>().TakeDamage(e_healthDecrease * Time.deltaTime);
                }
                else
                {
                    AdjustThePlayer(false, true);
                    deleteLine();
                }
            }
            else
            {
                //GAME OVER
                m_playerChar.AddHealth(p_healthDecrease * Time.deltaTime);
                fetchedEnemy.GetComponent<Bots.BotCharacter>().TakeDamage(0);
                //fetchedEnemy.GetComponent<Bots.BotController>().tether(isAttached);
            }
        }

        public void DrawRope()
        {
            //Attach the rope
            line.positionCount = 2;

            line.SetPosition(0, transform.position);
            line.SetPosition(1, fetchedEnemy.transform.position);
        }

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

    }
}
