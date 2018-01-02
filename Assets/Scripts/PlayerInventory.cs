using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inventory implementation for the player inventory.
/// </summary>
public class PlayerInventory : Inventory {
	
	public CoffeePot coffeePot = null;

	public PlayerInventory() : base("Player Inventory", null, false){
		slots.Add ("leftHand", null);
		slots.Add ("pocket", null);
	}

	/// <summary>
	/// Adds the item.
	/// </summary>
	/// <returns><c>true</c>, if item was added, <c>false</c> otherwise.</returns>
	/// <param name="item">Item.</param>
	public override bool AddItem(Item item) {
		if (item.GetType ().IsSubclassOf (typeof(Inventory))) {
			((Inventory)item).SetOnChangeCallback (delegate (Inventory obj) {
				UpdateGUI();
			});
		}
		if (base.AddItem (item)) {
			UpdateGUI ();
			return true;
		}
		return false;
	}

	/// <summary>
	/// Removes the item in the slot with the given name.
	/// </summary>
	/// <param name="sn">Slot key.</param>
	public override void RemoveItemInSlot(string sn) {
		base.RemoveItemInSlot (sn);
		UpdateGUI ();
	}

	/// <summary>
	/// Removes the item.
	/// </summary>
	/// <param name="item">Item.</param>
	public override void RemoveItem(Item item) {
		base.RemoveItem (item);
		UpdateGUI ();
	}

	/// <summary>
	/// Swap items of the two hand slots.
	/// </summary>
	public void Swap() {
		Item tmp = this.slots["leftHand"];
		this.slots["leftHand"] = this.slots["pocket"];
		this.slots["pocket"] = tmp;
		UpdateGUI ();
	}

	/// <summary>
	/// Drop the specified item.
	/// </summary>
	/// <param name="key">The key.</param>
	public virtual void Drop(string key) {
		if (key == null || slots.Count == 0)
			return;
	}

	/// <summary>
	/// Updates the GUI.
	/// </summary>
	private void UpdateGUI() {
		GameUI.instance.SetLeftHandSlot (slots["leftHand"]);
		GameUI.instance.SetPocketSlot (slots["pocket"]);
	}

}
