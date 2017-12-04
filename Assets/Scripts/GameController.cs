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
	private List<GameObject> questNPCs; // TODO merge both into one seperate by isQuestNPC()
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
	private List<GameObject> DirtSpots;
	[SerializeField]
	private GameObject dirtPrefab;
	[Space(10)]

	private int score;
	[Header("Don't touch")]
    public AudioMixerSnapshot gameover;

	private List<string> jobTypes = new List<string>();

	private int floor = 4; // TODO start at 0 and count up with time.

	// Use this for initialization
	void Start () {
		// JOB TYPES INIT
		jobTypes.Add ("delivery");
		jobTypes.Add ("watering");
		jobTypes.Add ("cleaning");

		score = 0;

		jobmanager = new Jobmanager (this);

		addRandomJob ();

        gameoversound = GetComponent<AudioSource>();
	}
		
	// Update is called once per frame
	void Update () {
		jobmanager.checkJobTimes ();
		CheckCoffeeNPCs ();
	}

	public PlayerController GetPlayer() {
		return player.GetComponent<PlayerController>();
	}
		
	private List<GameObject> getAvailable(List<GameObject> li) {
		return li.FindAll (x => 
			x.GetComponent(typeof(IAvailable)) != null && 
			(x.GetComponent(typeof(IAvailable)) as IAvailable).IsAvailable(floor));
	}

	public void addRandomJob() {
		addRandomJob (jobTypes [Random.Range (0, jobTypes.Count)]);
	}

	public void addRandomJob (string s) {
		switch (s) {
			case "delivery":
				List<GameObject> aNPCs = getAvailable (questNPCs);
				if(aNPCs.Count == 0) {
					List<string> leftJobTypes = new List<string> (jobTypes);
					leftJobTypes.Remove (s);
					addRandomJob(leftJobTypes [Random.Range (0, leftJobTypes.Count)]);
				} 

				GameObject npc = aNPCs [Random.Range (0, questNPCs.Count)];
				if (npc.GetComponent<NPC> ().GetFloor() <= floor) {
					jobmanager.AddJob (new DeliveryJob (npc.GetComponent<NPC> (), letterSpawnpoint, letterPrefab, this.jobmanager));
					Debug.Log ("Job: Delivery!");
				}
				break;
			case "watering":
				List<GameObject> aPlants = getAvailable (plants);	
				if (plants.Count == 0 || aPlants.Count == 0) {
					List<string> leftJobTypes = new List<string> (jobTypes);
					leftJobTypes.Remove (s);
					addRandomJob (leftJobTypes [Random.Range (0, leftJobTypes.Count)]);
				}
				int index = Random.Range (0, Mathf.Max (0, aPlants.Count - 3));
				int count = (aPlants.Count >= 3) ? 3 : aPlants.Count;
				List<GameObject> cAPlants = new List<GameObject> (aPlants);
				jobmanager.AddJob (new WateringJob (cAPlants.ConvertAll<JobEntitiy> (x => x.GetComponent<JobEntitiy> ()).GetRange (index, count), this.jobmanager));
				Debug.Log ("Job: Waterings!");
				break;
			case "cleaning":
				List<GameObject> aDirtSpots = getAvailable(new List<GameObject> (DirtSpots));
				if (DirtSpots.Count == 0 || aDirtSpots.Count == 0) {
					List<string> leftJobTypes = new List<string> (jobTypes);
					leftJobTypes.Remove (s);
					addRandomJob(leftJobTypes [Random.Range (0, leftJobTypes.Count)]);
				}
				
				int cindex = Random.Range (0, Mathf.Max (0, aDirtSpots.Count - 3));
				int ccount = (aDirtSpots.Count >= 3) ? 3 : aDirtSpots.Count;
				jobmanager.AddJob (new CleaningJob (aDirtSpots.GetRange (cindex, ccount), dirtPrefab, broomSpawn, broomPrefab, this.jobmanager));
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

    public int getScore() {
        return score;
    }

	public int GetFloor() {
		return floor;
	}
}
