using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour, IAvailable {
	private bool available = true;

	public void setAvailable (bool b){
		this.available = b;
	}

	public bool isAvailable() {
		return this.available;
	}
}

