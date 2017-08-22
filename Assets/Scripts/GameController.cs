﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	private void Awake () {
		//allows this object to persist between scenes
		DontDestroyOnLoad (this.gameObject);
	}

	public void startGame () {
		SceneManager.LoadScene ("arena");
	}

	public void exitApplication () {
		Application.Quit ();
	}
}
