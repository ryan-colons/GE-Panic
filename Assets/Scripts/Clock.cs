using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour {

	public Text clockText;
	private float time;
	private bool ticking = true;

	private void Update () {
		if (ticking) {
			if (time > 0f) {
				time -= Time.deltaTime;
				updateClockText ();
			} else {
				int[] scores = GameObject.Find ("Death Cube").GetComponent<ScoreKeeping> ().getScores ();
				ticking = false;
				GetComponent<ArenaLoader> ().endGame (scores);
			}
		}
	}

	private void updateClockText () {
		string str = "";
		if (time > 60f)
			str += (int)time / 60 + ":";
		if (time % 61 < 10)
			str += "0";
		str += (int)time % 61;
		clockText.text = str;
	}

	public void setTime (float f) {
		time = f;
	}
	public void setTicking (bool b) {
		ticking = b;
	}
	public bool isTicking () {
		return ticking;
	}
}
