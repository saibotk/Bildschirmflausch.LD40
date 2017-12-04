using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class GameController : MonoBehaviour {

	private AudioSource gameoversound;
	private int gamestate = 0;
	private Jobmanager jobmanager;

	[SerializeField]
	private GameUI gui;


	//Player related Objects
	[Header("Player")]
	[SerializeField]
	private GameObject player;
	[Space(5)]

	[Header("Jobs")]
	[SerializeField]
	private int jobTimeInterval = 40;
	[SerializeField]
	private List<GameObject> questNPCs;
	[SerializeField]
	private List<GameObject> coffeeNPCs;
	[Space(5)]

	[Header("Watering Job")]
	[SerializeField]
	private List<GameObject> plants;
	[Space(5)]

	[Header("Delivery Job")]
	[SerializeField]
	private GameObject letterPrefab;
	[SerializeField]
	private Transform letterSpawnpoint;
	[Space(5)]

	[Header("Cleaning Job")]
	[SerializeField]
	private Transform broomSpawn;
	[SerializeField]
	private GameObject broomPrefab;
	[SerializeField]
	private List<Transform> questDirtSpots;
	[SerializeField]
	private GameObject dirtPrefab;
	[Space(10)]

	private int score;
	[Header("Don't touch")]
	public List<GameObject> availableQuestNPCs;
	public List<GameObject> availableQuestPlants;
	public List<Transform> availableQuestDirtSpots;
    public AudioMixerSnapshot gameover;

    private float lastJob;

	private List<string> jobTypes = new List<string>();

	// Use this for initialization
	void Start () {
		// JOB TYPES INIT
		jobTypes.Add("delivery");
		jobTypes.Add ("watering");
		jobTypes.Add ("cleaning");

		score = 0;
		lastJob = Time.realtimeSinceStartup;
		jobmanager = new Jobmanager (this);
		//coffeeNPCs = new List<GameObject>();
		availableQuestNPCs = new List<GameObject> (questNPCs);
		availableQuestPlants = new List<GameObject> (plants);
		availableQuestDirtSpots = new List<Transform> (questDirtSpots);
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
			int index = Random.Range (0, Mathf.Max (0, availableQuestPlants.Count - 3));
			int count = (availableQuestPlants.Count >= 3) ? 3 : availableQuestPlants.Count;
			jobmanager.AddJob (new WateringJob (plants.ConvertAll<JobInteraction> (x => x.GetComponent<JobInteraction> ()).GetRange (index, count), this.jobmanager));
			lastJob = Time.realtimeSinceStartup;
			availableQuestPlants.RemoveRange (index, count);
			Debug.Log ("Job: Waterings!");
			break;
		case "cleaning":
			if (questDirtSpots.Count == 0 || availableQuestDirtSpots.Count == 0)
				return; // Todo here should the code try to retrieve another job.
			int cindex = Random.Range (0, Mathf.Max (0, availableQuestDirtSpots.Count - 3));
			int ccount = (availableQuestDirtSpots.Count >= 3) ? 3 : availableQuestDirtSpots.Count;
			jobmanager.AddJob (new CleaningJob (availableQuestDirtSpots.GetRange (cindex, ccount), dirtPrefab, broomSpawn, broomPrefab, this.jobmanager));
			lastJob = Time.realtimeSinceStartup;
			availableQuestDirtSpots.RemoveRange (cindex, ccount);
			Debug.Log ("Job: Cleaning!");
			break;
		}
	}

	private void GameOver() {
		if (gamestate != 1) {
			gamestate = 1;
            gameover.TransitionTo(0f);
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

	public void AddScore(int score)  {
		this.score += score;
		gui.UpdateScore (this.score);
		Debug.Log ("Score is: " + score);
	}
}
