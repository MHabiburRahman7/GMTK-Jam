using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TestGame.Weapons;

namespace TestGame
{
    public class WeaponEditor : Editor
    {
        public override void OnInspectorGUI()
     {
            DrawDefaultInspector(); // for other non-HideInInspector fields
    
            Weapon currentWeapon = (Weapon) target;
    
            // if the weapon is a shotgun, show the number of projectiles fired
            if (currentWeapon.WeaponType == WeaponType.Shotgun) 
            {
                //currentWeapon.numberOfProjectiles;
            }
        }
    }
}