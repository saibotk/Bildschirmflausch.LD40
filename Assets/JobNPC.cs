using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class JobNPC : MonoBehaviour, Interactable {

	private Func<GameObject, bool> interactMethod = null;

	public void Interact(GameObject player) {
		
		if (interactMethod != null) {
			interactMethod (player);
		}
	}

	public void SetInteract(Func<GameObject, bool> interact) {
		interactMethod = interact;
	}
}
