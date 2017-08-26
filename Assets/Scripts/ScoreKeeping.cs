using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeping : MonoBehaviour {

	private void OnTriggerEnter (Collider other) {
		if (other.tag.Equals ("Player")) {
			other.GetComponent<PlayerControl> ().respawn ();
			//change the scores accordingly
		} else {
			Destroy (other.gameObject);
		}
	}
}
