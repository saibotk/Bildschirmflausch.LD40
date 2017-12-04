using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	//private BoxCollider2D ccollider;

	void Start () {
		offset = transform.position;// - player.transform.position;
		//ccollider = GetComponent<BoxCollider2D>();
	}

	void LateUpdate () {
		//Bounds levelBounds = ccollider.bounds;

		var mapX = 100.0;
		var mapY = 100.0;

		var vertExtent = Camera.main.orthographicSize;    
		var horzExtent = vertExtent * Screen.width / Screen.height;
		// Calculations assume map is position at the origin
		var minX = horzExtent - mapX / 2.0;
		var maxX = mapX / 2.0 - horzExtent;
		var minY = vertExtent - mapY / 2.0;
		var maxY = mapY / 2.0 - vertExtent;

		var playerPos = player.transform.position + offset;
		var diff = playerPos - transform.position;
		diff.Scale (new Vector3 (0.15f, 0.15f, 0));
		transform.position += diff;
	}
}