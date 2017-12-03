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
		Debug.Log (nextFloor + " " + height);
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
		nextFloor++;
		if (nextFloor > floorCount)
			nextFloor = floorCount;
		Debug.Log ("up") ;
	}

			public void MoveDown() {
				nextFloor--;
				if (nextFloor < 0)
					nextFloor = 0;
				Debug.Log ("down"+ nextFloor);
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