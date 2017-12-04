using System;
using UnityEngine;

public class Item {
	public readonly String name;
	public readonly Sprite icon;

	public Item (String name, Sprite icon) {
		this.name = name;
		this.icon = icon;
	}
}
