using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class JobType {

	public static readonly int WATERING = 0;
	public static readonly int DELIVERY = 1;
	public static readonly int CLEANING = 2;
	//public static readonly Dictionary<string, JobType> JOB_TYPES = new Dictionary<string, JobType>();

	protected string name;
	protected GameController controller;
	protected Jobmanager manager;

	protected JobType(string name, GameController controller, Jobmanager manager) {
		this.name = name;
		this.controller = controller;
		this.manager = manager;
		//JOB_TYPES.Add (name, this);
	}

	public string GetName() {
		return name;
	}

	public abstract Job CreateJob();

	protected List<GameObject> GetAvailable(List<GameObject> li) {
		if (li.Count == 0) {
			return li;
		} else {
			return li.FindAll (x => 
				x.GetComponent (typeof(IAvailable)) != null &&
				(x.GetComponent (typeof(IAvailable)) as IAvailable).IsAvailable (controller.GetFloor ()));
		}
	}
}