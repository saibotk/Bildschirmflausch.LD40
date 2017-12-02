using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField]
	private int movementSpeed = 2;
	[SerializeField]
	private GameObject playerTexture;
	private int playerFaceDirection; // 0 = left, 1 = right
	private Inventory inv;
	private List<Interactable> interactivesInRange;

	// Use this for initialization
	void Start () {
		int playerFaceDirection =  0;
		inv = new Inventory ();
		inv.coffeePot = new Item<float> ("Coffee Pot", 0);
	}
	
	// Update is called once per frame
	void Update () {
		PlayerMovement ();
		PlayerInteract ();
	}

	public void GetInventory() {
		return inv;
	}

	private void PlayerInteract() {
		if (Input.GetKey ("e")) {
			if (interactivesInRange [0] != null) {
				interactivesInRange [0].Interact ();
			}
		}
	}

	public void AddInteractable( Interactable inter ) {
		interactivesInRange.Add (inter);
	}

	public void RemoveInteractable( Interactable inter ) {
		interactivesInRange.Remove (inter);
	}

	private void PlayerMovement() {
		if (Input.GetKey("d")) {
			gameObject.transform.Translate( new Vector3(1* movementSpeed * Time.deltaTime, 0));

			if (playerFaceDirection != 1) {
				playerFaceDirection = 1;
				playerTexture.transform.eulerAngles = new Vector3 (0, 180, 0);
			}

		}

		if (Input.GetKey("a")) {
			gameObject.transform.Translate( new Vector3(-1* movementSpeed * Time.deltaTime, 0));

			if (playerFaceDirection != 0) {
				playerFaceDirection = 0;
				playerTexture.transform.eulerAngles = new Vector3 (0, 0, 0);
			}
		}
	}

}
