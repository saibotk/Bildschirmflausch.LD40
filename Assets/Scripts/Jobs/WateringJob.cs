using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringJob : Job {
	private List<JobEntity> targets;
	private GameObject indicatorPrefab;

	public WateringJob(List<JobEntity> targets, GameObject indicatorPrefab) : base ("Watering the plants", "Water em all!", 20f, 25, Resources.Load<Sprite>("wateringCan")) {
		this.targets = targets;
		this.indicatorPrefab = indicatorPrefab;
		this.indicatorPrefab.GetComponent<SpriteRenderer>().color = jobColor = Random.ColorHSV (110f/360f, 130f/360f, 0.2f, 0.8f, 0.3f, 1.0f);
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
		GameController.instance.GetJobManager().FinishedJob(this);
	}

	override public void cleanup() {
		targets.ForEach (cleanupPlant);
		targets.Clear ();
		this.indicatorPrefab = null;
		this.targets = null;
	}
}
