using System;
using UnityEngine;

public class LiftController : MonoBehaviour {
	[SerializeField]
	private float levelHeight;
	[SerializeField]
	private float speed;
	[SerializeField]
	private int floorCount;
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
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, height * levelHeight, gameObject.transform.position.z);
	}

	public void MoveUp() {
		if (nextFloor - height < 0.1)
			nextFloor++;
		if (nextFloor > floorCount)
			nextFloor = floorCount;
	}

	public void MoveDown() {
		if (height - nextFloor < 0.1)
			nextFloor--;
		if (nextFloor < 0)
			nextFloor = 0;
	}

	public bool CanPlayerMove() {
		return nextFloor == height;
	}

	public float PlayerHeight() {
		return height * levelHeight;
	}

	/*void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.CompareTag ("Player") && !CanPlayerMove ()) {
			GameObject player = coll.GetComponent<GameObject> ();
			player.GetComponent<PlayerController> ().BlockPlayerMovement ();
		}

		if (coll.CompareTag ("Player") && CanPlayerMove ()) {
			GameObject player = coll.GetComponent<GameObject> ();
			player.GetComponent<PlayerController> ().BlockPlayerMovement ();
		}
	}*/
}