using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* All of this is a -little- bit garbage.
 * Basically, this class should do less, I think. */

public class ArenaLoader : MonoBehaviour {

	public GameObject winPanel;
	public GameObject pauseText;
	private float pauseRefresh = 0f, maxPauseRefresh = 0.25f;

	/* I added this little class (playerObjects) just to
	 * make it easier to set up the player objects in the inspector.
	 * It's all just to let the game know which objects to set active
	 * for which player */
	[System.Serializable]
	public class playerObjects {
		public GameObject player;
		public GameObject text;
		public bool ingame;
	}

	public playerObjects[] players;
	private Clock clock;

	private void Awake () {
		loadArena ();
		clock = GetComponent<Clock> ();
		clock.setTime (20f);
		clock.setTicking (true);
	}

	private void loadArena () {
		winPanel.SetActive (false);
		bool[] playerSelectionArray = GameObject.Find("Game Controller").GetComponent<GameController>().getPlayers();
		if (playerSelectionArray.Length != players.Length) {
			Debug.Log ("Something went wrong! " + playerSelectionArray.Length + "/" + players.Length);
			return;
		}
		for (int i = 0; i < players.Length; i++) {
			if (playerSelectionArray [i]) {
				players [i].player.SetActive (true);
				players [i].text.SetActive (true);
				players [i].ingame = true;
			} else {
				players [i].player.SetActive (false);
				players [i].text.SetActive (false);
				players [i].ingame = false;
			}
		}
	}

	public void endGame (int[] scores) {
		winPanel.SetActive (true);
		bool[] playerSelectionArray = GameObject.Find("Game Controller").GetComponent<GameController>().getPlayers();
		for (int i = 0; i < playerSelectionArray.Length; i++) {
			players [i].player.SetActive (false);
			players [i].text.SetActive (false);
		}
		int winIndex = 0;
		for (int i = 0; i < scores.Length; i++) {
			if (scores [i] < scores [winIndex] && playerSelectionArray[i])
				winIndex = i;
		}
		winPanel.transform.Find ("Winner Text").GetComponent<Text> ().text = "Player " + (winIndex + 1) + " is a WINNER!";
	}

	/* Let the record show that I acknowledge that the Pause stuff shouldn't be part of this class.
	 * This should probably be rectified eventually!
	 */

	private void Update () {
		if (winPanel.activeSelf) {
			if (Input.GetButton ("P1_Boost"))
				GameObject.Find ("Game Controller").GetComponent<GameController> ().startGame ();
			if (Input.GetButton ("P1_Alt"))
				GameObject.Find ("Game Controller").GetComponent<GameController> ().mainMenu ();
		} else {
			if (clock.isTicking ()) {
				// if the clock is ticking, and you press Alt the game should pause
				if ((Input.GetButton ("P1_Alt") || Input.GetButton ("P2_Alt") || Input.GetButton ("P3_Alt") || Input.GetButton ("P4_Alt")) && !(pauseRefresh > 0f)) {
					foreach (playerObjects obj in players) {
						if (obj.ingame) obj.player.GetComponent<PlayerControl> ().pause ();
					}
					clock.setTicking (false);
					pauseText.SetActive (true);
					pauseRefresh = maxPauseRefresh;
				}
			} else {
				// if the clock isn't ticking, and you press Alt the game should resume
				if ((Input.GetButton ("P1_Alt") || Input.GetButton ("P2_Alt") || Input.GetButton ("P3_Alt") || Input.GetButton ("P4_Alt")) && !(pauseRefresh > 0f)) {
					foreach (playerObjects obj in players) {
						if (obj.ingame) obj.player.GetComponent<PlayerControl> ().unpause ();
					}
					clock.setTicking (true);
					pauseText.SetActive (false);
					pauseRefresh = maxPauseRefresh;
				}
				// if the clock isn't ticking and you press Boost the game should quit
				// Boost should be the quit button, rather than the resume button, because otherwise you Boost on resuming which is dumb
				// possible problem: other player accidentally tries to boost just as you pause, and quits the game :/
				if ((Input.GetButton ("P1_Boost") || Input.GetButton ("P2_Boost") || Input.GetButton ("P3_Boost") || Input.GetButton ("P4_Boost")) && !(pauseRefresh > 0f)) {
					GameObject.Find ("Game Controller").GetComponent<GameController> ().mainMenu ();
				}
			}
			if (pauseRefresh > 0f)
				pauseRefresh -= Time.deltaTime;
		}
	}

}
