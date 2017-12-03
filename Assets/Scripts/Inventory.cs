using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {
	public Item leftHand = null;
	public Item pocket = null;
	public CoffeePot coffeePot = null;

	public bool AddItem(Item item) {
		if(leftHand == null) {
			this.leftHand = item;
			return true;
		}

		if (pocket == null) {
			pocket = leftHand;
			leftHand = item;
			return true;
		}

		return false;
	}

	public void RemoveItem(Item item) {
		if (leftHand != null && leftHand.Equals (item))
			leftHand = null;
		if (pocket != null && pocket.Equals (item))
			pocket = null;
	}

	public void Swap() {
		Item tmp = leftHand;
		this.leftHand = pocket;
		this.pocket = tmp;
	}
}
