using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningJobType : JobType {

	public CleaningJobType (GameController controller, Jobmanager manager) : base ("cleaning", controller, manager) {
	}

	override public Job CreateJob() {
		List<GameObject> aDirtSpots = getAvailable (controller.GetJobObjects ("DirtSpawnpoints"));
		if (aDirtSpots.Count == 0)
			return null;

		int cindex = Random.Range (0, Mathf.Max (0, aDirtSpots.Count - 3));
		int ccount = (aDirtSpots.Count >= 3) ? 3 : aDirtSpots.Count;
		return new CleaningJob (aDirtSpots.GetRange (cindex, ccount), controller.GetPrefab("Dirt"), controller.GetPrefab("__SpawnpointBroom").transform, controller.GetPrefab("Broom"), manager, controller.GetPrefab ("Indicator_0"));
	}
}
