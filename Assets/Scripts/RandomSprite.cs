using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour {

	public bool isManager = false;
	private bool initialized = false;

	void Start () {
	}

	void Update () {
		if (!initialized) {
			GetComponent<Animator> ().Play (
				isManager ? ("managerIdle" + Random.Range(0, 3))
						  : (Random.Range(0, 2) > 0 ? "female" : "male") + Random.Range (0, 12));
			initialized = true;
		}
	}
}