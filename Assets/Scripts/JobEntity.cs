using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class JobEntity : MonoBehaviour, IInteractable, IAvailable {

	[SerializeField]
	private int floor = 0;
	[SerializeField]
	protected Jobmanager.ENTITYLISTNAMES jobEntityListName;

	private Job job = null;
	private GameObject indicator;
	private Action<GameObject> interactMethod = null;
	private Func<GameObject, bool> canInteractMethod = null;
	private bool available = true;

	void Start() {
		Init ();
	}
		
	protected void Init() {
		if (jobEntityListName != Jobmanager.ENTITYLISTNAMES.UNDEFINED) {
			GameController.instance.GetJobManager().AddJobObject (jobEntityListName, this.gameObject);
		}
	}

	public virtual void Interact(GameObject player) {
		if (interactMethod != null) {
			interactMethod (player);
		}
	}

	public virtual bool CanInteract(GameObject player) {
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
