using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General Inventory class.
/// </summary>
public abstract class Inventory : Item {

	/// <summary>
	/// The slots which are dynamic, so that an added item is not determined to keep its position.
	/// </summary>
	protected Dictionary<string, Item> slots;

	protected bool isInfinite = true;


	/// <summary>
	/// Initializes a new instance of the <see cref="Inventory"/> class.
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="icon">Icon.</param>
	public Inventory(string name, Sprite icon) : base(name, icon, false, false) {
		this.slots = new Dictionary<string, Item> ();
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Inventory"/> class.
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="icon">Icon.</param>
	/// <param name="infinite">If set to <c>true</c> infinite.</param>
	public Inventory(string name, Sprite icon, bool infinite) : base(name, icon, false, false) {
		this.isInfinite = infinite;
		this.slots = new Dictionary<string, Item> ();
	}
		
	/// <summary>
	/// Gets the size of the inventory.
	/// </summary>
	/// <returns>The size.</returns>
	public int GetSize() {
		return slots.Count;
	}

	/// <summary>
	/// Gets the first occurance of a specific item instance in the inventory.
	/// </summary>
	/// <returns>The item.</returns>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public T GetItem<T>() where T : Item {
		if (slots.Values.Count != 0) {
			Item i = (new List<Item> (slots.Values)).Find (x => x != null && x.GetType () == typeof(T));
			return (T)i;
		}
		return null;
	}

	/// <summary>
	/// Adds the item.
	/// </summary>
	/// <returns><c>true</c>, if item was added, <c>false</c> otherwise.</returns>
	/// <param name="item">Item.</param>
	public virtual bool AddItem(Item item) {
		if (item == null || slots.Count == 0) {
			if (this.isInfinite) {
				slots.Add (item.GetHashCode ().ToString (), item);
				return true;
			} else {
				return false;
			}
		}
		List<Item> li = new List<Item> (slots.Values);
		if (li.Find (x => x != null && x.Equals (item)) != null)
			return false;

		if (this.isInfinite && !slots.ContainsKey(item.GetHashCode().ToString())) {
			slots.Add (item.GetHashCode ().ToString (), item);
			return true;
		} else {
			Item tmpreplace = null;
			bool replaced = false;
			List<string> tmplist = new List<string> (slots.Keys);
			foreach (string k in tmplist) {
				if (slots[k] != null && slots [k].isDroppable == false)
					continue;
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
			return replaced;
		}
	}

	/// <summary>
	/// Removes the item in the slot with the given name.
	/// </summary>
	/// <param name="sn">Slot key.</param>
	public virtual void RemoveItemInSlot(string sn) {
		if (slots.ContainsKey (sn)) {
			slots [sn] = null;
		}
		OrganizeInventory ();
	}

	/// <summary>
	/// Removes the item.
	/// </summary>
	/// <param name="item">Item.</param>
	public virtual void RemoveItem(Item item) {
		List<string> tmp = new List<string> (slots.Keys);
		foreach (string k in tmp) {
			if (slots [k] != null && slots [k].Equals (item)) {
				if (this.isInfinite) {
					slots.Remove (k);
				} else {
					slots [k] = null;
				}
			}
		}

		OrganizeInventory ();
	}

	/// <summary>
	/// Gets an item from a specific slot.
	/// </summary>
	/// <returns>The item.</returns>
	/// <param name="slot">Slot name.</param>
	public virtual Item GetItem(string slot) {
		if (!slots.ContainsKey (slot))
			return null;
		return slots [slot];
	}

	/// <summary>
	/// Determines whether an item is in the inventory.
	/// </summary>
	/// <returns><c>true</c> if the item is in this inventory the method returns <c>true</c> otherwise, <c>false</c>.</returns>
	/// <param name="i">The item.</param>
	public virtual bool IsInInventory(Item i) {
		if (i == null || slots.Count == 0)
			return false;

		List<Item> li = new List<Item> (slots.Values);
		return li.Contains (i);
	}

	/// <summary>
	/// Organizes the inventory.
	/// </summary>
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
}


