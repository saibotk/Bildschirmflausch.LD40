using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkersCoffeeNeeds : MonoBehaviour, Interactable {
	[SerializeField]
	float coffeeTimer_init;
	float coffeeTimer;

	[SerializeField]
	GameObject coffeeMeter;
	[SerializeField]
	Texture2D coffeeMeter_spriteSheet;

	// Use this for initialization
	void Start () {
		coffeeTimer = coffeeTimer_init;

	}
	
	// Update is called once per frame
	void Update () {
		coffeeTimer -= Time.deltaTime;

		SpriteRenderer render = coffeeMeter.GetComponent<SpriteRenderer>();

		if (coffeeTimer < coffeeTimer_init / 6 * 1)
			render.sprite = Sprite.Create (coffeeMeter_spriteSheet, new Rect (15, 0, 3, 7), new Vector2 (0.5f, 0.5f));
		else if (coffeeTimer < coffeeTimer_init / 6 * 2)
			render.sprite = Sprite.Create (coffeeMeter_spriteSheet, new Rect (12, 0, 3, 7), new Vector2 (0.5f, 0.5f));
		else if (coffeeTimer < coffeeTimer_init / 6 * 3)
			render.sprite = Sprite.Create (coffeeMeter_spriteSheet, new Rect (9, 0, 3, 7), new Vector2 (0.5f, 0.5f));
		else if (coffeeTimer < coffeeTimer_init / 6 * 4)
			render.sprite = Sprite.Create (coffeeMeter_spriteSheet, new Rect (6, 0, 3, 7), new Vector2 (0.5f, 0.5f));
		else if (coffeeTimer < coffeeTimer_init / 6 * 5)
			render.sprite = Sprite.Create (coffeeMeter_spriteSheet, new Rect (3, 0, 3, 7), new Vector2 (0.5f, 0.5f));
		else
			render.sprite = Sprite.Create (coffeeMeter_spriteSheet, new Rect (0, 0, 3, 7), new Vector2 (0.5f, 0.5f));
	}

	public void Interact( GameObject player ) {
		float neededCoffee = 1 - (coffeeTimer / coffeeTimer_init);
	    float coffeeInPot = player.GetComponent<PlayerController> ().GetInventory ().coffeePot.value;

		if (coffeeInPot >= neededCoffee){
			player.GetComponent<PlayerController> ().GetInventory ().coffeePot.value = coffeeInPot - neededCoffee;
			coffeeTimer = coffeeTimer_init;
		}else if (coffeeInPot < neededCoffee){
			player.GetComponent<PlayerController> ().GetInventory ().coffeePot.value = 0;
			coffeeTimer += (coffeeInPot / neededCoffee) * neededCoffee * coffeeTimer_init;
		}

		Debug.Log (player.GetComponent<PlayerController> ().GetInventory ().coffeePot.value);
		Debug.Log (coffeeTimer);
	}
}
