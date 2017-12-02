using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour, Interactable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Define what to do when a Player interacts with the Coffeemachine
	public void Interact( GameObject player ) {
		Inventory inv = player.GetComponent<PlayerController> ().GetInventory ();
		inv.coffeePot.value = 6;
	}
}
