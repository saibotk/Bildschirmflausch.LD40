using System;
using UnityEngine;

public class LiftController : MonoBehaviour {
	[SerializeField]
	private readonly float levelHeight;
	[SerializeField]
	private readonly float speed;
	[SerializeField]
	private readonly int floorCount;
	private float height = 0;
	private int nextFloor;

	public LiftController () {
		
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (height < nextFloor) { // move up
			height += speed;
			if (height >= nextFloor)
				height = nextFloor;
		} else if (height > nextFloor) {
			height -= speed;
			if (height <= nextFloor)
				height = nextFloor;
		}
	}

	public void MoveUp() {
		nextFloor--;
		if (nextFloor < 0)
			nextFloor = 0;
	}

	public void MoveDown() {
		nextFloor++;
		if (nextFloor > floorCount)
			nextFloor = floorCount;
	}

	public bool CanPlayerMove() {
		return nextFloor == height;
	}

	public float PlayerHeight() {
		return height * levelheigt;
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.CompareTag ("Player") && !canPlayerMove ()) {
			GameObject player = coll.GetComponent<GameObject> ();
			player.GetComponent<PlayerController> ().BlockPlayerMovement ();
		}

		if (coll.CompareTag ("Player") && canPlayerMove ()) {
			GameObject player = coll.GetComponent<GameObject> ();
			player.GetComponent<PlayerController> ().BlockPlayerMovement ();
		}
	}
}