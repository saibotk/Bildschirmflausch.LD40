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
	private Func<GameObject, bool> canInteractMethod = null;
	private bool available = true;

	public void Interact(GameObject player) {
		if (interactMethod != null) {
			interactMethod (player);
		}
	}

	public bool CanInteract(GameObject player) {
		if (interactMethod != null) {
			if (canInteractMethod != null) {
				return canInteractMethod (player);
			}
			return true;
		} else {
			return false;
		}
	}

	public void SetInteract(Action<GameObject> interact) {
		this.interactMethod = interact;
	}

	public void SetCanInteract(Func<GameObject, bool> canInteract) {
		this.canInteractMethod = canInteract;
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
