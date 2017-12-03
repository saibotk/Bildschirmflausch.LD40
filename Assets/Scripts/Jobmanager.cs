using System.Collections.Generic;
using UnityEngine;

public class Jobmanager
{
	private List<Job> jobList = new List<Job>();
	private GameController manager;


	public Jobmanager(GameController manger) {
		this.manager = manger;
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
				manager.addRandomJob ();
				manager.addRandomJob ();
				RemoveJob (job);
			}
		}
	}

	// AddJob: Job -> void
	// Adds the given job at the begin
	public void AddJob(Job inputJob)
	{
		jobList.Add(inputJob);
	}

	// RemoveJob: integer -> void
	// Removes the job with the given number, starting with 0
	public void RemoveJob(Job job)
	{
		jobList.Remove(job);
	}

	// GetAllJob: void -> List<Job>
	// Returns all jobs
	public List<Job> GetAllJob()
	{
		return jobList;
	}
}
