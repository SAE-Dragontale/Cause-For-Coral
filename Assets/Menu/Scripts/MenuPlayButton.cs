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

public class MenuPlayButton : MonoBehaviour {

	[Header("External Components")]
	[Tooltip("Please type the name of the Animator Variable that controls the transition")]
	[SerializeField] private Animator _anMenuCamera;

	[Tooltip("Please enter each dropdown menu text element")]
	[SerializeField] private List<Text> _ltxDropDown;

	[Tooltip("Please enter the two text elements that the Play Button is comprised of in the Select Screen")]
	[SerializeField] private List<Text> _ltxPlayButton;

	[Tooltip("Please enter the GameObject controlling the PlayButton")]
	[SerializeField] private GameObject _gmPlayButton;
	
	// Update is called once per frame
	void Update () {

		// If we are on the 'Select Screen' and at least two drop-down boxes have been assigned...
		if (_anMenuCamera.GetBool ("_isShowingSelect")) {

			int itPlayersReady = 0; // Number of Dropdown fields that do not contain blank values.

			// Disable the button that allows players to begin the game
			_gmPlayButton.SetActive(false);

			// Check each dropdown menu for the amount of players, and if there are any duplicate players entered.
			foreach (Text tx in _ltxDropDown) {
				if (tx.text != "None") {
					itPlayersReady++;
				}
			}

			// Show the button that allows players to begin the game
			if (itPlayersReady > 1) {
				_gmPlayButton.SetActive(true);
			}

		}

	}

	// Button-Specified Scene Loader. Configure the scene you want to load on the button itself.
	public void LoadSceneIndex (int itSceneIndex) {

		// Temporary Variables
		int itDropDown = 1;

		for (int it = 1; it <= 4; it++) {
			PlayerPrefs.DeleteKey("Character"+it);
		}

		// Retrieve the player choices and assign them to their respective player prefs.
		foreach (Text tx in _ltxDropDown) {
			if (tx.text == "None") {
				PlayerPrefs.SetInt ("Character"+itDropDown,0);
			} else {
				PlayerPrefs.SetInt ( "Character"+itDropDown, int.Parse(tx.text.Substring(tx.text.Length - 1)) );
			}
			itDropDown++;
		}

		// FInally, load the level.
		SceneManager.LoadScene (itSceneIndex);

	}

}
