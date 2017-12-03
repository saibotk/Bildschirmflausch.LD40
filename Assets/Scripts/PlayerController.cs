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
	private List<Interactable> interactivesInRange = new List<Interactable>();
	private Animator animator;
	private bool wallLeft = false;
	private bool wallRight = false;

	// Use this for initialization
	void Start () {
		playerFaceDirection =  0;
		inv = new Inventory ();
		inv.coffeePot = new Item<float> ("Coffee Pot", 0);

		animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		PlayerMovement ();
		PlayerInteract ();
	}

	public Inventory GetInventory() {
		return inv;
	}

	private void PlayerInteract() {
		if (Input.GetKey ("e")) {
			if (interactivesInRange.Count != 0) {
				interactivesInRange [0].Interact (gameObject);
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
		if (Input.GetKey("d") && !wallRight) {
			gameObject.transform.Translate( new Vector3(1* movementSpeed * Time.deltaTime, 0));

			if (playerFaceDirection != 1) {
				playerFaceDirection = 1;
				playerTexture.transform.eulerAngles = new Vector3 (0, 180, 0);
			}
		}

		if (Input.GetKey("a") && !wallLeft) {
			gameObject.transform.Translate( new Vector3(-1* movementSpeed * Time.deltaTime, 0));

			if (playerFaceDirection != 0) {
				playerFaceDirection = 0;
				playerTexture.transform.eulerAngles = new Vector3 (0, 0, 0);
			}
		}

		animator.SetBool("Running", Input.GetKey("a") || Input.GetKey("d"));
	}

	void OnTriggerEnter2D( Collider2D col ) {
		if (col.gameObject.GetComponent (typeof(Interactable)) != null) {
			AddInteractable (col.gameObject.GetComponent (typeof(Interactable)) as Interactable);
		} else {
			Debug.Log("No Interactable Script found!");
		}

		if (col.CompareTag ("Wall")) {
			if (Input.GetKey ("d"))
				wallRight = true;
			if (Input.GetKey ("a"))
				wallLeft = true;
		}
			
	}

	void OnTriggerExit2D( Collider2D col ) {
		if (col.gameObject.GetComponent (typeof(Interactable)) != null) {
			RemoveInteractable (col.gameObject.GetComponent (typeof(Interactable)) as Interactable);
		} else {
			Debug.Log("No Interactable Script found!");
		}

		if (col.CompareTag ("Wall")) {
			if (Input.GetKey ("a"))
				wallRight = false;
			if (Input.GetKey ("d"))
				wallLeft = false;
		}
	}
}
