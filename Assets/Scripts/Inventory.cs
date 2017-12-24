using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {
	protected Dictionary<string, Item> slots = new Dictionary<string, Item> ();
	public CoffeePot coffeePot = null;

	public Inventory() {
		slots.Add ("leftHand", null);
		slots.Add ("pocket", null);
	}

	public Item GetItem(string slot) {
		if (!slots.ContainsKey (slot))
			return null;
		return slots [slot];
	}

	public bool AddItem(Item item) {
		if (item == null || slots.Count == 0)
			return false;
		List<Item> li = new List<Item> (slots.Values);
		if (li.Find (x => x != null && x.IsEqual (item)) != null)
			return false;

		Item tmpreplace = null;
		bool replaced = false;
		List<string> tmplist = new List<string> (slots.Keys);
		foreach (string k in tmplist) {
			if (tmpreplace != null && tmpreplace != slots [k]) {
				Item tmp2 = slots [k];
				slots [k] = tmpreplace;
				tmpreplace = tmp2;
			}
			if (!replaced) {
				tmpreplace = slots [k];
				slots [k] = item;
				replaced = true;
			}
		}
		UpdateGUI ();
		return true;
		// TODO UGLY CODE!!!!
		// And it got worse
//		if (item is Letter) {
//			if (leftHand is LetterBundle) {
//				((LetterBundle)leftHand).Add ((Letter) item);
//				UpdateGUI ();
//				return true;
//			} else if (pocket is LetterBundle) {
//				((LetterBundle)pocket).Add ((Letter) item);
//				UpdateGUI ();
//				return true;
//			} else {
//				if (leftHand == null) {
//					LetterBundle lb = new LetterBundle ();
//					lb.Add ((Letter)item);
//					this.leftHand = lb;
//					UpdateGUI ();
//					return true;
//				}
//
//				//if (pocket == null)
//				{
//					pocket = leftHand;
//					LetterBundle lb = new LetterBundle ();
//					lb.Add ((Letter)item);
//					leftHand = lb;
//					UpdateGUI ();
//					return true;
//				}
//
//				//return false;
//			}
//		} else {
//			// !!! NO UNIQUE ITEMS POSSIBLE -> JUST FOR WATERING CAN N STUFF
//			if (leftHand != null && leftHand.GetType() == item.GetType() || pocket != null && pocket.GetType() == item.GetType())
//				return false;
//			// Place in left hand
//			if (leftHand == null) {
//				this.leftHand = item;
//				UpdateGUI ();
//				return true;
//			}
//
//			// Place in pocket
//			if (pocket == null) {
//				pocket = leftHand;
//				leftHand = item;
//				UpdateGUI ();
//				return true;
//			}
//				
//			if ((pocket is WateringCan && item is Broom) || (pocket is Broom && item is WateringCan)) {
//				pocket = leftHand;
//				leftHand = item;
//				UpdateGUI ();
//				return true;
//			}
//			if ((leftHand is WateringCan && item is Broom) || (leftHand is Broom && item is WateringCan)) {
//				leftHand = item;
//				UpdateGUI ();
//				return true;
//			}
//		}
//		return false;
	}

	private void OrganizeInventory() {
		List<string> tmplist = new List<string> (slots.Keys);
		List<string> emptyslots = new List<string> ();
		foreach (string k in tmplist) {
			if (slots[k] == null) {
				emptyslots.Add(k);
			}
			if (emptyslots.Count != 0 && slots[k] != null) {
				slots[emptyslots[0]] = slots[k];
				slots [k] = null;
				emptyslots.RemoveAt(0);
			}
		}
	}

	public void RemoveItemInSlot(string sn) {
		slots [sn] = null;
		OrganizeInventory ();
		UpdateGUI ();
	}

	public void RemoveItem(Item item) {
//		if (leftHand != null && (leftHand is LetterBundle) && (item is Letter)) {
//			((LetterBundle)leftHand).Remove ((Letter)item);
//			if (((LetterBundle) leftHand).letters.Count == 0)
//				leftHand = null;
//		}
//		if (pocket != null && (pocket is LetterBundle) && (item is Letter)) {
//			((LetterBundle) pocket).Remove ((Letter)item);
//			if (((LetterBundle) pocket).letters.Count == 0)
//				pocket = null;
//		}
//		if (leftHand != null && leftHand.Equals (item)) {
//			leftHand = null;
//		}
//		if (pocket != null && pocket.Equals (item)) {
//			pocket = null;
//		}

		foreach (string k in slots.Keys) {
			if (slots [k].IsEqual (item))
				slots [k] = null;
		}

		OrganizeInventory ();
		UpdateGUI ();
	}

	public bool IsInInventory(Item i) {
		if (i == null || slots.Count == 0)
			return false;
		
		List<Item> li = new List<Item> (slots.Values);
		return li.Contains (i);
	}

	public void Drop(Item i) {
		if (i == null || slots.Count == 0)
			return;
	}

	private void UpdateGUI() {
		GameUI.instance.SetLeftHandImage (((slots["leftHand"] == null) ? null : slots["leftHand"].icon));
		GameUI.instance.SetPocketImage (((slots["pocket"] == null) ? null : slots["pocket"].icon));
		//gui.SetRightHandImage ();
	}

	public void Swap() {
		Item tmp = this.slots["leftHand"];
		this.slots["leftHand"] = this.slots["pocket"];
		this.slots["pocket"] = tmp;
		UpdateGUI ();
	}
}
