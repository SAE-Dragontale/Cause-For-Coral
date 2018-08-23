using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			Projectile.cs
   Description: 	This script simply allows for the projectile to move forward, and then interact with any players it touches.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

public class Controller : MonoBehaviour {

	// ---- Inspector Variables
	[SerializeField] private GameObject _gmWinScreen;
	[SerializeField] private List<Text> _ltxWinText;
	[SerializeField] public int _itTotalPlayers;

	// ---- Private Variables
	[HideInInspector] public int _itWinningPlayer;
	[HideInInspector] public int _itKillCount;
	
	// Update is called once per frame
	void Update () {

		if (_itKillCount == _itTotalPlayers) {
			StartCoroutine (EndTheGame());
		}

	}

	IEnumerator EndTheGame() {

		_gmWinScreen.SetActive (true);

		foreach (Text tx in _ltxWinText) {
			tx.text = "Player " + _itWinningPlayer + " Wins!";
		}

		yield return new WaitForSeconds (10f);

		SceneManager.LoadScene (1);

	}


}
