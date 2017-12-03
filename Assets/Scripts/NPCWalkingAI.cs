﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCActionsManager : MonoBehaviour {

	private float idleTime = 5;
	private float walkingDistance = 0;
	[SerializeField]
	private float speed = 3; 
	// state -1: moving left, state 0 : idle , state 1: moving right
	private int state = 0;

	// Use this for initialization
//	void Start () {
//		
//	}
	
	// Update is called once per frame
	void Update () {
		if (state == 0) {
			if (idleTime <= 0) {
				changeState ();
			} else {
				idleTime -= Time.deltaTime;
			}
		} else {
			if (walkingDistance <= 0) {
				changeState();
			} else {
				transform.Translate ((state * speed * Time.deltaTime) / 100, 0, 0);
				walkingDistance -= (speed * Time.deltaTime) / 100;
			}
		}
	}

	private void changeState() {
		state = Random.Range (-1, 2);
		if (state == 0) {
			idleTime = (float) Random.Range (5, 10);			
		} else {
			walkingDistance = (float) Random.Range (1, 3);
		}
	}

}