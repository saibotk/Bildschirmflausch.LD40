using System;
using UnityEngine;

public abstract class Item {
	public readonly String name;
	public readonly Sprite icon;

	public Item (String name, Sprite icon) {
		this.name = name;
		this.icon = icon;
	}

	public bool IsEqual(Item i) {
		if (i == null)
			return false;
		return i.name == this.name;
	}
}
