using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private AudioSource gameoversound;
	private int gamestate = 0;
	private Jobmanager jobmanager;
	[SerializeField]
	private int jobTimeInterval = 40;
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
	private List<GameObject> availableQuestPlants;

	private float lastJob;

	private List<string> jobTypes = new List<string>();

	// Use this for initialization
	void Start () {
		// JOB TYPES INIT
		jobTypes.Add("delivery");
		jobTypes.Add ("watering");
		jobTypes.Add ("cleaning");

		lastJob = Time.realtimeSinceStartup;
		jobmanager = new Jobmanager (this);
		//coffeeNPCs = new List<GameObject>();
		availableQuestNPCs = new List<GameObject> (questNPCs);
		availableQuestPlants = new List<GameObject> (plants);
		addRandomJob ();

        gameoversound = GetComponent<AudioSource>();
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

	public void MakePlantAvailable(GameObject obj) {
		availableQuestPlants.Add(obj);
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
			if (plants.Count == 0 || availableQuestPlants.Count == 0)
				return; // Todo here should the code try to retrieve another job.
			int index = Random.Range (0, Mathf.Max (0, plants.Count - 3));
			int count = (plants.Count >= 3) ? 3 : plants.Count;
			jobmanager.AddJob (new WateringJob (plants.ConvertAll<JobInteraction> (x => x.GetComponent<JobInteraction> ()).GetRange (index, count), this.jobmanager));
			lastJob = Time.realtimeSinceStartup;
			availableQuestPlants.RemoveRange (index, count);
			Debug.Log ("Job: Waterings!");
			break;
		case "cleaning":
			
			break;
		}
	}

	private void GameOver() {
		if (gamestate != 1) {
			gamestate = 1;
            gameoversound.Play();
			Debug.Log ("----- GAME OVER. YOU CANT BEAT THE BOSS! ----------");
		}
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
