using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/* 
 * Author:     Thau Shien Hsu
 * Student ID: 1007996
 */


public class MineScript : MonoBehaviour
{

    void OnCollisionEnter(Collision collide)
    {
        if (collide.collider.gameObject.tag == "Player")
        {
			collide.collider.gameObject.GetComponent<_Pawn>()._isAlive = false;
        }
    }
}
