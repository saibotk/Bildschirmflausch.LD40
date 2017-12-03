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
	private List<GameObject> plants;

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
		// JOB TYPES INIT
		jobTypes.Add("delivery");
		jobTypes.Add ("watering");

		lastJob = Time.realtimeSinceStartup;
		jobmanager = new Jobmanager (this);
		//coffeeNPCs = new List<GameObject>();
		availableQuestNPCs = new List<GameObject>(questNPCs);
		addRandomJob ();
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
			if (availableQuestNPCs.Count == 0)
				return; // Todo here should the code try to retrieve another job.
			int usedNpc = Random.Range (0, availableQuestNPCs.Count);
			jobmanager.AddJob (new DeliveryJob (availableQuestNPCs [usedNpc].GetComponent<JobInteraction> (), letterSpawnpoint, letterPrefab, this.jobmanager));
			lastJob = Time.realtimeSinceStartup;
			Debug.Log ("Job: Delivery!");
			availableQuestNPCs.RemoveAt(usedNpc);
			break;
		case "watering":
			if (plants.Count == 0) return; // Todo here should the code try to retrieve another job.
			jobmanager.AddJob (new WateringJob ( plants.ConvertAll<JobInteraction>(x => x.GetComponent<JobInteraction>()).GetRange(Random.Range(0, Mathf.Max(0, plants.Count - 3)), (plants.Count >= 3 ) ? 3 : plants.Count), this.jobmanager) );
			lastJob = Time.realtimeSinceStartup;
			Debug.Log ("Job: Waterings!");
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
