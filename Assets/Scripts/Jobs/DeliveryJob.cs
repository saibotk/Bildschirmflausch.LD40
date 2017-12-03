using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryJob : Job {
	private JobInteraction target;
	private Transform interactableSpawnpoint;
	private GameObject letterPrefab;
	private GameObject letterGO;
	private Jobmanager jobmanager;
	private Letter letter;

	public DeliveryJob(JobInteraction target, Transform transform, GameObject prefab, Jobmanager manager) : base ("Delivery", "Deliver the item!", 30f) {
		this.target = target;
		this.letter = new Letter(this);
		this.interactableSpawnpoint = transform;
		this.letterPrefab = prefab;
		this.jobmanager = manager;
		init ();
	}

	override public void init() {
		this.letterGO = GameObject.Instantiate(this.letterPrefab, this.interactableSpawnpoint.position, this.interactableSpawnpoint.rotation);
		this.letterGO.GetComponent<JobInteraction>().SetJob(this);
		this.letterGO.GetComponent<JobInteraction>().SetInteract(
			delegate (GameObject player) {
				bool added = player.GetComponent<PlayerController>().GetInventory().AddItem(this.letter);
				if (added) {
					GameObject.Destroy(this.letterGO);
					Debug.Log("Added Letter to Inventory");
				}
			}
		);

		this.target.SetJob(this);
		this.target.SetInteract (
			delegate (GameObject player) {
				if(player.GetComponent<PlayerController>().GetInventory().leftHand != null) {
					if ( player.GetComponent<PlayerController>().GetInventory().leftHand is Letter ) {
						if (((Letter) player.GetComponent<PlayerController>().GetInventory().leftHand).job == this) {
							player.GetComponent<PlayerController>().GetInventory().leftHand = null;
							Debug.Log("Removed Letter from Inventory");
							this.finishJob();
						}
					}
				}
			}
		);
	}

	public void finishJob() {
		Debug.Log("QUEST COMPLETED! U GENIUS");
		this.jobmanager.RemoveJob(this);
	}

	override public void cleanup() {
		this.jobmanager.GetGameController().GetPlayer().GetInventory().RemoveItem(this.letter);
		this.target.SetInteract (null);
		this.target.SetJob (null);
		this.jobmanager.GetGameController().MakeNPCAvailable(this.target.gameObject);
		this.letterPrefab = null;
		this.letter = null;
		GameObject.Destroy(this.letterGO);
		this.target = null;
	}

}
