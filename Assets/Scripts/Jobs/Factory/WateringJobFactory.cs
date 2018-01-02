using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringJobFactory : JobFactory {
	private GameObject indicator;

	public WateringJobFactory (GameObject indicator, float probability) : base (FACTORYNAMES.WATERING, probability) {
		this.indicator = indicator;
	}

	public override bool CanCreateJob () {
		List<GameObject> aPlants = Helper.GetAvailable (GameController.instance.GetJobManager().GetJobObjects (Jobmanager.ENTITYLISTNAMES.WATERINGPLANTS));
		if (aPlants.Count == 0) {
			return false;
		} 
		return true;
	}

	override public Job CreateJob() {
		List<GameObject> aPlants = Helper.GetAvailable (GameController.instance.GetJobManager().GetJobObjects (Jobmanager.ENTITYLISTNAMES.WATERINGPLANTS));
		if (aPlants.Count == 0) {
			return null;
		}
		int index = Random.Range (0, Mathf.Max (0, aPlants.Count - 3));
		int count = (aPlants.Count >= 3) ? 3 : aPlants.Count;
		List<GameObject> cAPlants = new List<GameObject> (aPlants);
		return new WateringJob (cAPlants.ConvertAll<JobEntity> (x => x.GetComponent<JobEntity> ()).GetRange (index, count), indicator);
	}
}