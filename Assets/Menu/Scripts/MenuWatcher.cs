using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			MenuWatcher.cs
   Description: 	This script contains the functions used by the main menu to control the camera, change scenes, and manipulate the options settings. 
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

public class MenuWatcher : MonoBehaviour {

	// Inspector Variables

	[Header("Button Interface")]
	[Tooltip("Please type the name of the Animator Variable that controls the transition")]
	[SerializeField] private string _stBooleanToChange;

	[Header("External Components")]
	[Tooltip("Please type the name of the Animator Variable that controls the transition")]
	[SerializeField] private Animator _anMenuCamera;

	// Private Variables

	private bool _isOnEntry = true; // Whether the input has been toggled yet


	// --------------------------------------------------------------------------------------------------------------------------------------------------------- //

	// Update is called once per frame
	void Update () {

		// If the 'Entrance Screen' has not already been triggered, monitor inputs for 'Any Key' and activate the transition. Then, disable this check to save memory.
		if (_isOnEntry) {
			if (Input.anyKey) {
				_anMenuCamera.SetBool ("_isShowingMain", true);
				_isOnEntry = false;
			}
		}
		
	}

	// Toggle an animator variable to true or false depending on the button's settings.
	public void SetAnimatorBoolean () {
		_anMenuCamera.SetBool (_stBooleanToChange, !_anMenuCamera.GetBool(_stBooleanToChange));
	}

	// Quit the application.
	public void ApplicationQuit() {
		Application.Quit ();
		EditorApplication.isPlaying = false;
	}

}
