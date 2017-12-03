using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField]
	private int movementSpeed = 2;
	[SerializeField]
	private GameObject playerTexture;
	[SerializeField]
	private GameObject lift;
	private int movementState; // -1 = left, 0 = idle, 1 = right
	private Inventory inv;
	private List<Interactable> interactivesInRange = new List<Interactable>();
	private Animator animator;
	private LiftController liftController;
	public bool inLift = false;

	// Use this for initialization
	void Start () {
		movementState =  0;
		inv = new Inventory ();
		inv.coffeePot = new CoffeePot (0);

		animator = GetComponentInChildren<Animator>();
		liftController = lift.GetComponent<LiftController> (); // could be null!
	}
	
	// Update is called once per frame
	void Update () {
		PlayerMovement ();
		PlayerInteract ();
		//Debug.Log (inLift);
	}

	public Inventory GetInventory() {
		return inv;
	}

	private void PlayerInteract() {
		if (Input.GetKeyDown ("e")) {
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
		if (liftController == null || !inLift || liftController.CanPlayerMove()) {
			if (Input.GetKey ("d")) {
				gameObject.transform.Translate (new Vector3 (1 * movementSpeed * Time.deltaTime, 0));
				if (movementState != 1) {
					movementState = 1;
					playerTexture.transform.eulerAngles = new Vector3 (0, 180, 0);
				}
			}

			if (Input.GetKey ("a")) {
				gameObject.transform.Translate (new Vector3 (-1 * movementSpeed * Time.deltaTime, 0));
				if (movementState != -1) {
					movementState = -1;
					playerTexture.transform.eulerAngles = new Vector3 (0, 0, 0);
				}
			}

			if (!(Input.GetKey ("a") ^ Input.GetKey ("d")))
				movementState = 0;
		}
		if (liftController != null && inLift) {
			if (Input.GetKey ("w") && !Input.GetKey ("s"))
				liftController.MoveUp ();
			if (Input.GetKey ("s") && !Input.GetKey ("w"))
				liftController.MoveDown ();
			if (!liftController.CanPlayerMove ())
				movementState = 0;
		}
		animator.SetBool("Running", movementState != 0);
	}

	void OnTriggerEnter2D( Collider2D col ) {
		if (col.gameObject.GetComponent (typeof(Interactable)) != null)
			AddInteractable (col.gameObject.GetComponent (typeof(Interactable)) as Interactable);

		if (col.CompareTag ("Lift")) {
			if (col.isTrigger) {
				inLift = true;
				transform.parent = lift.transform;	
			}
		}
	}

	void OnTriggerExit2D( Collider2D col ) {
		if (col.gameObject.GetComponent (typeof(Interactable)) != null)
			RemoveInteractable (col.gameObject.GetComponent (typeof(Interactable)) as Interactable);

		if (col.CompareTag ("Lift")) {
			if (col.isTrigger) {
				inLift = false;
				transform.parent = null;
			}
		}
	}
}
