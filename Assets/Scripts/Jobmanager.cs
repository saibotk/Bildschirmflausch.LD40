using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jobmanager {
	public enum ENTITYLISTNAMES { UNDEFINED, WATERINGPLANTS, DIRTSPAWNPOINTS, LETTERSPAWNPOINTS, BROOMSPAWNPOINTS, DELIVERYNPCS, COFFEENPCS };
	private static readonly float[] JOB_PROBABILITIES = new float[] {0.3f, 0.8f, 1.0f};
	
	private List<Job> jobList = new List<Job>();
	private List<JobFactory> jobFactorys = new List<JobFactory> ();
	private Dictionary<ENTITYLISTNAMES, List<GameObject>> jobObjects = new Dictionary<ENTITYLISTNAMES, List<GameObject>> ();
	private int maxJobs = 2;

	public Jobmanager (GameObject indicator, GameObject letterprefab, GameObject dirt, GameObject broomprefab) {
		jobFactorys.Add ((JobFactory) new WateringJobFactory (indicator));
		jobFactorys.Add ((JobFactory) new DeliveryJobFactory (letterprefab, indicator));
		jobFactorys.Add ((JobFactory) new CleaningJobFactory (dirt, broomprefab, indicator));
	}

	public T getJobFactory<T>() where T : JobFactory {
		return (T) jobFactorys.Find (x => x.GetType() is T);
	}

	// GetJobAt: integer -> Jobmanager
	// Returns the job at the given position, starting at 0
	public Job GetJobAt(int i) {
		return this.jobList[i];
	}

	public void Update() {
		List<Job> tmpJobList = new List<Job> (jobList);
		foreach (Job job in tmpJobList) {
			if (Time.realtimeSinceStartup >= (job.GetJobStartTime () + job.GetJobTime ())) {
				GameController.instance.GetPlayer().GetComponent<AudioControl>().SfxPlay(4);
				RemoveJob (job);
				maxJobs++;
				if (Random.value < 0.5)
					addRandomJob ();
			}
		}
		if (jobList.Count < maxJobs && Random.value > 0.995) // TODO vary probability
			addRandomJob ();
	}

	// AddJob: Job -> void
	// Adds the given job at the begin
	public void AddJob(Job inputJob) {
		inputJob.init ();
		jobList.Add(inputJob);
		GameUI.instance.UpdateJobListUI (new List<Job>(jobList));
	}

	// RemoveJob: integer -> void
	// Removes the job with the given number, starting with 0
	public void RemoveJob(Job job) {
		job.cleanup();
		jobList.Remove(job);
		GameUI.instance.UpdateJobListUI (new List<Job>(jobList));
	}

	// GetAllJob: void -> List<Job>
	// Returns all jobs
	public List<Job> GetAllJobs() {
		return jobList;
	}

	public void FinishedJob(Job job) {
		GameController.instance.AddScore (job.GetScoreValue() * jobList.Count * jobList.Count);
		GameController.instance.GetPlayer().GetComponent<AudioControl>().SfxPlay(3);
		RemoveJob (job);
		if (Random.value < 0.5)
			addRandomJob ();
	}

	public void addRandomJob() {
		Job job = jobFactorys[GetRandomJobType()].CreateJob ();
		Debug.Log ("Called addRandomJob");
		if (job != null) {
			Debug.Log ("Adding random job " + job.GetTaskName () + ".");
			AddJob (job);
		} else {
			// TODO
		}
	}

	private int GetRandomJobType() {
		float random = Random.value;
		// TODO weight this with the amount of job currently active
		float[] probs = JOB_PROBABILITIES;
		for (int i = 0; i < probs.Length; i++)
			if (random < probs [i])
				return i;
		Debug.LogWarning ("No random job type found; check your probabilities!");
		return 0;
	}

	public void AddJobObject(ENTITYLISTNAMES name, GameObject go) {
		if (jobObjects.ContainsKey (name)) {
			jobObjects [name].Add (go);
		} else {
			List<GameObject> li = new List<GameObject> ();
			li.Add (go);
			jobObjects.Add(name, li);
		}
	}

	public List<GameObject> GetJobObjects(ENTITYLISTNAMES name) {
		if (jobObjects.ContainsKey (name)) {
			return jobObjects [name];

		} else {
			return new List<GameObject> ();
		}
	}

	public int GetMaxJobs () {
		return this.maxJobs;
	}

}
