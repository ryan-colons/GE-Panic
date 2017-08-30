using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaLoader : MonoBehaviour {

	public GameObject winPanel;

	/* I added this little class (playerObjects) just to
	 * make it easier to set up the player objects in the inspector.
	 * It's all just to let the game know which objects to set active
	 * for which player */
	[System.Serializable]
	public class playerObjects {
		public GameObject player;
		public GameObject text;
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
			} else {
				players [i].player.SetActive (false);
				players [i].text.SetActive (false);
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

	private void Update () {
		if (winPanel.activeSelf) {
			if (Input.GetButton ("P1_Boost"))
				GameObject.Find ("Game Controller").GetComponent<GameController> ().startGame ();
			if (Input.GetButton ("P1_Alt"))
				GameObject.Find ("Game Controller").GetComponent<GameController> ().mainMenu ();
		}
	}

}
