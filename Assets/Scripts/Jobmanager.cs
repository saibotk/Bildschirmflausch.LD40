using System.Collections.Generic;
using UnityEngine;

public class Jobmanager
{
	private List<Job> jobList = new List<Job>();
	private GameController manager;

	public Jobmanager(GameController manager) {
		this.manager = manager;
	}

	// GetJobAt: integer -> Jobmanager
	// Returns the job at the given position, starting at 0
	public Job GetJobAt(int i)
	{
		return this.jobList[i];
	}

	public void checkJobTimes() {
		List<Job> tmpJobList = new List<Job> (jobList);
		foreach (Job job in tmpJobList) {
			if (Time.realtimeSinceStartup >= (job.GetJobStartTime () + job.GetJobTime ())) {
				Debug.Log ("Failed Job " + job.GetTaskName() + ", you too late my son!");
                GetGameController().GetPlayer().GetComponent<AudioControl>().sfxplay(4);
				RemoveJob (job);
				manager.addRandomJob ();
				manager.addRandomJob ();
			}
		}
	}

	// AddJob: Job -> void
	// Adds the given job at the begin
	public void AddJob(Job inputJob)
	{
		Debug.Log ("cAddJobbefore: " + jobList.Count);
		jobList.Add(inputJob);
		Debug.Log ("cAddJob: " + jobList.Count);
		manager.getGui ().UpdateJobListUI (new List<Job>(jobList));
	}

	// RemoveJob: integer -> void
	// Removes the job with the given number, starting with 0
	public void RemoveJob(Job job)
	{
		job.cleanup();
		Debug.Log ("cRemoveJobbefore: " + jobList.Count);
		jobList.Remove(job);
		Debug.Log ("cRemoveJobafter: " + jobList.Count);
		manager.getGui ().UpdateJobListUI (new List<Job>(jobList));
	}

	// GetAllJob: void -> List<Job>
	// Returns all jobs
	public List<Job> GetAllJobs()
	{
		return jobList;
	}

	public void finishedJob(Job job) {
		GetGameController ().AddScore (job.GetScoreValue());
        GetGameController().GetPlayer().GetComponent<AudioControl>().sfxplay(3);
        RemoveJob (job);
		manager.addRandomJob ();
	}

	public GameController GetGameController() {
		return this.manager;
	}
}
