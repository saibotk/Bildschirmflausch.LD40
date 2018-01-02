using System;
using UnityEngine;

public abstract class Item {
	public readonly string name;
	protected readonly Sprite icon;
	public readonly bool isDestroyable = false;
	public readonly bool isDroppable = true;

	public Item (string name, Sprite icon) {
		this.name = name;
		this.icon = icon;
	}

	public Item (string name, Sprite icon, bool destroyable, bool droppable) {
		this.name = name;
		this.icon = icon;
		this.isDestroyable = destroyable;
		this.isDroppable = droppable;
	}

	public override bool Equals (object obj) {
		if (obj is Item) {
			if (obj == null)
				return false;
			return ((Item) obj).name == this.name;
		}
		return base.Equals (obj);
	}
		
	public virtual Sprite GetIcon() {
		return this.icon;
	}
}
