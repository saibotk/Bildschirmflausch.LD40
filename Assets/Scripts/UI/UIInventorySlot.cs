using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventorySlot : MonoBehaviour {
	[SerializeField]
	private UnityEngine.UI.Image image;
	[SerializeField]
	private UnityEngine.UI.Text countTxt;

	private Item curItem;

	// Use this for initialization
	void Start () {
		RefreshGUI ();
	}

	public void SetItem(Item item) {
		curItem = item;
		RefreshGUI ();
	}

	public void RefreshGUI() {
		if (this.curItem != null) {
			this.image.sprite = this.curItem.GetIcon ();
		} else {
			this.image.sprite = null;
		}
		if (this.curItem != null && this.curItem.GetType ().IsSubclassOf (typeof(Inventory))) {
			this.countTxt.text = ((Inventory)curItem).GetSize ().ToString();
		} else {
			this.countTxt.text = "";
		}
	}
}
