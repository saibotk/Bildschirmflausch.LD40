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

	[SerializeField]
	private GameObject gameObject;

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
		Time.timeScale = 1;
		score = 0;

		jobmanager = new Jobmanager (this);
		jobmanager.addRandomJob ();
	}
		
	// Update is called once per frame
	void Update () {
		if (gamestate == 0) {
			jobmanager.Update ();
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
	}

	public PlayerController GetPlayer() {
		return player.GetComponent<PlayerController>();
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
		foreach (GameObject NPC in GetJobObjects ("CoffeeNPCs")) {
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

	public Jobmanager getJobManager() {
		return jobmanager;
	}

	// Maybe make private
	public GameObject GetGameObject() {
		return gameObject;
	}

	/** Returns one of the direct children of the _Gamecontroller object by its name. */
	public GameObject GetPrefab(string name) {
		return gameObject.transform.Find (name).gameObject;
	}

	protected Dictionary<string, List<GameObject>> jobObjects = new Dictionary<string, List<GameObject>> ();

	public void AddJobObject(string name, GameObject go) {
		if (jobObjects.ContainsKey (name)) {
			jobObjects [name].Add (go);
		} else {
			List<GameObject> li = new List<GameObject> ();
			li.Add (go);
			jobObjects.Add(name, li);
		}
	}

	public List<GameObject> GetJobObjects(string name) {
		if (jobObjects.ContainsKey (name))
			return jobObjects [name];
		else
			return new List<GameObject> ();
	}
}
