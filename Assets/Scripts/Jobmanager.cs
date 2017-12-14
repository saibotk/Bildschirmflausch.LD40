using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jobmanager {

	private static readonly float[] JOB_PROBABILITIES = new float[] {0.3f, 0.8f, 1.0f};
	
	private List<Job> jobList = new List<Job>();
	private List<JobType> jobTypes = new List<JobType> ();
	//private Dictionary<string, int> jobCount;
	private int maxJobs = 2;
	private GameController controller;

	public Jobmanager (GameController controller) {
		this.controller = controller;

		jobTypes.Add ((JobType) new WateringJobType (controller, this));
		jobTypes.Add ((JobType) new DeliveryJobType (controller, this));
		jobTypes.Add ((JobType) new CleaningJobType (controller, this));
	}

	public JobType getJobType(string typeName) {
		return jobTypes.Find (type => type.GetName ().Equals (typeName));
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
				controller.GetPlayer().GetComponent<AudioControl>().sfxplay(4);
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
		controller.AddScore (job.GetScoreValue() * jobList.Count * jobList.Count);
		controller.GetPlayer().GetComponent<AudioControl>().sfxplay(3);
		RemoveJob (job);
		if (Random.value < 0.5)
			addRandomJob ();
	}

	public void addRandomJob() {
		Job job = jobTypes[GetRandomJobType()].CreateJob ();
		Debug.Log ("Adding random job " + job + ".");
		if (job != null)
			AddJob (job);
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
}
