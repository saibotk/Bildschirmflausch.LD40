
public class Job
{
	private bool status = true;						// true = active, false = inactiv (done, aborted)
	private string taskName = "Missing taskName";					// name of the task / headline
	private string jobClient = "Missing client";
	private string jobReward = "Missing jobReward";
	private string jobDescription = "Missing jobDescription";			// jobdescription
	// needs job time
	// taskKind

	public Job(string taskName, string client, string jobDescription, string jobReward)
	{
		this.taskName = taskName;
		this.jobClient = client;
		this.jobDescription = jobDescription;
		this.jobReward = jobReward;
	}

	// Getter
	public bool GetStatus()
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

	public string GetJobReward()
	{
		return this.jobReward;
	}

	public string GetJobDescription()
	{
		return this.jobDescription;
	}

	// Setter
	public void SetStatus( bool status)
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

	public void SetJobReward( string jobReward)
	{
		this.jobReward = jobReward;
	}

	public void SetJobDescription( string jobDescription)
	{
		this.jobDescription = jobDescription;
	}


}
