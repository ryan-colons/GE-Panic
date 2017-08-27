﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

	public int playerID;
	public float speed;
	private Rigidbody body;
	private Vector3 startPos;
	public string horizInput, vertInput, boostInput;
	private float boostTimeout;
	private float maxBoostTimeout = 5f;
	public Slider boostSlider;

	private void Start () {
		body = GetComponent<Rigidbody> ();
		startPos = transform.position;
	}

	private void FixedUpdate () {
		//apparently FixedUpdate() should be used with rigidbodies, instead of Update()
		float horiz_move = Input.GetAxis(horizInput);
		float vert_move = Input.GetAxis (vertInput);

		if (Input.GetButton (boostInput) && boostTimeout <= 0f) {
			horiz_move *= 50f;
			vert_move *= 50f;
			boostTimeout = maxBoostTimeout;
		}

		Vector3 move = new Vector3 (horiz_move, 0f, vert_move);

		body.AddForce (move * speed);
		if (boostTimeout > 0) {
			boostTimeout -= Time.deltaTime;
		}

		boostSlider.value = 1 - (boostTimeout / maxBoostTimeout);
	}

	public void respawn () {
		this.transform.position = startPos;
		body.velocity = Vector3.zero;
		body.angularVelocity = Vector3.zero;
		boostTimeout = 0;
	}

	public float getBoostTimeout () {
		return boostTimeout;
	}
}
