using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Author:     Thau Shien Hsu
 * Student ID: 1007996
 */

public class SpawnExitNodeScript : MonoBehaviour
{
    //[SerializeField] private List<GameObject> _patrolNodes = null;





    void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.tag == "Predator" && !collide.isTrigger)
        {
            if (collide.gameObject.GetComponent<PredatorAI>().GetState() == State.EXIT)
            {
                Destroy(collide.gameObject);
            }
        }
    }
}
