using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringJob : Job {
	private List<JobEntity> targets;
	private Jobmanager jobmanager;
	private GameObject indicatorPrefab;

	public WateringJob(List<JobEntity> targets, Jobmanager manager, GameObject indicatorPrefab) : base ("Watering the plants", "Water em all!", 20f, 25, Resources.Load<Sprite>("wateringCan")) {
		this.targets = targets;
		this.jobmanager = manager;
		this.indicatorPrefab = indicatorPrefab;
		this.indicatorPrefab.GetComponent<SpriteRenderer>().color = GetJobColor();
	}

	override public void init() {
		foreach (JobEntity target in targets) {
			target.SetAvailable (false);
			target.SetJob (this);
			GameObject indicator = GameObject.Instantiate (indicatorPrefab, target.transform);
			indicator.SetActive (true);
			target.SetIndicator(indicator);
			target.SetInteract (
				delegate (GameObject player) {
					if (player.GetComponent<PlayerController> ().GetInventory ().leftHand != null) {
						if (player.GetComponent<PlayerController> ().GetInventory ().leftHand is WateringCan) {
							cleanupPlant(target);
							targets.Remove(target);
							if(this.targets.Count == 0)
								this.FinishJob ();
						}
					}
				}
			);
		}
	}

	private void cleanupPlant(JobEntity target) {
		target.SetInteract (null);
		target.SetJob (null);
		target.SetAvailable (true);
		GameObject.Destroy (target.GetIndicator ());
	}

	public void FinishJob() {
		this.jobmanager.FinishedJob(this);
	}

	override public void cleanup() {
		targets.ForEach (cleanupPlant);
		targets.Clear ();
		this.indicatorPrefab = null;
		this.targets = null;
		this.jobmanager = null;
	}
}
