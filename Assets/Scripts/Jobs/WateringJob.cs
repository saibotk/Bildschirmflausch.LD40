using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringJob : Job {
	private List<JobEntitiy> targets;
	private Jobmanager jobmanager;

	public WateringJob(List<JobEntitiy> targets, Jobmanager manager) : base ("Watering the plants", "Water em all!", 30f, 50) {
		this.targets = targets;
		this.jobmanager = manager;
		init ();
	}

	override public void init() {
		List<JobEntitiy> tmptargets = new List<JobEntitiy> (targets);
		foreach (JobEntitiy target in tmptargets) {
			target.setAvailable (false);
			target.SetJob (this);
			target.SetInteract (
				delegate (GameObject player) {
					if (player.GetComponent<PlayerController> ().GetInventory ().leftHand != null) {
						if (player.GetComponent<PlayerController> ().GetInventory ().leftHand is WateringCan) {
							Debug.Log ("Used Watering Can");
							target.SetInteract (null);
							targets.Remove(target);
							if(this.targets.Count == 0) {
								this.finishJob ();
							}
						}
					}
				}
			);
		}
	}

	public void finishJob() {
		Debug.Log("QUEST COMPLETED! U GENIUS");
		this.jobmanager.finishedJob(this);
	}

	override public void cleanup() {
		foreach (JobEntitiy target in targets) {
			target.setAvailable (true);
			target.SetInteract (null);
			target.SetJob (null);
			target.setAvailable (true);
		}
		this.targets = null;
		this.jobmanager = null;
	}

}
