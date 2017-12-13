using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController instance;

	private int gamestate = 0;
	private int score;
	private int floor = 0;

	private Jobmanager jobmanager;

	private List<string> jobTypes = new List<string>();

	private Dictionary<string, List<GameObject>> jobObjects = new Dictionary<string, List<GameObject>>();

	//Player related Objects
	[Header("Player")]
	[SerializeField]
	private GameObject player;
	[Space(5)]

	[Header("Jobs")]
	[SerializeField]
	private GameObject indicator;
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
	private GameObject dirtPrefab;

	// SINGLETON
	public GameController() {
		GameController.instance = this;
	}

	// Use this for initialization
	void Start () {
		// JOB TYPES INIT
		// TODO Change to ENUM
		jobTypes.Add ("watering");
		jobTypes.Add ("delivery");
		jobTypes.Add ("cleaning");
		// jobTypes.Add ("copying");
		Time.timeScale = 1;
		score = 0;

		jobmanager = new Jobmanager ();

		addRandomJob ();
	}
		
	// Update is called once per frame
	void Update () {
		if (gamestate == 0) {
			jobmanager.checkJobTimes ();
			CheckCoffeeNPCs();

			// TODO Convert too Hook
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

	public void AddJobObject(string name, GameObject go) {
		if (jobObjects.ContainsKey (name)) {
			jobObjects [name].Add (go);
		} else {
			List<GameObject> li = new List<GameObject> ();
			li.Add (go);
			jobObjects.Add(name, li);
		}
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
		}
	}

	private bool addRandomJob (List<string> rjobtypes) {
		if (rjobtypes.Count == 0)
			return false;
		GameObject jobIndicator = this.indicator;
		string jt = rjobtypes [Random.Range (0, Mathf.Min(1, jobTypes.Count))];
		switch (jt) {
			case "delivery":
			List<GameObject> aNPCs = getAvailable (getQuestNPCs());
			if(aNPCs.Count == 0 || floor == 0) {
					List<string> leftJobTypes = new List<string> (rjobtypes);
					leftJobTypes.Remove (jt);
					addRandomJob(leftJobTypes);
					return false;
				} 

				GameObject npc = aNPCs [Random.Range (0, aNPCs.Count)];
				if (npc.GetComponent<NPC> ().GetFloor() <= floor) {
					jobmanager.AddJob (new DeliveryJob (npc.GetComponent<NPC> (), letterSpawnpoint, letterPrefab, this.jobmanager, jobIndicator));
				}
				break;
			case "watering":
				List<GameObject> aPlants = getAvailable (new List<GameObject> (jobObjects["WateringPlants"]));	
				if (jobObjects.ContainsKey("WateringPlants") && jobObjects["WateringPlants"].Count == 0 || aPlants.Count == 0) {
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
				break;
			case "cleaning":
				List<GameObject> aDirtSpots = getAvailable (new List<GameObject> (jobObjects["DirtSpawnpoints"]));
				if (!jobObjects.ContainsKey("DirtSpawnpoints") && jobObjects["DirtSpawnpoints"].Count == 0 || aDirtSpots.Count == 0 || floor < 2) {
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
				jobmanager.AddJob (new CleaningJob (aDirtSpots.GetRange (cindex, ccount), dirtPrefab, broomSpawn, broomPrefab, this.jobmanager, jobIndicator));
				break;
		}
		return true;
	}

	private void GameOver() {
		if (gamestate != 1) {
			gamestate = 1;
            player.GetComponent<AudioControl>().gameoverplay();
		}
		Time.timeScale = 0;
		GameUI.instance.showGameOver ();
	}

	public void CheckCoffeeNPCs() {
		int emptyCofeeCounter = 0;
        int almostemptyCofeeCounter = 0;
		foreach (GameObject NPC in getCoffeeNPCs()) {
			if (NPC.GetComponent<CoffeeNPC> ().GetCoffeeTimer () == 0) {
				emptyCofeeCounter++;
			}
	        if (NPC.GetComponent<CoffeeNPC> ().GetCoffeeTimer() < 15)
	        {
	            almostemptyCofeeCounter++;
	        }
		}
        
        if (emptyCofeeCounter == 2 || almostemptyCofeeCounter > 2) {
			GameUI.instance.SetCoffeeWarningVisible (true);
            if(player.GetComponent<AudioControl>().sfxplaying(1))
                player.GetComponent<AudioControl>().sfxplay(1); 
        }
        else {
			GameUI.instance.SetCoffeeWarningVisible (false);
            player.GetComponent<AudioControl>().sfxstop(1);
        }
        if (emptyCofeeCounter >= 3) {
		    GameOver ();
		}
	}

	private List<GameObject> getCoffeeNPCs() {
		if (jobObjects.ContainsKey ("CoffeeNPCs")) 
			return jobObjects["CoffeeNPCs"];
		return new List<GameObject> ();

	}

	private List<GameObject> getQuestNPCs() {
		if (jobObjects.ContainsKey ("DeliveryNPCs")) 
			return jobObjects["DeliveryNPCs"];
		return new List<GameObject> ();
	}

	public void AddScore(int score)  {
		this.score += score;
		GameUI.instance.UpdateScore (this.score);
		Debug.Log ("Score is: " + score);
	}

    public int getScore() {
        return score;
    }

	public int GetFloor() {
		return floor;
	}
}
