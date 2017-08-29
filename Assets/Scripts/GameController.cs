using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	private bool[] players = {true, false, false, false};

	private void Awake () {
		//allows this object to persist between scenes
		DontDestroyOnLoad (this.gameObject);
	}

	public void startGame () {
		SceneManager.LoadScene("Arena");
	}

	public void exitApplication () {
		Application.Quit ();
	}

	public void setPlayer (int index, bool b) {
		players [index] = b;
	}
	public bool[] getPlayers () {
		return players;
	}
}
