using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestQueueItem : MonoBehaviour {
	private Job job;

	[SerializeField]
	private GameObject indicator;

	[SerializeField]
	private GameObject icon;

	// Use this for initialization
	void Start () {

	}

	public void SetJob(Job j) {
		this.job = j;
		indicator.GetComponent<UnityEngine.UI.Image>().color = job.GetJobColor();
		icon.GetComponent<UnityEngine.UI.Image> ().sprite = job.GetJobIcon ();
	}

	public Job GetJob() {
		return this.job;
	}

}
