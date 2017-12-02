public class Jobmanager
{
	private List<Job> jobList = new List<Job>();

	// GetJobAt: integer -> Jobmanager
	// Returns the job at the given position, starting at 0
	public Job GetJobAt(integer i)
	{
		returns this.jobList[i];
	}

	// AddJob: Job -> void
	// Adds the given job at the begin
	public void AddJob(Job inputJob)
	{
		jobList.Add(inputJob);
	}

	// RemoveJob: integer -> void
	// Removes the job with the given number, starting with 0
	public void RemoveJob(int i)
	{
		jobList.RemoveAt(i);
	}

	// GetAllJobs: void -> List<Job>
	// Returns all jobs
	public void GetAllJobs()
	{
		return jobsList;
	}
}
