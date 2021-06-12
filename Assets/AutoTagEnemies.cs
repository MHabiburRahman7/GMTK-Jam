using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame
{
    public class AutoTagEnemies : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {
        
        }

        private void changeIntoEnemyTag()
        {
            for(int i=0; i<transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.tag = "Enemy";
            }
        }

        // Update is called once per frame
        void Update()
        {
            changeIntoEnemyTag();
        }
    }
}
