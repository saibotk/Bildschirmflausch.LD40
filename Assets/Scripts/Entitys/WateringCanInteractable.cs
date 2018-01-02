using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCanInteractable : MonoBehaviour, IInteractable {

	public bool CanInteract(GameObject player) {
		return player.GetComponent<PlayerController>().GetInventory().GetItem<WateringCan>() == null;
	}

	public void Interact(GameObject player) {
		bool added = player.GetComponent<PlayerController> ().GetInventory ().AddItem (new WateringCan ());
		if (added == true) {
			//GameObject.Destroy (this.gameObject);
		}
	}
}
