using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TestGame
{
    [RequireComponent(typeof(LineRenderer))]
    public class LeashCtrl : MonoBehaviour
    {
        [Range(0, 50)]
        public int segments = 50;
        LineRenderer line;

        //asdasdsad
        public float minRange;
        public GameObject fetchedEnemy, nextFetchedEnemy;

        public GameObject[] EnemyList;

        // Start is called before the first frame update
        void Start()
        {
            line = gameObject.GetComponent<LineRenderer>();
            line.useWorldSpace = false;
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
                Debug.Log("leash released");
                CreatePoints();
            }
            else if (Input.GetMouseButtonUp(1))
            {
                EnergyFetched();
            }

            if (fetchedEnemy != null)
                DrawRope();

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
            if(nextFetchedEnemy == null)
            {
                Debug.Log("delete called");
                line.useWorldSpace = false;
                line.positionCount = 0;

                fetchedEnemy = null;
            }
        }

        private bool EnergyFetched()
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
            //.Take(3)   //or use .FirstOrDefault();  if you need just one
            //.ToArray();
            //Debug.Log("closestEnemy: " + nClosest.name);

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

        public void DrawRope()
        {
            //Attach the rope
            line.positionCount = 2;

            line.SetPosition(0, transform.position);
            line.SetPosition(1, fetchedEnemy.transform.position);
            //line.SetPosition(i, new Vector3(x, 0, z));
        }

        public void GetEnemies()
        {
            EnemyList =  GameObject.FindGameObjectsWithTag("Enemy");
        }

    }
}
