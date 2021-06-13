using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame
{
    public enum Shape
    {
        Circle,
        Cone,
        SingleTarget
    }
    [CreateAssetMenu(fileName = "HitShape.asset", menuName = "Weapons/Shape")]
    public class HitShape : MonoBehaviour
    {
        public float radius;
        public float angle;
        public float direction;
        public Vector3 targetPosition;
        public float DamageScale;
        public Effect[] effects;

        [TagSelector]
        public string[] TagFilterArray = new string[] { };
        void doEffects(GameObject Collider)
        {
            var targets = new List<GameObject>();
            if (radius <= 0.01)//not == 0 because fuck floats.
            {
                if (Collider != null)
                    targets.Add(Collider);
            }
            else
            {
                var candidates = new List<GameObject>();
                foreach (var item in TagFilterArray)
                {
                    candidates.AddRange(GameObject.FindGameObjectsWithTag(item));
                }

                //TODO: actually do the calculations in 2d
                if (angle >= 360)
                {
                    foreach (var item in candidates)
                    {
                        if (Vector3.Distance(item.transform.position, targetPosition) <= radius)
                        {
                            targets.Add(item);
                        }
                    }
                }
                else if (angle > 0)
                {

                    foreach (var item in candidates)
                    {
                        var sAngle = Vector3.SignedAngle(item.transform.position, targetPosition, Vector3.up) + 180;
                        if (Vector3.Distance(item.transform.position, targetPosition) <= radius &&
                            sAngle <= (direction + (angle / 2) + 180) % 360 && sAngle >= (direction - (angle / 2) + 180) % 360)
                        {
                            targets.Add(item);
                        }
                    }
                }
                else//line
                {

                }
                foreach (var effect in effects)
                {
                    foreach (var target in targets)
                    {
                        effect.Apply(target);
                    }
                }
            }
        }
    }
}
