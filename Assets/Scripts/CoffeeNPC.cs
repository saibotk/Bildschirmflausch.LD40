using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeNPC : NPC {
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
		base.Init ();
	}
	
	// Update is called once per frame
	void Update () {
		if (coffeeTimer >= Time.deltaTime) {
			if (GetComponent<NPC> ().IsAvailable (GameController.instance.GetFloor ()))
				coffeeTimer -= Time.deltaTime;
		} else {
			coffeeTimer = 0;
		}
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

	public override bool CanInteract( GameObject player ) {
		CoffeePot pcpot = player.GetComponent<PlayerController> ().GetInventory ().coffeePot;
		return pcpot != null && pcpot.getFillLevel() != 0f && this.coffeeTimer <= this.coffeeTimer_init * 0.95;
	}

	public override void Interact( GameObject player ) {
		if (player.GetComponent<PlayerController> ().GetInventory ().coffeePot == null)
			return;
		float neededCoffee = 1 - (coffeeTimer / coffeeTimer_init);
		float coffeeNotInCup = player.GetComponent<PlayerController> ().GetInventory ().coffeePot.drain (neededCoffee);
		coffeeTimer = (coffeeNotInCup == 0) ? coffeeTimer_init : coffeeTimer + (neededCoffee - coffeeNotInCup) * coffeeTimer_init;

		//Debug.Log (player.GetComponent<PlayerController> ().GetInventory ().coffeePot.getFillLevel());
		//Debug.Log (coffeeTimer);
	}

	public float GetCoffeeTimer() {
		return coffeeTimer;
	}
}
