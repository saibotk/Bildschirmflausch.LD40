using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class JobNPC : MonoBehaviour, Interactable {

	private Job job = null;
	private Func<GameObject, bool> interactMethod = null;
	[SerializeField]
	private string name;

	public void Interact(GameObject player) {
		
		if (interactMethod != null) {
			interactMethod (player);
		}
	}

	public void SetInteract(Func<GameObject, bool> interact) {
		interactMethod = interact;
	}

	public void ShowName() {
		// TODO
	}

	public void HideName() {
		// TODO
	}

	public void SetJob(Job job) {
		this.job = job;
	}

}
