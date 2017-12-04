using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {
	public Item leftHand = null;
	public Item pocket = null;
	public CoffeePot coffeePot = null;
	private GameUI gui;

	public Inventory(GameUI ui) {
		this.gui = ui;
	}

	public bool AddItem(Item item) {
		if(leftHand == null) {
			this.leftHand = item;
			UpdateGUI ();
			return true;
		}

		if (pocket == null) {
			pocket = leftHand;
			leftHand = item;
			UpdateGUI ();
			return true;
		}
		return false;
	}

	public void RemoveItem(Item item) {
		if (leftHand != null && leftHand.Equals (item))
			leftHand = null;
		if (pocket != null && pocket.Equals (item))
			pocket = null;
		UpdateGUI ();
	}

	private void UpdateGUI() {
		gui.SetLeftHandImage (((leftHand == null) ? null : leftHand.icon));
		gui.SetPocketImage (((pocket == null) ? null : pocket.icon));
		//gui.SetRightHandImage ();
	}

	public void Swap() {
		Item tmp = leftHand;
		this.leftHand = pocket;
		this.pocket = tmp;
		UpdateGUI ();
		Debug.Log ("Swapped Item to " + ((leftHand != null) ? leftHand.name : "null"));
	}
}
