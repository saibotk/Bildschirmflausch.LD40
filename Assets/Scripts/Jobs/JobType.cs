using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class JobType {

	protected GameController controller;
	protected Jobmanager manager;

	protected JobType(string name, GameController controller, Jobmanager manager) {
		this.controller = controller;
		this.manager = manager;
	}

	public abstract Job CreateJob();

	protected List<GameObject> getAvailable(List<GameObject> li) {
		if (li.Count == 0) {
			return li;
		} else {
			return li.FindAll (x => 
				x.GetComponent (typeof(IAvailable)) != null &&
				(x.GetComponent (typeof(IAvailable)) as IAvailable).IsAvailable (controller.GetFloor ()));
		}
	}
}