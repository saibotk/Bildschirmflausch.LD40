using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCanInteractable : MonoBehaviour, Interactable {

	public void Interact(GameObject player) {
		bool added = player.GetComponent<PlayerController> ().GetInventory ().AddItem (new WateringCan ());
		if (added == true) {
			Debug.Log ("Watering Can interact");
			GameObject.Destroy (this.gameObject);
		}
	}
}
