using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class GameController : MonoBehaviour {

	private int gamestate = 0;
    private bool coffeesound = false;
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
	private GameObject indicator;
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

	private List<string> jobTypes = new List<string>();

	private int floor = 0; // TODO start at 0 and count up with time.

	// Use this for initialization
	void Start () {
		// JOB TYPES INIT
		jobTypes.Add ("watering");
		jobTypes.Add ("delivery");
		jobTypes.Add ("cleaning");
		// jobTypes.Add ("copying");
		Time.timeScale = 1;
		score = 0;

		jobmanager = new Jobmanager (this);
	}
		
	// Update is called once per frame
	void Update () {
		if (gamestate == 0) {
			jobmanager.checkJobTimes ();
			CheckCoffeeNPCs();

			// Unlock new floor
			if ((floor < 1 && score >= 50) ||
			   (floor < 2 && score >= 100) ||
			   (floor < 3 && score >= 200)) {
				floor++;
				Debug.Log ("Unlocked floor " + floor);
				player.GetComponent<AudioControl> ().AddLayer (floor);
				// TODO notification
			}
		}
		if (jobmanager.GetAllJobs ().Count == 0 && Random.value > 0.995)
			addRandomJob ();
	}

	public PlayerController GetPlayer() {
		return player.GetComponent<PlayerController>();
	}
		
	private List<GameObject> getAvailable(List<GameObject> li) {
		if (li.Count == 0) {
			return li;
		} else {
			return li.FindAll (x => 
			x.GetComponent (typeof(IAvailable)) != null &&
			(x.GetComponent (typeof(IAvailable)) as IAvailable).IsAvailable (floor));
		}
	}

	public void addRandomJob() {
		if (addRandomJob (jobTypes))
	        player.GetComponent<AudioControl>().sfxplay(2);
		if (jobmanager.GetAllJobs ().Count > 5) {
			GameOver ();
			Debug.Log ("You suck at multitasking");
		}
	}

	private bool addRandomJob (List<string> rjobtypes) {
		if (rjobtypes.Count == 0)
			return false;
		GameObject jobIndicator = this.indicator;
		string jt = rjobtypes [Random.Range (0, Mathf.Min(floor+1, jobTypes.Count))];
		switch (jt) {
			case "delivery":
				List<GameObject> aNPCs = getAvailable (questNPCs);
			if(aNPCs.Count == 0 || floor == 0) {
					List<string> leftJobTypes = new List<string> (rjobtypes);
					leftJobTypes.Remove (jt);
					addRandomJob(leftJobTypes);
					return false;
				} 

				GameObject npc = aNPCs [Random.Range (0, aNPCs.Count)];
				if (npc.GetComponent<NPC> ().GetFloor() <= floor) {
					jobmanager.AddJob (new DeliveryJob (npc.GetComponent<NPC> (), letterSpawnpoint, letterPrefab, this.jobmanager, jobIndicator));
					Debug.Log ("Job: Delivery!");
				}
				break;
			case "watering":
				List<GameObject> aPlants = getAvailable (plants);	
				if (plants.Count == 0 || aPlants.Count == 0) {
					List<string> leftJobTypes = new List<string> (rjobtypes);
					leftJobTypes.Remove (jt);
					if (leftJobTypes.Count == 0) {
						return false;
					}
					addRandomJob (leftJobTypes);
					return false;
				}
				int index = Random.Range (0, Mathf.Max (0, aPlants.Count - 3));
				int count = (aPlants.Count >= 3) ? 3 : aPlants.Count;
				List<GameObject> cAPlants = new List<GameObject> (aPlants);
				jobmanager.AddJob (new WateringJob (cAPlants.ConvertAll<JobEntitiy> (x => x.GetComponent<JobEntitiy> ()).GetRange (index, count), this.jobmanager, jobIndicator));
				Debug.Log ("Job: Waterings!");
				break;
			case "cleaning":
				List<GameObject> aDirtSpots = getAvailable (new List<GameObject> (DirtSpots));
				if (DirtSpots.Count == 0 || aDirtSpots.Count == 0 || floor < 2) {
					List<string> leftJobTypes = new List<string> (rjobtypes);
					leftJobTypes.Remove (jt);
					if (leftJobTypes.Count == 0) {
						return false;
					}
					addRandomJob(leftJobTypes); 
					return false;
				}
				int cindex = Random.Range (0, Mathf.Max (0, aDirtSpots.Count - 3));
				int ccount = (aDirtSpots.Count >= 3) ? 3 : aDirtSpots.Count;
				jobmanager.AddJob (new CleaningJob (aDirtSpots.GetRange (cindex, ccount), dirtPrefab, broomSpawn, broomPrefab, this.jobmanager));
				Debug.Log ("Job: Cleaning!");
				break;
		}
		return true;
	}

	private void GameOver() {
		if (gamestate != 1) {
			gamestate = 1;
            player.GetComponent<AudioControl>().gameoverplay();
			Debug.Log ("----- GAME OVER. YOU CANT BEAT THE BOSS! ----------");
		}
		Time.timeScale = 0;
		gui.showGameOver ();
	}

	public void CheckCoffeeNPCs() {
		int emptyCofeeCounter = 0;
        int almostemptyCofeeCounter = 0;
		foreach (GameObject NPC in coffeeNPCs) {
			if (NPC.GetComponent<WorkersCoffeeNeeds> ().GetCoffeeTimer () == 0) {
				emptyCofeeCounter++;
			}
            if (NPC.GetComponent<WorkersCoffeeNeeds> ().GetCoffeeTimer() < 15)
            {
                almostemptyCofeeCounter++;
            }
		}
        
        if ((emptyCofeeCounter == 2 || almostemptyCofeeCounter > 2) && coffeesound == false)
        {
            coffeesound = true;
            player.GetComponent<AudioControl>().sfxplay(1);
            Debug.Log("Almost empty");
        }
        else if (!(emptyCofeeCounter == 2 || almostemptyCofeeCounter > 2))
        {
            player.GetComponent<AudioControl>().sfxstop(1);
            coffeesound = false;
        }
        if (emptyCofeeCounter >= 3) {
			Debug.Log ("The company ran out of coffee");
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

	public GameUI getGui() {
		return gui;
	}
}
