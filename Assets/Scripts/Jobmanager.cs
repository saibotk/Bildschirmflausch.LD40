using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jobmanager {
	private List<Job> jobList = new List<Job>();
	private Dictionary<string, JobType> jobTypes = new Dictionary<string, JobType> ();
	private GameController controller;

	public Jobmanager (GameController controller) {
		this.controller = controller;

		jobTypes.Add ("watering", (JobType) new WateringJobType (controller, this));
		jobTypes.Add ("delivery", (JobType) new DeliveryJobType (controller, this));
		jobTypes.Add ("cleaning", (JobType) new CleaningJobType (controller, this));
	}

	public JobType getJobType(string typeName) {
		return jobTypes [typeName];
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
				addRandomJob ();
				addRandomJob ();
			}
		}
		if (GetAllJobs ().Count == 0 && Random.value > 0.995)
			addRandomJob ();
	}

	// AddJob: Job -> void
	// Adds the given job at the begin
	public void AddJob(Job inputJob) {
		jobList.Add(inputJob);
		// TODO init job
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

	public void finishedJob(Job job) {
		controller.AddScore (job.GetScoreValue());
		controller.GetPlayer().GetComponent<AudioControl>().sfxplay(3);
		RemoveJob (job);
		addRandomJob ();
	}

	public void addRandomJob() {
		Job job = getJobType ("watering").CreateJob ();
		if (job != null)
			AddJob (job);
	}
}
