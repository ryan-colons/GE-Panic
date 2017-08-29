using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaLoader : MonoBehaviour {

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

	private void Awake () {
		loadArena ();
	}

	private void loadArena () {
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
}
