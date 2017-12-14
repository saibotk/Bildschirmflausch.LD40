using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryJob : Job {
	private NPC deliveryTarget;
	private Transform interactableSpawnpoint;
	private GameObject letterPrefab;
	private GameObject indicatorPrefab;
	private GameObject letterGO;
	private Jobmanager jobmanager;
	private Letter letter;

	public DeliveryJob(NPC deliveryTarget, Transform transform, GameObject prefab, Jobmanager manager, GameObject indicatorPrefab)
												: base ("Delivery", "Deliver the item!", 30f, 30, Resources.Load<Sprite>("letter")) {
		this.deliveryTarget = deliveryTarget;
		this.indicatorPrefab = indicatorPrefab;
		this.indicatorPrefab.GetComponent<SpriteRenderer>().color = GetJobColor();
		this.letter = new Letter(this);
		this.interactableSpawnpoint = transform;
		this.letterPrefab = prefab;
		this.jobmanager = manager;
	}

	override public void init() {
		this.deliveryTarget.SetAvailable (false);
		this.letterGO = GameObject.Instantiate(this.letterPrefab, this.interactableSpawnpoint.position, this.interactableSpawnpoint.rotation);
		this.letterGO.SetActive (true);
		this.letterGO.GetComponent<JobEntity>().SetJob(this);
		this.letterGO.GetComponent<JobEntity>().SetInteract(
			delegate (GameObject player) {
				bool added = player.GetComponent<PlayerController>().GetInventory().AddItem(this.letter);
				if (added) {
					GameObject.Destroy(this.letterGO);
				}
			}
		);

		GameObject indicator = GameObject.Instantiate (indicatorPrefab, deliveryTarget.transform);
		indicator.SetActive (true);
		this.deliveryTarget.SetIndicator(indicator);
		this.deliveryTarget.GetIndicator ().transform.localScale = new Vector3 (0.5f, 0.5f, 0);
		this.deliveryTarget.SetJob(this);
		this.deliveryTarget.SetInteract (
			delegate (GameObject player) {
				if(player.GetComponent<PlayerController>().GetInventory().leftHand != null) {
					if ( player.GetComponent<PlayerController>().GetInventory().leftHand is LetterBundle ) {
						if (((LetterBundle) player.GetComponent<PlayerController>().GetInventory().leftHand).Contains(this.letter)) {
							((LetterBundle) player.GetComponent<PlayerController>().GetInventory().leftHand).Remove(this.letter);
							if (((LetterBundle) player.GetComponent<PlayerController>().GetInventory().leftHand).letters.Count == 0)
								player.GetComponent<PlayerController>().GetInventory().leftHand = null;
							this.finishJob();
						}
					}
				}
			}
		);
	}

	public void finishJob() {
		this.jobmanager.FinishedJob(this);
	}

	override public void cleanup() {
		GameController.instance.GetPlayer().GetInventory().RemoveItem(this.letter);
		this.deliveryTarget.SetInteract (null);
		this.deliveryTarget.SetJob (null);
		this.deliveryTarget.SetAvailable (true);
		this.indicatorPrefab = null;
		GameObject.Destroy (this.deliveryTarget.GetIndicator());
		this.letterPrefab = null;
		this.letter = null;
		GameObject.Destroy(this.letterGO);
		this.deliveryTarget = null;
	}
}
