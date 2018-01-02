using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryJob : Job {
	private NPC deliveryTarget;
	private Transform interactableSpawnpoint;
	private GameObject letterPrefab;
	private GameObject indicatorPrefab;
	private GameObject letterGO;
	private Letter letter;

	public DeliveryJob(NPC deliveryTarget, Transform transform, GameObject prefab, GameObject indicatorPrefab)
												: base ("Delivery", "Deliver the item!", 30f, 30, Resources.Load<Sprite>("letter")) {
		this.deliveryTarget = deliveryTarget;
		this.indicatorPrefab = indicatorPrefab;
		this.indicatorPrefab.GetComponent<SpriteRenderer>().color = jobColor = Random.ColorHSV (50f/360f, 60f/360f, 0.3f, 0.6f, 0.7f, 0.9f);
		this.letter = new Letter(this);
		this.interactableSpawnpoint = transform;
		this.letterPrefab = prefab;
	}

	override public void init() {
		this.deliveryTarget.SetAvailable (false);
		this.letterGO = GameObject.Instantiate(this.letterPrefab, this.interactableSpawnpoint.position, this.interactableSpawnpoint.rotation);
		this.letterGO.SetActive (true);
		this.letterGO.GetComponent<JobEntity>().SetJob(this);
		this.letterGO.GetComponent<JobEntity>().SetInteract(
			delegate (GameObject player) {
				LetterBundle lb = player.GetComponent<PlayerController>().GetInventory().GetItem<LetterBundle>();
				bool added = false;
				if(lb == null) {
					lb = new LetterBundle();
					lb.AddItem(this.letter);
					added = player.GetComponent<PlayerController>().GetInventory().AddItem(lb);
				} else {
					added = lb.AddItem(this.letter);
				}
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
				Inventory inv = player.GetComponent<PlayerController>().GetInventory();
				if(inv.GetItem("leftHand") != null) {
					if ( inv.GetItem("leftHand").GetType() == typeof(LetterBundle) ) {
						LetterBundle lb = (LetterBundle) inv.GetItem("leftHand");
						if (lb.IsInInventory(this.letter)) {
							this.finishJob();
						}
					}
				}
			}
		);
	}

	public void finishJob() {
		GameController.instance.GetJobManager().FinishedJob(this);
	}

	override public void cleanup() {
		Inventory inv = GameController.instance.GetPlayer ().GetInventory ();
		LetterBundle lb = inv.GetItem<LetterBundle> ();
		if (lb != null) {
			if (lb.GetSize () > 1) {
				lb.RemoveItem (this.letter);
			} else {
				inv.RemoveItem (lb);
			}
		}
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
