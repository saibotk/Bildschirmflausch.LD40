using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCanInteractable : MonoBehaviour, IInteractable {

	[SerializeField]
	public bool isBroom = false;

	public void Interact(GameObject player) {
		if (isBroom)
			player.GetComponent<PlayerController> ().GetInventory ().AddItem (new Broom(null));
		else
			player.GetComponent<PlayerController> ().GetInventory ().AddItem (new WateringCan ());
		//if (added == true) {
		//	GameObject.Destroy (this.gameObject);
		//}
	}
}
