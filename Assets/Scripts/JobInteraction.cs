﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class JobInteraction : MonoBehaviour, Interactable {

	private Job job = null;
	private Action<GameObject> interactMethod = null;

	public void Interact(GameObject player) {

		if (interactMethod != null) {
			interactMethod (player);
		}
	}

	public void SetInteract(Action<GameObject> interact) {
		interactMethod = interact;
	}

	public void SetJob(Job job) {
		this.job = job;
	}

}