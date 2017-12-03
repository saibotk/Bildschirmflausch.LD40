using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
private List<string> jobTypes = new List<string>();

// Use this for initialization
void Start () {

}

// Update is called once per frame
void Update () {
	if (Time.realtimeSinceStartup >= lastJob + jobTimeInterval) {
		addRandomJob ();
	}
}

public void addRandomJob () {
	string jobType = jobTypes[Random.Range (0, jobTypes.Count)];
	switch (jobType) {
	case "bring":
		//      jobManager.AddJob (new Job ("test", "test", "super Test", 30));
		break; 
	}
}
}