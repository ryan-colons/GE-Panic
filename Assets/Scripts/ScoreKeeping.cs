﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeping : MonoBehaviour {

	public Text p1ScoreText, p2ScoreText, p3ScoreText, p4ScoreText;
	private int[] scores = {0, 0, 0, 0};

	private void updateScoreText () {
		p1ScoreText.text = scores[0].ToString() + " deaths";
		p2ScoreText.text = scores[1].ToString() + " deaths";
		p3ScoreText.text = scores[2].ToString() + " deaths";
		p4ScoreText.text = scores[3].ToString() + " deaths";
	}

	private void OnTriggerEnter (Collider other) {
		if (other.tag.Equals ("Player")) {
			PlayerControl player = other.GetComponent<PlayerControl> ();
			scores [player.playerID - 1]++;
			updateScoreText();
			player.respawn ();
		} else {
			Destroy (other.gameObject);
		}
	}

	public int[] getScores() {
		return scores;
	}
}
