using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController instance;

	// 0 = playing 1= gameover -1= paused
	private int gamestate = 0;
	private int score;
	private int floor = 0;

	private Jobmanager jobmanager;

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
	[Space(5)]

	[Header("Cleaning Job")]
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

		jobmanager = new Jobmanager (indicator, letterPrefab, dirtPrefab, broomPrefab);
		GameUI.instance.InitJobListUI ();
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

	public void PauseGame() {
		if (gamestate != -1) {
			gamestate = -1;
            player.GetComponent<AudioControl>().ChangeToLift(0f);
            Time.timeScale = 0;
			GameUI.instance.ShowPauseMenu ();
		}
	}

	public void UnPauseGame() {
		if (gamestate != 0) {
			gamestate = 0;
			Time.timeScale = 1;
            player.GetComponent<AudioControl>().ChangeToMain();
			GameUI.instance.ClosePauseMenu ();
            
		}
	}

	private void GameOver() {
		if (gamestate != 1) {
			gamestate = 1;
            player.GetComponent<AudioControl>().GameOverPlay();
		}
		Time.timeScale = 0;
		GameUI.instance.ShowGameOver ();
	}

	public void CheckCoffeeNPCs() {
		int emptyCofeeCounter = 0;
        int almostemptyCofeeCounter = 0;
		foreach (GameObject NPC in jobmanager.GetJobObjects (Jobmanager.ENTITYLISTNAMES.COFFEENPCS)) {
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
            if(player.GetComponent<AudioControl>().SfxPlaying(1))
                player.GetComponent<AudioControl>().SfxPlay(1); 
        }
        else {
			GameUI.instance.SetCoffeeWarningVisible (false);
            player.GetComponent<AudioControl>().SfxStop(1);
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

    public int GetScore() {
        return score;
    }

	public int GetFloor() {
		return floor;
	}

	public int GetState () {
		return gamestate;
	}

	public Jobmanager GetJobManager() {
		return jobmanager;
	}

	// Maybe make private
	public GameObject GetGameObject() {
		return gameObject;
	}
}
