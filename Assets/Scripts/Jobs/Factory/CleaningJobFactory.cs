using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningJobFactory : JobFactory {
	private GameObject dirt;
	private GameObject broom;
	private GameObject indicator;

	public CleaningJobFactory (GameObject dirt, GameObject broom, GameObject indicator) : base (FACTORYNAMES.CLEANING) {
		this.dirt = dirt;
		this.broom = broom;
		this.indicator = indicator;
	}

	public override bool CanCreateJob () {
		List<GameObject> aDirtSpots = Helper.GetAvailable (GameController.instance.GetJobManager().GetJobObjects (Jobmanager.ENTITYLISTNAMES.DIRTSPAWNPOINTS));
		if (aDirtSpots.Count == 0 || GameController.instance.GetFloor() < 2)
			return false;
		return true;
	}

	override public Job CreateJob() {
		List<GameObject> aDirtSpots = Helper.GetAvailable (GameController.instance.GetJobManager().GetJobObjects (Jobmanager.ENTITYLISTNAMES.DIRTSPAWNPOINTS));
		if (aDirtSpots.Count == 0 || GameController.instance.GetFloor() < 2)
			return null;

		int cindex = Random.Range (0, Mathf.Max (0, aDirtSpots.Count - 3));
		int ccount = (aDirtSpots.Count >= 3) ? 3 : aDirtSpots.Count;
		// TODO check spawnpoint null etc
		return new CleaningJob (aDirtSpots.GetRange (cindex, ccount), dirt, GameController.instance.GetJobManager().GetJobObjects(Jobmanager.ENTITYLISTNAMES.BROOMSPAWNPOINTS)[0].transform, broom, indicator);
	}
}
