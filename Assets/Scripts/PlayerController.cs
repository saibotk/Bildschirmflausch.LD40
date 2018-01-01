using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
	[SerializeField]
	private int movementSpeed = 2;
	[SerializeField]
	private GameObject graphics;
	[SerializeField]
	private GameObject playerTexture;
	[SerializeField]
	private GameObject cornerTexture;
	[SerializeField]
	private GameObject rightHandTexture;
	[SerializeField]
	private Texture2D coffeePotLevels;
	[SerializeField]
	private Texture2D coffeePotUILevels;
	[SerializeField]
	private GameObject lift;
	private int movementState; // -1 = left, 0 = idle, 1 = right
	private PlayerInventory inv;
	private List<GameObject> interactivesInRange = new List<GameObject>();
	private Animator animator;
	private LiftController liftController;
	public bool inLift = false;

	// Use this for initialization
	void Start () {
		movementState =  0;
		inv = new PlayerInventory ();
		inv.coffeePot = new CoffeePot (6);

		animator = GetComponentInChildren<Animator>();
		liftController = lift.GetComponent<LiftController> (); // could be null!
		SetPlayerCoffeeTextures ();
	}
	
	// Update is called once per frame
	void Update () {
		PlayerMovement ();
		PlayerInteract ();
	}

	public PlayerInventory GetInventory() {
		return inv;
	}

	private void PlayerInteract() {
		if (CrossPlatformInputManager.GetButtonDown ("Cancel")) {
			if (GameController.instance.GetState () == -1) {
				GameController.instance.UnPauseGame ();
			} else {
				GameController.instance.PauseGame ();
			}
		}
		if (CrossPlatformInputManager.GetButtonDown("Use")) {
			if (interactivesInRange.Count != 0) {
				GameObject iaclose = null;
				interactivesInRange = interactivesInRange.FindAll (x => (x.GetComponent(typeof(IInteractable)) as IInteractable).CanInteract(this.gameObject));
				foreach (GameObject ia in interactivesInRange) {
					if (iaclose == null || Vector2.Distance (gameObject.transform.position, ia.transform.position) < Vector2.Distance (gameObject.transform.position, iaclose.transform.position))
						iaclose = ia;
				}
				if (iaclose != null) {
					(iaclose.GetComponentInChildren (typeof(IInteractable)) as IInteractable).Interact (gameObject);
				}
				SetPlayerCoffeeTextures();
			}
		}
		if (CrossPlatformInputManager.GetButtonDown("Swap")) {
			inv.Swap ();
		}
	}

	private void SetPlayerCoffeeTextures() {
		if (inv.coffeePot == null) {
			animator.SetBool ("Coffee", false);
			GameUI.instance.SetCoffeePotEnabled (false);
			return;
		} else {
			animator.SetBool ("Coffee", true);
			GameUI.instance.SetCoffeePotEnabled (true);
		}

		float perc = inv.coffeePot.getFillLevel () / inv.coffeePot.getMaxFillLevel ();

		if (perc > 0.86) {
			GameUI.instance.SetCoffeePotFillImage(Sprite.Create (coffeePotUILevels, new Rect (0, 0, 12, 12), new Vector2 (6, 6)));
			rightHandTexture.GetComponent<SpriteRenderer> ().sprite = Sprite.Create (coffeePotLevels, new Rect(60, 0, 10, 9), new Vector2(0.5f, 0.5f));
		} else if (perc > 0.72) {
			GameUI.instance.SetCoffeePotFillImage(Sprite.Create (coffeePotUILevels, new Rect (12, 0, 12, 12), new Vector2 (6, 6)));
			rightHandTexture.GetComponent<SpriteRenderer> ().sprite = Sprite.Create (coffeePotLevels, new Rect(50, 0, 10, 9), new Vector2(0.5f, 0.5f));
		} else if (perc > 0.58) {
			GameUI.instance.SetCoffeePotFillImage(Sprite.Create (coffeePotUILevels, new Rect (24, 0, 12, 12), new Vector2 (6, 6)));
			rightHandTexture.GetComponent<SpriteRenderer> ().sprite = Sprite.Create (coffeePotLevels, new Rect(40, 0, 10, 9), new Vector2(0.5f, 0.5f));
		} else if (perc > 0.44) {
			GameUI.instance.SetCoffeePotFillImage(Sprite.Create (coffeePotUILevels, new Rect (36, 0, 12, 12), new Vector2 (6, 6)));
			rightHandTexture.GetComponent<SpriteRenderer> ().sprite = Sprite.Create (coffeePotLevels, new Rect(30, 0, 10, 9), new Vector2(0.5f, 0.5f));
		} else if (perc > 0.30) {
			GameUI.instance.SetCoffeePotFillImage(Sprite.Create (coffeePotUILevels, new Rect (48, 0, 12, 12), new Vector2 (6, 6)));
			rightHandTexture.GetComponent<SpriteRenderer> ().sprite = Sprite.Create (coffeePotLevels, new Rect(20, 0, 10, 9), new Vector2(0.5f, 0.5f));
		} else if (perc > 0.16) {
			GameUI.instance.SetCoffeePotFillImage(Sprite.Create (coffeePotUILevels, new Rect (60, 0, 12, 12), new Vector2 (6, 6)));
			rightHandTexture.GetComponent<SpriteRenderer> ().sprite = Sprite.Create (coffeePotLevels, new Rect(10, 0, 10, 9), new Vector2(0.5f, 0.5f));
		} else {
			GameUI.instance.SetCoffeePotFillImage(Sprite.Create (coffeePotUILevels, new Rect (72, 0, 12, 12), new Vector2 (6, 6)));
			rightHandTexture.GetComponent<SpriteRenderer> ().sprite = Sprite.Create (coffeePotLevels, new Rect(0, 0, 10, 9), new Vector2(0.5f, 0.5f));
		}
	}

	public void AddInteractable( GameObject inter ) {
		interactivesInRange.Add (inter);
	}

	public void RemoveInteractable( GameObject inter ) {
		interactivesInRange.Remove (inter);
	}

	private void PlayerMovement() {
		if (liftController == null || !inLift || liftController.CanPlayerMove()) {
			//Debug.Log (CrossPlatformInputManager.GetAxis ("Horizontal"));
			if ( CrossPlatformInputManager.GetAxisRaw("Horizontal") == 0 ) // TODO Fix player moving when both keys pressed
                movementState = 0;
			else if (CrossPlatformInputManager.GetAxisRaw("Horizontal") > 0) {
				gameObject.transform.Translate (new Vector3 (1 * movementSpeed * Time.deltaTime, 0));
				if (movementState != 1) {
					movementState = 1;
					graphics.transform.eulerAngles = new Vector3 (0, 180, 0);
				}
			}

			else if (CrossPlatformInputManager.GetAxisRaw("Horizontal") < 0) {
				gameObject.transform.Translate (new Vector3 (-1 * movementSpeed * Time.deltaTime, 0));
				if (movementState != -1) {
					movementState = -1;
					graphics.transform.eulerAngles = new Vector3 (0, 0, 0);
				}
			}
		}
		if (liftController != null && inLift) {
			if (CrossPlatformInputManager.GetAxisRaw("Vertical") > 0.5)
				liftController.MoveUp ();
			if (CrossPlatformInputManager.GetAxisRaw("Vertical") < -0.5)
				liftController.MoveDown ();
			if (!liftController.CanPlayerMove ())
				movementState = 0;
		}
		animator.SetBool("Running", movementState != 0);
	}

	void OnTriggerEnter2D( Collider2D col ) {
		if (col.gameObject.GetComponent (typeof(IInteractable)) != null) {
			AddInteractable (col.gameObject);
		}
		if (col.CompareTag ("Lift")) {
			if (col.isTrigger) {
				inLift = true;
				transform.parent = lift.transform;	
			}
		}
	}

	void OnTriggerExit2D( Collider2D col ) {
		if (col.gameObject.GetComponent (typeof(IInteractable)) != null)
			RemoveInteractable (col.gameObject);

		if (col.CompareTag ("Lift")) {
			if (col.isTrigger) {
				inLift = false;
				transform.parent = null;
			}
		}
	}
}
