using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField]
	private int movementSpeed = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey("d")) {
			gameObject.transform.Translate( new Vector3(1* movementSpeed * Time.deltaTime, 0));
		}

		if (Input.GetKey("a")) {
			gameObject.transform.Translate( new Vector3(-1* movementSpeed * Time.deltaTime, 0));
		}
	}
}
