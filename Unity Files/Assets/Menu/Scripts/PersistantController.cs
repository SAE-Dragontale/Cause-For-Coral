using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			PersistantController.cs
   Description: 	This script makes the object attached persist throughout the game.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

public class PersistantController : MonoBehaviour {

	private static PersistantController _instance; // Retain personalised instance, so we don't create more than one of this object.

	// --------------------------------------------------------------------------------------------------------------------------------------------------------- //

	// Use this for initialization
	void Start () {

		// Make sure we've only got one of this little bugger running.
		if(!_instance) {
			_instance = this;
		} else {
			Destroy (this.gameObject);
		}

		// Make sure this isn't destroyed. I quite like it.
		DontDestroyOnLoad (gameObject);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
