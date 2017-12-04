﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWalkingAI : MonoBehaviour {

	private float idleTime = 5;
	private float walkingDistance = 0;
	[SerializeField]
	private float leftBoundary = 0;
	[SerializeField]
	private float rightBoundary = 5;
	[SerializeField]
	private float speed = 30; 
	// state -1: moving left, state 0 : idle , state 1: moving right
	private int state = 0;
	[SerializeField]
	private GameObject NPCTexture;

	// Use this for initialization
	void Start () {
		updateSprite ();
	}
	
	// Update is called once per frame
	void Update () {
		if (state == 0) {
			if (idleTime <= 0) {
				changeState ();
			} else {
				idleTime -= Time.deltaTime;
			}
		} else {
			if (gameObject.transform.position.x < leftBoundary) {
				state = 1;
				updateSprite ();
			}
			if  ( gameObject.transform.position.x > rightBoundary) {
				state = -1;
				updateSprite ();
			}
			if (walkingDistance <= 0) {
				changeState();
			} else {
				transform.Translate ((state * speed * Time.deltaTime) / 100, 0, 0);
				walkingDistance -= (speed * Time.deltaTime) / 100;
			}
		}
	}

	// change state between walking and standing
	private void changeState() {
		state = Random.Range (-1, 2);
		if (state == 0) {
			idleTime = (float) Random.Range (5, 10);
			updateSprite ();
		} else {
			walkingDistance = (float) Random.Range (1, 3);
			updateSprite ();
		}
	}

	private void updateSprite() {
		if (state == 0) {
			gameObject.GetComponent<Animator> ().SetBool ("walking", false);
		} else {
			gameObject.GetComponent<Animator> ().SetBool ("walking", true);
			if (state == -1) {
				NPCTexture.transform.eulerAngles = new Vector3 (0, 0, 0);
			} else {
				NPCTexture.transform.eulerAngles = new Vector3 (0, 180, 0);
			}
		}
	}
}
