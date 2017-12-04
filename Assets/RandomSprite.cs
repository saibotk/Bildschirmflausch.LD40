using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour {

	void Start () {
		Sprite[] textures = Resources.LoadAll<Sprite>("");
		Sprite texture = textures[Random.Range(0, textures.Length)];
		GetComponent<SpriteRenderer> ().sprite = texture;
	}

	void Update () {
		
	}
}