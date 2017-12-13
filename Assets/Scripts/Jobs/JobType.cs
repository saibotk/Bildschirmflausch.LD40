using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class JobType<T> where T : Job {

	protected Dictionary<string, List<GameObject>> jobObjects = new Dictionary<string, List<GameObject>> ();
	protected GameController controller;
	protected Jobmanager manager;

	protected JobType(string name, GameController controller, Jobmanager manager) {
		this.controller = controller;
		this.manager = manager;
	}

	public abstract T CreateJob();

	public void AddJobObject(string name, GameObject go) {
		if (jobObjects.ContainsKey (name)) {
			jobObjects [name].Add (go);
		} else {
			List<GameObject> li = new List<GameObject> ();
			li.Add (go);
			jobObjects.Add(name, li);
		}
	}

	public List<GameObject> getGameObjects(string name) {
		if (jobObjects.ContainsKey (name))
			return jobObjects [name];
		else
			return new List<GameObject> ();
	}

	// TODO make this protected and non-static after refactoring
	public static List<GameObject> getAvailable(List<GameObject> li) {
		if (li.Count == 0) {
			return li;
		} else {
			return li.FindAll (x => 
				x.GetComponent (typeof(IAvailable)) != null &&
				(x.GetComponent (typeof(IAvailable)) as IAvailable).IsAvailable (GameController.instance.GetFloor ()));
		}
	}
}