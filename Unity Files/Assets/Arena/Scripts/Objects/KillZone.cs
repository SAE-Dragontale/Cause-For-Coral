using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			KillZone.cs
   Description: 	This script triggers the players death on collision
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

public class KillZone : MonoBehaviour {

	void OnTriggerEnter(Collider cl) {

		if (cl.gameObject.tag == "Player") {
			cl.gameObject.GetComponent<_Pawn> ()._isAlive = false;
		}

	}

}
