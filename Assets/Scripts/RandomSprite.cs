using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour {

	bool initialized = false;

	void Start () {
	}

	void Update () {
		if (!initialized) {
			GetComponent<Animator> ().Play ((Random.Range(0, 1) > 0 ? "female" : "male")+ Random.Range (0, 11));
			initialized = true;
		}
	}
}