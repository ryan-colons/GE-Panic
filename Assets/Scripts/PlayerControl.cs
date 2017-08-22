using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public float speed;
	private Rigidbody body;
	private Vector3 startPos;
	public string horizInput, vertInput;

	void Start () {
		body = GetComponent<Rigidbody> ();
		startPos = transform.position;
	}

	void FixedUpdate () {
		//apparently FixedUpdate() should be used with rigidbodies, instead of Update()
		float horiz_move = Input.GetAxis(horizInput);
		float vert_move = Input.GetAxis (vertInput);

		Vector3 move = new Vector3 (horiz_move, 0f, vert_move);

		body.AddForce (move * speed);
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag.Equals ("Respawn")) {
			this.transform.position = startPos;
			body.velocity = Vector3.zero;
			body.angularVelocity = Vector3.zero;
		}
	}
}
