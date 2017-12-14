using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningJob : Job {

	private List<GameObject> dirtSpots;
	private GameObject dirtSpotPrefab;
	private Transform broomSpawn;
	private GameObject broomPrefab;
	private GameObject indicatorPrefab;
	private Jobmanager manager;
	private GameObject broomGO;
	private List<GameObject> dirtSpotsGO;

	public CleaningJob(List<GameObject> dirtSpots, GameObject dirtSpotPrefab, Transform broomSpawn, GameObject broomPrefab, Jobmanager manager, GameObject indicatorPrefab) : base ("Clean", "Clean all the dirtspots", 50f, 50, Resources.Load<Sprite>("broom")) {
		this.dirtSpots = dirtSpots;
		this.dirtSpotPrefab = dirtSpotPrefab;
		this.indicatorPrefab = indicatorPrefab;
		this.indicatorPrefab.GetComponent<SpriteRenderer>().color = GetJobColor();
		this.broomSpawn = broomSpawn;
		this.broomPrefab = broomPrefab;
		this.manager = manager;
		init ();
	}

	override public void init() {
		dirtSpotsGO = new List<GameObject> ();
		foreach (GameObject dirtSpot in dirtSpots) {
			dirtSpotsGO.Add (GameObject.Instantiate (this.dirtSpotPrefab, dirtSpot.transform.position, dirtSpot.transform.rotation));
			(dirtSpot.GetComponent (typeof(IAvailable)) as IAvailable).SetAvailable (false);
		}
		foreach (GameObject dirtGo in dirtSpotsGO) {
			dirtGo.GetComponent<JobEntitiy> ().SetJob (this);
			dirtGo.GetComponent<JobEntitiy> ().SetIndicator (GameObject.Instantiate(indicatorPrefab, dirtGo.transform));
			dirtGo.GetComponent<JobEntitiy> ().SetInteract (
				delegate(GameObject player) {
					if (player.GetComponent<PlayerController> ().GetInventory ().leftHand != null) {
						if (player.GetComponent<PlayerController> ().GetInventory ().leftHand is Broom) {
							this.dirtSpotsGO.Remove (dirtGo);
							if (this.dirtSpotsGO.Count == 0) {
								this.finishJob ();
							}
							GameObject.Destroy(dirtGo.GetComponent<JobEntitiy>().GetIndicator());
							GameObject.Destroy (dirtGo);
						}
					}
				}
			);
		}

	}

	public void finishJob() {
		this.manager.finishedJob (this);
	}

	override public void cleanup() {
		GameObject.Destroy (this.broomGO);
		if (dirtSpotsGO != null && dirtSpotsGO.Count != 0) {
			List<GameObject> tmpDirtGO = new List<GameObject> (dirtSpotsGO);
			foreach (GameObject dirtGo in tmpDirtGO) {
				GameObject.Destroy(dirtGo.GetComponent<JobEntitiy>().GetIndicator());
				GameObject.Destroy (dirtGo);
			}
			foreach (GameObject t in dirtSpots) {
				(t.GetComponent (typeof(IAvailable)) as IAvailable).SetAvailable (true);
			}
		}
	}
}
