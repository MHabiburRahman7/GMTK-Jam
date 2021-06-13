using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame
{
    public class RopeJointController : MonoBehaviour
    {
        [Range(1, 10)]
        public int ropeSegment;

        public Transform sourceTarget, destinationTarget;
        public GameObject ropeSegmentPrefab, ropeHead;

        public void DestroyRope()
        {
            Destroy(ropeHead.gameObject);
        }

        public void StartInit(GameObject source, GameObject destination)
        {
            sourceTarget = source.transform;
            destinationTarget = destination.transform;

            if (ropeHead != null)
                DestroyRope();

            InitChilds();
        }

        public void InitChilds()
        {
            List<Vector3> positions = calculateDistance();

            GameObject parent_game = sourceTarget.gameObject;
            //ropeHead = parent_game;
            GameObject child_game;
            for (int i = 0; i < ropeSegment; i++)
            {
                child_game = Instantiate(ropeSegmentPrefab, positions[i], Quaternion.identity, parent_game.gameObject.transform);
                
                parent_game = child_game;

                if (i == 0)
                {
                    ropeHead = parent_game;
                }
            }
        }

        public List<Vector3> calculateDistance()
        {
            List<Vector3> res = new List<Vector3>();

            //Initialization
            //res.Add(sourceTarget.transform.position);

            //Divide by source - target relation
            for (int i = 1; i < ropeSegment; i++)
            {
                var x = ((destinationTarget.transform.position.x - sourceTarget.transform.position.x) / ropeSegment) * i;
                var y = ((destinationTarget.transform.position.y - sourceTarget.transform.position.y) / ropeSegment) * i;
                var z = ((destinationTarget.transform.position.z - sourceTarget.transform.position.z) / ropeSegment) * i;

                res.Add(new Vector3(x, y, z));
            }

            return res;
        }
    }
}
