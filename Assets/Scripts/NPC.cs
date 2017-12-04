using System;
using UnityEngine;

public class NPC : JobEntitiy {

	[SerializeField]
	private int floor = 0;
	[SerializeField]
	public bool questNPC;

	private bool available; // If it can execute a job. Ignored if questNPC = false

	public NPC ()	{
	}

	public void setAvailable(bool available) {
		this.available = available;
	}

	public bool isAvailable(){
		return available;
	}

	public int getFloor() {
		return floor;
	}
}