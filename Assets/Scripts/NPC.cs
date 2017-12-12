using System;
using UnityEngine;

public class NPC : JobEntitiy {

	[SerializeField]
	public bool QuestNPC;

	public bool isQuestNPC() {
		return QuestNPC;
	}
}