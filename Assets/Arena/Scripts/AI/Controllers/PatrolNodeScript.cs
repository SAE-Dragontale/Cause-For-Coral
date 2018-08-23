using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Author:     Thau Shien Hsu
 * Student ID: 1007996
 */

public class PatrolNodeScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> _neighbourNodes;


    void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.tag == "Predator" && !collide.isTrigger)
        {
            if (collide.gameObject.GetComponent<PredatorAI>().GetState() != State.EXIT || collide.gameObject.GetComponent<PredatorAI>().GetState() != State.PURSUIT)
            {
                collide.gameObject.GetComponent<PredatorAI>().SetNewTarget(_neighbourNodes[Random.Range(0, _neighbourNodes.Count)]);
            }
        }
    }
}
