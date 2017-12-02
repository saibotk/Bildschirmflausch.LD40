using System;

public class Item<Type> {
	public readonly String name;
	public Type value;

	public Item (String name, Type value) {
		this.name = name;
		this.value = value;
	}
}