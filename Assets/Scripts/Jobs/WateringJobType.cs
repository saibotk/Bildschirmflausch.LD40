using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringJobType : JobType<WateringJob> {

	public WateringJobType (GameController controller, Jobmanager manager) : base ("watering", controller, manager) {
	}

	override public WateringJob CreateJob() {
		List<GameObject> aPlants = getAvailable (getGameObjects ("WateringPlants"));
		if (aPlants.Count == 0)
			return null;
		int index = Random.Range (0, Mathf.Max (0, aPlants.Count - 3));
		int count = (aPlants.Count >= 3) ? 3 : aPlants.Count;
		List<GameObject> cAPlants = new List<GameObject> (aPlants);
		return new WateringJob (cAPlants.ConvertAll<JobEntitiy> (x => x.GetComponent<JobEntitiy> ()).GetRange (index, count), manager, controller.GetPrefab("Indicator_0"));
	}
}
