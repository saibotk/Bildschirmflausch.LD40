using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryJobFactory : JobFactory {
	private GameObject letter;
	private GameObject indicator;

	public DeliveryJobFactory (GameObject letter, GameObject indicator) : base (FACTORYNAMES.DELIVERY) {
		this.letter = letter;
		this.indicator = indicator;
	}

	public override bool CanCreateJob () {
		List<GameObject> aNPCs = Helper.GetAvailable (GameController.instance.GetJobManager().GetJobObjects(Jobmanager.ENTITYLISTNAMES.DELIVERYNPCS));
		if (aNPCs.Count == 0)
			return false;
		return true;
	}

	override public Job CreateJob() {
		List<GameObject> aNPCs = Helper.GetAvailable (GameController.instance.GetJobManager().GetJobObjects(Jobmanager.ENTITYLISTNAMES.DELIVERYNPCS));
		if (aNPCs.Count == 0)
			return null;

		GameObject npc = aNPCs [Random.Range (0, aNPCs.Count)];
		return new DeliveryJob (npc.GetComponent<NPC> (), GameController.instance.GetJobManager().GetJobObjects(Jobmanager.ENTITYLISTNAMES.LETTERSPAWNPOINTS)[0].transform, letter, indicator); // TODO check spawnpoint null etc
	}
}
