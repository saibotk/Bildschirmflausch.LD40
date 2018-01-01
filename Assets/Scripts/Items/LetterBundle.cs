using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBundle : Inventory {

	public LetterBundle () : base ("Letter Bundle", Resources.Load<Sprite>("letterBundle"), true) {
	}

	/// <summary>
	/// Removes the item.
	/// </summary>
	/// <param name="item">Item.</param>
	public override void RemoveItem (Item item) {
		if(item.GetType() == typeof(Letter)) {
			base.RemoveItem (item);
		} 
	}

	/// <summary>
	/// Adds the item.
	/// </summary>
	/// <returns>true</returns>
	/// <c>false</c>
	/// <param name="item">Item.</param>
	public override bool AddItem (Item item) {
		if(item.GetType() == typeof(Letter)) {
			base.AddItem (item);
			return true;
		} else {
			return false;
		}
	}

	public override Sprite GetIcon ()
	{
		if (GetSize () > 1) {
			return this.icon;
		} else if (GetItem<Letter>() != null) {
			return GetItem<Letter>().GetIcon();
		}
		return Resources.Load<Sprite>("missingTexture");
	}
}
