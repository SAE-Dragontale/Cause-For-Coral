using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			Projectile.cs
   Description: 	This script simply allows for the projectile to move forward, and then interact with any players it touches.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

public class Projectile : MonoBehaviour {

	// ---- Inspector Variables
	[SerializeField] private float _flSpeed = 100f;
	[SerializeField] private float _flForce = 100f;

	// ---- Private Variables
	[HideInInspector] public GameObject _gmOwner;
	[HideInInspector] public bool _isLaunched = false;
	
	// Update is called once per frame
	void Update () {

		transform.position += transform.right.normalized * Time.deltaTime * _flSpeed;
		Destroy (gameObject, 2f);

	}

	void OnTriggerEnter(Collider cl) {

		if (cl.gameObject.tag == "Player" && cl.gameObject != _gmOwner) {

			if (!cl.gameObject.GetComponent<_Pawn> ()._isBlocking){
				cl.gameObject.GetComponent<_Pawn> ()._isCharging = false;
				cl.gameObject.GetComponent<Rigidbody> ().AddForce (transform.position * _flForce, ForceMode.Impulse);
				Destroy (gameObject);
			}

		}

	}

}
