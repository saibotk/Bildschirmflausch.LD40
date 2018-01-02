using System;
using UnityEngine;

public class NPC : JobEntity {

	[SerializeField]
	public bool QuestNPC;

	public bool isQuestNPC() {
		return QuestNPC;
	}
}