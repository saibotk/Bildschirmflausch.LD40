using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour {

	void Start () {
		Sprite[] textures = Resources.LoadAll<Sprite>("");
		Sprite texture = textures[(Random.Range(0, textures.Length)/3)*3];
		GetComponent<SpriteRenderer> ().sprite = texture;
	}

	void Update () {
		
	}
}