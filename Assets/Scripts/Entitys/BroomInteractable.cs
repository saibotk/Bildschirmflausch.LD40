using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomInteractable : MonoBehaviour, IInteractable {

	public bool CanInteract(GameObject player) {
		return player.GetComponent<PlayerController>().GetInventory().GetItem<Broom>() == null;
	}

	public void Interact(GameObject player) {
		bool added = player.GetComponent<PlayerController> ().GetInventory ().AddItem (new Broom());
		if (added == true) {
		//	GameObject.Destroy (this.gameObject);
		}
	}
}
