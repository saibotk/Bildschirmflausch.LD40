using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryJobType : JobType<DeliveryJob> {

	public DeliveryJobType (GameController controller, Jobmanager manager) : base ("delivery", controller, manager) {
	}

	override public DeliveryJob CreateJob() {
		List<GameObject> aNPCs = getAvailable (controller.GetJobObjects ("DeliveryNPCs"));
		if (aNPCs.Count == 0)
			return null;

		GameObject npc = aNPCs [Random.Range (0, aNPCs.Count)];
		return new DeliveryJob (npc.GetComponent<NPC> (), controller.GetPrefab("__SpawnpointLetter").transform, controller.GetPrefab("QuestLetter"), manager, controller.GetPrefab ("Indicator_0"));
	}
}
