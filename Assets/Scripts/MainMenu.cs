using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	private GameController game;
	private GameObject playerSelectPanel;
	private Text[] options = new Text[2];
	private int optionIndex;
	private float menuScrollRefresh = 0f, maxScrollRefresh = 0.25f;

	private GameObject[] playerSelectTexts = new GameObject[4];

	private Text titleText;
	private string title = "Grue Eaters: Panic!";
	private string instructions = "Press Boost to join! Press Alt to leave!\n" +
	                              "Player One, press Boost to start the game, or Alt to go back!";

	private void Start () {
		game = GameObject.Find ("Game Controller").GetComponent<GameController> ();
		playerSelectPanel = transform.Find ("Player Select Panel").gameObject;
		options [0] = transform.Find ("Start Text").GetComponent<Text> ();
		options [1] = transform.Find ("Quit Text").GetComponent<Text> ();
		optionIndex = 0;
		playerSelectTexts [0] = transform.Find ("Player Select Panel/P1 Select Text").gameObject;
		playerSelectTexts [1] = transform.Find ("Player Select Panel/P2 Select Text").gameObject;
		playerSelectTexts [2] = transform.Find ("Player Select Panel/P3 Select Text").gameObject;
		playerSelectTexts [3] = transform.Find ("Player Select Panel/P4 Select Text").gameObject;
		titleText = transform.Find ("Title Text").GetComponent<Text> ();
	}

	private void openPlayerSelectMenu () {
		foreach (Text txt in options) {
			txt.gameObject.SetActive (false);
		}
		playerSelectPanel.SetActive (true);
		titleText.text = instructions;
	}

	private void openMainMenu() {
		playerSelectPanel.SetActive (false);
		foreach (Text txt in options) {
			txt.gameObject.SetActive (true);
		}
		titleText.text = title;
	}

	private void Update () {
		if (!playerSelectPanel.activeSelf) { 
			updateMainMenu ();
		} else {
			updatePlayerSelectMenu ();
		}
	}

	private void updatePlayerSelectMenu () {

		/* this is a little bit garbage lol */

		if (Input.GetButton ("P1_Boost"))
			game.startGame ();
		if (Input.GetButton ("P2_Boost"))
			game.setPlayer (1, true);
		if (Input.GetButton ("P3_Boost"))
			game.setPlayer (2, true);
		if (Input.GetButton ("P4_Boost"))
			game.setPlayer (3, true);

		if (Input.GetButton ("P1_Alt")) {
			openMainMenu ();
			return;
		}
		if (Input.GetButton ("P2_Alt"))
			game.setPlayer (1, false);
		if (Input.GetButton ("P3_Alt"))
			game.setPlayer (2, false);
		if (Input.GetButton ("P4_Alt"))
			game.setPlayer (3, false);

		for (int i = 0; i < playerSelectTexts.Length; i++) {
			if (game.getPlayers()[i])
				playerSelectTexts [i].SetActive (true);
			else
				playerSelectTexts [i].SetActive (false);
		}
	}

	private void updateMainMenu () {
		if (Input.GetButton("Submit") || Input.GetButton("P1_Boost")) {
			switch (options [optionIndex].gameObject.name) {
			case "Start Text":
				openPlayerSelectMenu ();
				break;
			case "Quit Text":
				game.exitApplication ();
				break;
			default:
				Debug.Log ("What is this name??");
				break;
			}
		}
		float p1_vert_axis = Input.GetAxis ("P1_Vert");
		if (menuScrollRefresh <= 0) {
			if (p1_vert_axis > 0) {
				optionIndex = (optionIndex + 1) % options.Length;
				menuScrollRefresh = maxScrollRefresh;
			} else if (p1_vert_axis < 0) {
				optionIndex -= 1;
				if (optionIndex < 0)
					optionIndex = options.Length - 1;
				menuScrollRefresh = maxScrollRefresh;
			}
		}
		if (menuScrollRefresh > 0)
			menuScrollRefresh -= Time.deltaTime;

		for (int i = 0; i < options.Length; i++) {
			if (i == optionIndex) {
				options [i].color = Color.yellow;
			} else {
				options [i].color = Color.black;
			}
		}
	}
}
