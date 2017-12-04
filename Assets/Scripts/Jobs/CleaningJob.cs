using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningJob : Job {

	private List<Transform> dirtSpots;
	private GameObject dirtSpotPrefab;
	private Transform broomSpawn;
	private GameObject broomPrefab;
	private Jobmanager manager;
	private GameObject broomGO;
	private List<GameObject> dirtSpotsGO;

	public CleaningJob(List<Transform> dirtSpots, GameObject dirtSpotPrefab,Transform broomSpawn, GameObject broomPrefab, Jobmanager manager) : base ("Clean", "Clean all the dirtspots", 30f, 50) {
		this.dirtSpots = dirtSpots;
		this.dirtSpotPrefab = dirtSpotPrefab;
		this.broomSpawn = broomSpawn;
		this.broomPrefab = broomPrefab;
		this.manager = manager;
		init ();
	}

	override public void init() {
		this.broomGO = GameObject.Instantiate (this.broomPrefab, this.broomSpawn.position, this.broomSpawn.rotation);
		this.broomGO.GetComponent<JobInteraction> ().SetJob (this);
		this.broomGO.GetComponent<JobInteraction> ().SetInteract (delegate (GameObject player) {
			bool added = player.GetComponent<PlayerController> ().GetInventory ().AddItem (new Broom (this));
			if (added) {
				GameObject.Destroy(this.broomGO);
			}
		});
		dirtSpotsGO = new List<GameObject> ();
		foreach (Transform dirtSpot in dirtSpots) {
			dirtSpotsGO.Add (GameObject.Instantiate (this.dirtSpotPrefab, dirtSpot.position, dirtSpot.rotation));
		}


		foreach (GameObject dirtGo in dirtSpotsGO) {
			dirtGo.GetComponent<JobInteraction> ().SetJob (this);
			dirtGo.GetComponent<JobInteraction> ().SetInteract (
				delegate(GameObject player) {
					if (player.GetComponent<PlayerController> ().GetInventory ().leftHand != null) {
						if (player.GetComponent<PlayerController> ().GetInventory ().leftHand is Broom) {
							this.dirtSpotsGO.Remove (dirtGo);
							if (this.dirtSpotsGO.Count == 0) {
								player.GetComponent<PlayerController>().GetInventory().leftHand = null;
								Debug.Log("Removed Broom from Inventory");
								this.finishJob ();
							}
							GameObject.Destroy (dirtGo);
						}
					}
				}
			);
		}

	}

	public void finishJob() {
		Debug.Log ("QUEST COMPLETED! U AQUAMAN!");
		this.manager.finishedJob (this);
	}

	override public void cleanup() {
		GameObject.Destroy (this.broomGO);
		this.manager.GetGameController ().GetPlayer ().GetInventory ().RemoveItem (new Broom (this));
		if (dirtSpotsGO != null && dirtSpotsGO.Count != 0) {
			List<GameObject> tmpDirtGO = new List<GameObject> (dirtSpotsGO);
			foreach (GameObject dirtGo in tmpDirtGO) {
				GameObject.Destroy (dirtGo);
			}
			foreach (Transform t in dirtSpots) {
				this.manager.GetGameController ().availableQuestDirtSpots.Add (t);
			}
		}
	}

}
