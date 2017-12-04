using System;
using UnityEngine;

public class NPC : JobEntitiy {

	[SerializeField]
	private int floor = 0;
	[SerializeField]
	public bool isQuestNPC;

	public NPC ()	{
	}

	public int getFloor() {
		return floor;
	}
}