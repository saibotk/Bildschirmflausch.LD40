using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour, IAvailable {
	private bool available = true;

	[SerializeField]
	private int floor = 0;

	public void SetAvailable(bool b) {
		this.available = b;
	}

	public bool IsAvailable(int floor) {
		return available && GetFloor () <= floor;
	}

	public int GetFloor() {
		return floor;
	}
}
