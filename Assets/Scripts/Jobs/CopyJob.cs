using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is actually a print job
public class CopyJob : Job {
	private NPC target;
	private Transform interactableSpawnpoint;
	private GameObject documentPrefab;
	private GameObject documentGO;
	private Jobmanager jobmanager;
	private Document document;

	public CopyJob(NPC boss, IInteractable copyMashine, GameObject prefab, Jobmanager manager) : base ("Copy", "Copy that document!", 30f, 70, (Resources.Load("letter") as Sprite)) {
		this.target = boss;
		this.target.SetAvailable (false);
		this.document = new Document(this);
		//this.interactableSpawnpoint = transform;
		//this.letterPrefab = prefab;
		this.jobmanager = manager;
		init ();
	}

	override public void init() {
		this.documentGO = GameObject.Instantiate(this.documentPrefab, this.interactableSpawnpoint.position, this.interactableSpawnpoint.rotation);
		this.documentGO.GetComponent<JobEntity>().SetJob(this);
		this.documentGO.GetComponent<JobEntity>().SetInteract(
			delegate (GameObject player) {
				bool added = player.GetComponent<PlayerController>().GetInventory().AddItem(this.document);
				if (added) {
					//GameObject.Destroy(this.letterGO);
				}
			}
		);

		this.target.SetJob(this);
		this.target.SetInteract (
			delegate (GameObject player) {
				if(player.GetComponent<PlayerController>().GetInventory().GetItem("leftHand") != null) {
					if ( player.GetComponent<PlayerController>().GetInventory().GetItem("leftHand") is Letter ) {
						if (((Letter) player.GetComponent<PlayerController>().GetInventory().GetItem("leftHand")).job == this) {
							player.GetComponent<PlayerController>().GetInventory().RemoveItemInSlot("leftHand");
							this.finishJob();
						}
					}
				}
			}
		);
	}

	public void finishJob() {
		Debug.Log("QUEST COMPLETED! U GENIUS");
		this.jobmanager.FinishedJob(this);
	}

	override public void cleanup() {
		GameController.instance.GetPlayer().GetInventory().RemoveItem(this.document);
		this.target.SetInteract (null);
		this.target.SetJob (null);
		this.target.SetAvailable (true);
		this.documentPrefab = null;
		this.document = null;
		GameObject.Destroy(this.documentGO);
		this.target = null;
	}

}
