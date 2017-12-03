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
	private int playerFaceDirection; // 0 = left, 1 = right
	private Inventory inv;
	private List<Interactable> interactivesInRange = new List<Interactable>();
	private Animator animator;
	private LiftController liftController;
	private bool wallLeft = false;
	private bool wallRight = false;
	public bool inLift;

	// Use this for initialization
	void Start () {
		playerFaceDirection =  0;
		inv = new Inventory ();
		inv.coffeePot = new Item<float> ("Coffee Pot", 0);

		animator = GetComponentInChildren<Animator>();
		liftController = lift.GetComponent<LiftController> ();
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
		if (!inLift || liftController.CanPlayerMove()) {
			if (Input.GetKey ("d") && !wallRight) {
				gameObject.transform.Translate (new Vector3 (1 * movementSpeed * Time.deltaTime, 0));

				if (playerFaceDirection != 1) {
					playerFaceDirection = 1;
					playerTexture.transform.eulerAngles = new Vector3 (0, 180, 0);
				}
			}

			if (Input.GetKey ("a") && !wallLeft) {
				gameObject.transform.Translate (new Vector3 (-1 * movementSpeed * Time.deltaTime, 0));

				if (playerFaceDirection != 0) {
					playerFaceDirection = 0;
					playerTexture.transform.eulerAngles = new Vector3 (0, 0, 0);
				}
			}
		}
		if (inLift) {
			if (Input.GetKey ("w"))
				liftController.MoveUp ();
			if (Input.GetKey ("s"))
				liftController.MoveDown ();
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, liftController.PlayerHeight (), gameObject.transform.position.z);
		}
		animator.SetBool("Running", (Input.GetKey("a") ^ Input.GetKey("d")) && liftController.CanPlayerMove());
	}

	void OnTriggerEnter2D( Collider2D col ) {
		if (col.gameObject.GetComponent (typeof(Interactable)) != null) {
			AddInteractable (col.gameObject.GetComponent (typeof(Interactable)) as Interactable);
		} else {
			Debug.Log("No Interactable Script found!");
		}
			
	}

	void OnTriggerExit2D( Collider2D col ) {
		if (col.gameObject.GetComponent (typeof(Interactable)) != null) {
			RemoveInteractable (col.gameObject.GetComponent (typeof(Interactable)) as Interactable);
		} else {
			Debug.Log("No Interactable Script found!");
		}
	}

	/*public void BlockPlayerMovement()
	{
		wallRight = true;
		wallLeft = true;
	}

	public void AllowPlayerMovement()
	{
		wallRight = false;
		wallLeft = false;
	}*/

}
