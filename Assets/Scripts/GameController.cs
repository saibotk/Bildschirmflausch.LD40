using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	private Jobmanager jobmanager;
	[SerializeField]
	private int jobTimeInterval = 20;
	[SerializeField]
	private GameObject player;
	[SerializeField]
	private List<GameObject> coffeeNPCs;
	[SerializeField]
	private GameObject letterPrefab;
	[SerializeField]
	private Transform letterSpawnpoint;

	[SerializeField]
	private List<GameObject> questNPCs;
	private List<GameObject> availableQuestNPCs;

	private float lastJob;

	private List<string> jobTypes = new List<string>();

	// Use this for initialization
	void Start () {
		jobTypes.Add("delivery");
		lastJob = Time.realtimeSinceStartup;
		jobmanager = new Jobmanager (this);
		//coffeeNPCs = new List<GameObject>();
		availableQuestNPCs = new List<GameObject>(questNPCs);
	}

	public PlayerController GetPlayer() {
		return player.GetComponent<PlayerController>();
	}

	// Update is called once per frame
	void Update () {
		if (Time.realtimeSinceStartup >= lastJob + jobTimeInterval) {
		 	addRandomJob ();
		}

		jobmanager.checkJobTimes ();
		CheckCoffeeNPCs ();
	}

	public void MakeNPCAvailable(GameObject obj) {
		availableQuestNPCs.Add(obj);
	}

	public void addRandomJob () {
		switch (jobTypes[Random.Range (0, jobTypes.Count)]) {
			case "delivery":
				if (availableQuestNPCs.Count == 0) return;
				int usedNpc = Random.Range (0, availableQuestNPCs.Count);
				jobmanager.AddJob (new DeliveryJob (availableQuestNPCs[usedNpc].GetComponent<JobInteraction>(), letterSpawnpoint, letterPrefab, this.jobmanager));
				availableQuestNPCs.RemoveAt(usedNpc);
				break;
		}
	}

	private void GameOver() {
		//Debug.Log ("GAME OVER");
	}

	public void CheckCoffeeNPCs() {
		int emptyCofeeCounter = 0;
		foreach (GameObject NPC in coffeeNPCs) {
			if (NPC.GetComponent<WorkersCoffeeNeeds> ().GetCoffeeTimer () == 0) {
				emptyCofeeCounter++;
			}
		}
		if (emptyCofeeCounter >= 3) {
			GameOver ();
		}
	}
}
