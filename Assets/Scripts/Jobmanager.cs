using System.Collections.Generic;
using UnityEngine;

public class Jobmanager
{
	private List<Job> jobList = new List<Job>();

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
				GameController.instance.GetPlayer().GetComponent<AudioControl>().sfxplay(4);
				RemoveJob (job);
				GameController.instance.addRandomJob ();
				GameController.instance.addRandomJob ();
			}
		}
	}

	// AddJob: Job -> void
	// Adds the given job at the begin
	public void AddJob(Job inputJob) {
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

	public void finishedJob(Job job) {
		GameController.instance.AddScore (job.GetScoreValue());
        GameController.instance.GetPlayer().GetComponent<AudioControl>().sfxplay(3);
        RemoveJob (job);
		GameController.instance.addRandomJob ();
	}
}
