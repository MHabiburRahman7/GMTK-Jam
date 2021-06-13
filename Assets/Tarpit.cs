using System.Collections;
using System.Collections.Generic;
using TestGame.Player;
using UnityEngine;

namespace TestGame
{
    public class Tarpit : MonoBehaviour
    {
        void OnCollisionEnter(Collision collision)
        {
            var player = collision.collider.gameObject.GetComponent("PlayerController") as PlayerController;
            if (player!=null){
                player.DivideCurrentSpeed(2);//todo use a dictionnary for debuffs instead.
            }
        }
        void OnCollisionExit(Collision collision)
        {

            var player = collision.collider.gameObject.GetComponent("PlayerController") as PlayerController;
            if (player != null)
            {
                player.RestoreBaseSpeed();
            }
        }
    }
}
