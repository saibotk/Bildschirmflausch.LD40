using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField]
	private int movementSpeed = 2;
	[SerializeField]
	private GameObject playerTexture;
	private int playerFaceDirection; // 0 = left, 1 = right

	// Use this for initialization
	void Start () {
		playerFaceDirection =  0;
	}
	
	// Update is called once per frame
	void Update () {
		PlayerMovement ();
	}

	private void PlayerMovement() {
		if (Input.GetKey("d")) {
			gameObject.transform.Translate( new Vector3(1* movementSpeed * Time.deltaTime, 0));

			if (playerFaceDirection != 0) {
				playerFaceDirection = 0;
				playerTexture.transform.eulerAngles = new Vector3 (0, 180, 0);
			}

		}

		if (Input.GetKey("a")) {
			gameObject.transform.Translate( new Vector3(-1* movementSpeed * Time.deltaTime, 0));

			if (playerFaceDirection != 1) {
				playerFaceDirection = 1;
				playerTexture.transform.eulerAngles = new Vector3 (0, 0, 0);
			}
		}
	}

}
