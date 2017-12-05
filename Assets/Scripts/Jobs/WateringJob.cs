﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringJob : Job {
	private List<JobEntitiy> targets;
	private Jobmanager jobmanager;
	private GameObject indicatorPrefab;

	public WateringJob(List<JobEntitiy> targets, Jobmanager manager, GameObject indicatorPrefab) : base ("Watering the plants", "Water em all!", 30f, 50, Resources.Load<Sprite>("wateringCan")) {
		this.targets = targets;
		this.jobmanager = manager;
		this.indicatorPrefab = indicatorPrefab;
		this.indicatorPrefab.GetComponent<SpriteRenderer>().color = GetJobColor();
		init ();
	}

	override public void init() {
		List<JobEntitiy> tmptargets = new List<JobEntitiy> (targets);
		foreach (JobEntitiy target in tmptargets) {
			target.SetAvailable (false);
			target.SetJob (this);
			target.SetIndicator(GameObject.Instantiate (indicatorPrefab, target.transform));
			target.SetInteract (
				delegate (GameObject player) {
					if (player.GetComponent<PlayerController> ().GetInventory ().leftHand != null) {
						if (player.GetComponent<PlayerController> ().GetInventory ().leftHand is WateringCan) {
							Debug.Log ("Used Watering Can");
							target.SetInteract (null);
							GameObject.Destroy(target.GetIndicator());
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
			target.SetInteract (null);
			target.SetJob (null);
			target.SetAvailable (true);
			GameObject.Destroy (target.GetIndicator ());
		}
		this.indicatorPrefab = null;
		this.targets = null;
		this.jobmanager = null;
	}

}
