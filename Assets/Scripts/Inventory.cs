﻿using System.Collections;
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
		// TODO UGLY CODE!!!!
		// And it got worse
		if (item is Letter) {
			if (leftHand is LetterBundle) {
				((LetterBundle)leftHand).Add ((Letter) item);
				UpdateGUI ();
				return true;
			} else if (pocket is LetterBundle) {
				((LetterBundle)pocket).Add ((Letter) item);
				UpdateGUI ();
				return true;
			} else {
				if (leftHand == null) {
					LetterBundle lb = new LetterBundle ();
					lb.Add ((Letter)item);
					this.leftHand = lb;
					UpdateGUI ();
					return true;
				}

				//if (pocket == null)
				{
					pocket = leftHand;
					LetterBundle lb = new LetterBundle ();
					lb.Add ((Letter)item);
					leftHand = lb;
					UpdateGUI ();
					return true;
				}

				//return false;
			}
		} else {
			// !!! NO UNIQUE ITEMS POSSIBLE -> JUST FOR WATERING CAN N STUFF
			if (leftHand != null && leftHand.GetType() == item.GetType() || pocket != null && pocket.GetType() == item.GetType())
				return false;
			// Place in left hand
			if (leftHand == null) {
				this.leftHand = item;
				UpdateGUI ();
				return true;
			}

			// Place in pocket
			if (pocket == null) {
				pocket = leftHand;
				leftHand = item;
				UpdateGUI ();
				return true;
			}
				
			if ((pocket is WateringCan && item is WateringCan) || (pocket is Broom && item is Broom)
				|| (pocket is WateringCan && leftHand is WateringCan) || (pocket is Broom && leftHand is Broom)) {
				pocket = leftHand;
				leftHand = item;
				UpdateGUI ();
				return true;
			}
		}
		return false;
	}

	public void RemoveItem(Item item) {
		if (leftHand != null && (leftHand is LetterBundle) && (item is Letter)) {
			((LetterBundle)leftHand).Remove ((Letter)item);
			if (((LetterBundle) leftHand).letters.Count == 0)
				leftHand = null;
		}
		if (pocket != null && (pocket is LetterBundle) && (item is Letter)) {
			((LetterBundle) pocket).Remove ((Letter)item);
			if (((LetterBundle) pocket).letters.Count == 0)
				pocket = null;
		}
		if (leftHand != null && leftHand.Equals (item)) {
			leftHand = null;
		}
		if (pocket != null && pocket.Equals (item)) {
			pocket = null;
		}
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
