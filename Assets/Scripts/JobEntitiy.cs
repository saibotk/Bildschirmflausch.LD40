using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class JobEntitiy : MonoBehaviour, IInteractable, IAvailable {

	private Job job = null;
	private Action<GameObject> interactMethod = null;
	private bool available = true;

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

	public void setAvailable(bool b) {
		this.available = b;
	}

	public bool isAvailable() {
		return this.available;
	}
}
