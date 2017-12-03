using UnityEngine;

public class Job
{
	private int status = 0;						// true = active, false = inactiv (done, aborted)
	private string taskName = "Missing taskName";					// name of the task / headline
	private string jobClient = "Missing client";
	private string jobDescription = "Missing jobDescription";			// jobdescription
	private float jobStartTime = 0;
	private float jobTime = 0; // in seconds

	public Job(string taskName, string client, string jobDescription, float jobTime)
	{
		this.taskName = taskName;
		this.jobClient = client;
		this.jobDescription = jobDescription;
		this.jobStartTime = Time.realtimeSinceStartup;
		this.jobTime = jobTime;
	}

	// Getter
	public int GetStatus()
	{
		return this.status;
	}

	public string GetTaskName()
	{
		return this.taskName;
	}

	public string GetClient()
	{
		return this.jobClient;
	}

	public string GetJobDescription()
	{
		return this.jobDescription;
	}

	public float GetJobStartTime(float jobStartTime){
		return this.jobStartTime;
	}

	public float GetJobTime(float jobTime){
		return this.jobTime;
	}

	// Setter
	public void SetStatus( int status)
	{
		this.status = status;
	}

	public void SetTaskName( string taskName)
	{
		this.taskName = taskName;
	}

	public void SetClient( string client)
	{
		this.jobClient = client;
	}

	public void SetJobDescription( string jobDescription)
	{
		this.jobDescription = jobDescription;
	}

	public void SetJobStartTime(float jobStartTime){
		this.jobStartTime = jobStartTime;
	}

	public void SetJobTime(float jobTime){
		this.jobTime = jobTime;
	}
}
