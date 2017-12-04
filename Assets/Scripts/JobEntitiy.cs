using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class JobEntitiy : MonoBehaviour, IInteractable, IAvailable {

	[SerializeField]
	private int floor = 0;
	private Job job = null;
	private GameObject indicator;
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

	public void SetIndicator(GameObject g) {
		this.indicator = g;
	}

	public GameObject GetIndicator() {
		return this.indicator;
	}

	public void SetAvailable(bool b) {
		this.available = b;
	}

	public bool IsAvailable(int floor) {
		return available && GetFloor () <= floor;
	}

	public int GetFloor() {
		return floor;
	}
}
