using System;
using UnityEngine;

public class LiftController : MonoBehaviour {
	[SerializeField]
	private float levelHeight;
	[SerializeField]
	private float speed;
	[SerializeField]
	private int floorCount;
	[SerializeField]
	public GameObject player;
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
			if (height >= nextFloor) {
				height = nextFloor;
				player.GetComponent<AudioControl> ().Pling ();
			}
		} else if (height > nextFloor) {
			height -= speed;
			if (height <= nextFloor) {
				height = nextFloor;
				player.GetComponent<AudioControl> ().Pling ();
			}
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
}