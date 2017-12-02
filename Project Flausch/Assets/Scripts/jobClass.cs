
public class Job
{
	private bool status = true;						// true = active, false = inactiv (done, aborted)
	private string taskName = "Missing taskName";					// name of the task / headline
	private string client = "Missing client";
	private string jobReward = "Missing jobReward";
	private string jobDescription = "Missing jobDescription";			// jobdescribtion
	// taskKind

	public Job(string taskName, string client, string jobDescription, string jobReward)
	{
		this.taskName = taskName;
		this.client = client;
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
		return this.client();
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
		this.client = client;
	}

	public void SetJobReward( string jobReward)
	{
		this.jobReward = jobReward;
	}

	public void SetJobDescription( string jobDescription)
	{
		this.jobDesciption = jobDescription;
	}


}
