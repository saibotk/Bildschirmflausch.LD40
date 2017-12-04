﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour, Interactable {
	private CoffeePot cpot = null;
	private Animator canimator;
	private AudioSource caudio;
	[SerializeField]
	private SpriteRenderer sr;
	[SerializeField]
	private float brewingTime = 2;
	private float currentBrewingTime;

	// Use this for initialization
	void Start () {
		canimator = gameObject.GetComponent<Animator> ();
		canimator.SetFloat ("brewingTimeMultiplier", (1f / brewingTime)); // DOESNT WORK!
		currentBrewingTime = brewingTime;
		caudio = gameObject.GetComponentInChildren<AudioSource> ();
	}

	void Update() {
		if (cpot != null) {
			if (currentBrewingTime > 0) {
				currentBrewingTime -= Time.deltaTime;
			} else {
				if (canimator.GetBool ("brewing")) {
					canimator.SetBool ("brewing", false);
					caudio.Stop ();
				}
			}
		}
	}

	// Define what to do when a player interacts with the coffeemachine
	public void Interact( GameObject player ) {
		Inventory inv = player.GetComponent<PlayerController> ().GetInventory ();
		if (inv.coffeePot != null && cpot == null) {
			cpot = inv.coffeePot;
			inv.coffeePot = null;
			currentBrewingTime = brewingTime;
			canimator.SetFloat ("brewingTimeMultiplier", (1f / brewingTime));
			canimator.SetBool ("brewing", true);
			canimator.SetBool ("empty", false);
			caudio.Play ();

		} else if (currentBrewingTime <= 0 && cpot != null && inv.coffeePot == null ) {
			cpot.fill(cpot.getMaxFillLevel());
			inv.coffeePot = cpot;
			cpot = null;
			canimator.SetBool ("empty", true);
		}

	}
}
