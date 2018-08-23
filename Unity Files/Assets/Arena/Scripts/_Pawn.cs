using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			_Pawn.cs
   Description: 	This script controls the player character, whether they are player 1, 2, 3, or 4. 
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

[RequireComponent(typeof(Rigidbody))]

public class _Pawn : MonoBehaviour {

	// ---- Inspector Variables

	[Header("Gameplay Elements")]

	[Tooltip("Astronaut: 1\nNeckbeard: 2\nLawyer: 3\nMusician: 4")]
	[SerializeField] private int _itCharacterNumber;

	[Tooltip("The player's projectile object")]
	[SerializeField] private GameObject _gmProjectile;

	[Tooltip("This is how fast the players move around the game field.")]
	[SerializeField] private float _flSpeed = 10f;

	[Tooltip("This how fast the player charges their projectile")]
	[SerializeField] private float _flChargeScalar = 1f;

	[Header("Required Components")]

	[Tooltip("This is the pause component. Attach it to the Game Ender in the Inspector.")]
	[SerializeField] private Controller _scControl;

	// ---- Private Variables

	// Create instances of the gameObjects variables.
	private PlayerIndex _piPLayerNum;
	private GamePadState _psState;

	private Rigidbody _rbSelf;
	private BoxCollider _bcSelf;
	private Animator _anSelf;

	// Control the player's actions with these variables.
	[HideInInspector] public bool _isBlocking = false;
	[HideInInspector] public bool _isCharging = false;

	// Control the player's projectile with these variables.
	private float _flStartTime;
	private bool _isTrackingTime = false;

	// Control the player's state with these variables.
	[HideInInspector] public bool _isAlive = true;
	private bool _isReportedDead = false;
	private bool _isWin = false;

	// --------------------------------------------------------------------------------------------------------------------------------------------------------- //

	// Use this for initialization.
	// Note: This should be used to assign the player's sprites and controls in accordance to their choice in the Character Select. If there was no choice, remove this pawn.
	void Start () {

		// Fetch any neccessary components for the player here.
		_rbSelf = GetComponent<Rigidbody> ();
		_bcSelf = GetComponent<BoxCollider> ();
		_anSelf = gameObject.GetComponentInChildren<Animator> ();

		int itPrefNum = PlayerPrefs.GetInt ("Character" + _itCharacterNumber);

		// Assign the players to their respective characters.
		if (itPrefNum == 0) {
			_scControl._itKillCount += 1;
			Destroy (gameObject);
		}

		if (itPrefNum == 1) {
			_piPLayerNum = PlayerIndex.One;
		}

		if (itPrefNum == 2) {
			_piPLayerNum = PlayerIndex.Two;
		}

		if (itPrefNum == 3) {
			_piPLayerNum = PlayerIndex.Three;
		}

		if (itPrefNum == 4) {
			_piPLayerNum = PlayerIndex.Four;
		}

	}

	// --------------------------------------------------------------------------------------------------------------------------------------------------------- //

	// Update is called once per frame.
	void Update () {

		if (_isAlive) {

			// Set the controller's state
			_psState = XInputDotNetPure.GamePad.GetState (_piPLayerNum);

			InputCheck ();
			Movement ();
			Looking ();

			CheckIfGameHasEnded ();

		} else {

			Death ();

		}
		
	}

	// --------------------------------------------------------------------------------------------------------------------------------------------------------- //

	// The player's Movement.
	void Movement() {

		if (_isBlocking == false) {
		
			// Find the Movement Thumbstick's Direction
			Vector3 v3AimedDir = new Vector3 (_psState.ThumbSticks.Left.X, _psState.ThumbSticks.Left.Y, 0f);

			// Move the player based on their Left Thumbstick's direction
			_rbSelf.AddForce (v3AimedDir * _flSpeed * Time.deltaTime, ForceMode.Acceleration);
		
		}

	}

	// The player's directional rotation.
	void Looking() {

		// Flip the player's rotation when they have turned enough in an angle.
		if (_psState.ThumbSticks.Right.X > 0) {
			transform.eulerAngles = new Vector3 (0, 0, 0);
		} else if (_psState.ThumbSticks.Right.X < 0) {
			transform.eulerAngles = new Vector3 (0, 180, 0);
		}

		// Change the player's rotation to face towards the angle the user has pressed the thumbstick in.
		transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, (45 * _psState.ThumbSticks.Right.Y));

	}

	// The player's attack function.
	void InputCheck() {

		// ---- If the player is pressing the [Charge Function].

		if (_psState.Triggers.Right == 1f && !_isBlocking) {

			// Set our variables and begin tracking the time spent charging.
			_isCharging = true;
			_anSelf.SetInteger ("_itChargeState", 1);

			if (!_isTrackingTime) {
				_flStartTime = Time.time;
				_isTrackingTime = true;
			}

		} else if (_isCharging && !_isBlocking) {

			StartCoroutine (CreateProjectile());
			_isTrackingTime = false;

		} else {

			// This function is being used to deal with blocks that have been interrupted or other miscellaneous events.
			_isCharging = false;
			_anSelf.SetInteger ("_itChargeState", 0);

		}
			
		// ---- If the player is pressing the [Block Function].

		if (_psState.Triggers.Left == 1f) {

			// We want to both enable gravity, and create a sudden impulse downwards to kickstart it.
			_rbSelf.useGravity = true;
			_rbSelf.AddForce (new Vector3 (0f, -1f, 0f) * Time.deltaTime * _flSpeed, ForceMode.Acceleration);

			// We want to enable blokcing as a variable, and then enable the animation. We don't need to do anything else for block.
			_isBlocking = true;
			_anSelf.SetBool ("_isBlocking", true);

			// Since we cannot block and charge at the same time, we need to abort any charging functions.
			_isCharging = false;
			_anSelf.SetInteger ("_itChargeState", 0);

		} else if (_isBlocking) {
			
			_rbSelf.useGravity = false;

			// We simply disable the variable, and stop the animation here.
			_isBlocking = false;
			_anSelf.SetBool ("_isBlocking", false);

		}

	}

	IEnumerator CreateProjectile () {

		// Cleanup our variables
		_isCharging = false;
		_anSelf.SetInteger ("_itChargeState", 2);

		// Instantiate
		GameObject gmProjectileNew = Instantiate (_gmProjectile, transform.position, transform.rotation) as GameObject;
		gmProjectileNew.GetComponent<Projectile> ()._gmOwner = gameObject;
		gmProjectileNew.transform.localScale *= Mathf.Clamp(_flChargeScalar * (Time.time - _flStartTime),0.5f,2f);

		// Wait for this to resolve, before allowing the player to fire their projectile again
		yield return new WaitForSeconds (0.1f);
		_anSelf.SetInteger ("_itChargeState", 0);

	}

	// The Player's death function.
	void Death() {

		// Play the player's animation here!
		_anSelf.SetBool("_isAlive",false);

		// Disable the player's collision and enable gravity
		_bcSelf.enabled = false;
		_rbSelf.useGravity = true;

		if (!_isReportedDead) {

			_scControl._itWinningPlayer = PlayerPrefs.GetInt ("Character" + _itCharacterNumber);
			_scControl._itKillCount += 1;
			_isReportedDead = true;
		
		}

		// Once the player has fallen off the screen, delete them.
		if (transform.position.y < -50) {
			Destroy (gameObject);
		}

	}

	void CheckIfGameHasEnded () {

		if (_scControl._itKillCount == _scControl._itTotalPlayers - 1 && !_isWin) {

			_scControl._itWinningPlayer = PlayerPrefs.GetInt ("Character" + _itCharacterNumber);
			_scControl._itKillCount += 1;

			_isWin = true;

			StartCoroutine (VictoryInDeath());

		}

	}

	IEnumerator VictoryInDeath () {

		yield return new WaitForSeconds (5f);

		// Play the player's animation here!
		_anSelf.SetBool("_isAlive",false);

		_isAlive = false;

		// Disable the player's collision and enable gravity
		_bcSelf.enabled = false;
		_rbSelf.useGravity = true;

	}

}
