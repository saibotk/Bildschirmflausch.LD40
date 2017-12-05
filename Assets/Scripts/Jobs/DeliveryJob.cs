using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryJob : Job {
	private NPC target;
	private Transform interactableSpawnpoint;
	private GameObject letterPrefab;
	private GameObject indicatorPrefab;
	private GameObject letterGO;
	private Jobmanager jobmanager;
	private Letter letter;

	public DeliveryJob(NPC target, Transform transform, GameObject prefab, Jobmanager manager, GameObject indicatorPrefab) : base ("Delivery", "Deliver the item!", 30f, 30, Resources.Load<Sprite>("letter")) {
		this.target = target;
		this.indicatorPrefab = indicatorPrefab;
		this.indicatorPrefab.GetComponent<SpriteRenderer>().color = GetJobColor();
		this.target.SetAvailable (false);
		this.letter = new Letter(this);
		this.interactableSpawnpoint = transform;
		this.letterPrefab = prefab;
		this.jobmanager = manager;
		init ();
	}

	override public void init() {
		this.letterGO = GameObject.Instantiate(this.letterPrefab, this.interactableSpawnpoint.position, this.interactableSpawnpoint.rotation);
		this.letterGO.GetComponent<JobEntitiy>().SetJob(this);
		this.letterGO.GetComponent<JobEntitiy>().SetInteract(
			delegate (GameObject player) {
				bool added = player.GetComponent<PlayerController>().GetInventory().AddItem(this.letter);
				if (added) {
					GameObject.Destroy(this.letterGO);
				}
			}
		);

		this.target.SetIndicator(GameObject.Instantiate (indicatorPrefab, this.target.transform));
		this.target.GetIndicator ().transform.localScale = new Vector3 (0.5f, 0.5f, 0);
		this.target.SetJob(this);
		this.target.SetInteract (
			delegate (GameObject player) {
				if(player.GetComponent<PlayerController>().GetInventory().leftHand != null) {
					if ( player.GetComponent<PlayerController>().GetInventory().leftHand is LetterBundle ) {
						if (((LetterBundle) player.GetComponent<PlayerController>().GetInventory().leftHand).Contains(this.letter)) {
							((LetterBundle) player.GetComponent<PlayerController>().GetInventory().leftHand).Remove(this.letter);
							if (((LetterBundle) player.GetComponent<PlayerController>().GetInventory().leftHand).letters.Count == 0) player.GetComponent<PlayerController>().GetInventory().leftHand = null;
							this.finishJob();
						}
					}
				}
			}
		);
	}

	public void finishJob() {
		Debug.Log("QUEST COMPLETED! U GENIUS");
		this.jobmanager.finishedJob(this);
	}

	override public void cleanup() {
		this.jobmanager.GetGameController().GetPlayer().GetInventory().RemoveItem(this.letter);
		this.target.SetInteract (null);
		this.target.SetJob (null);
		this.target.SetAvailable (true);
		this.indicatorPrefab = null;
		GameObject.Destroy (this.target.GetIndicator());
		this.letterPrefab = null;
		this.letter = null;
		GameObject.Destroy(this.letterGO);
		this.target = null;
	}

}
