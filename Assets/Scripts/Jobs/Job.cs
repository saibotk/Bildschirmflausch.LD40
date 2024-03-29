using UnityEngine;

public abstract class Job
{
	private bool completed = false;												// true = active, false = inactiv (done, aborted)
	private string taskName = "Missing taskName";						// name of the task / headline
	private string jobDescription = "Missing jobDescription";			// jobdescription
	private float jobStartTime = 0;
	protected float jobTime = 0; 											// in seconds
	private int scoreValue = 0;
	private Sprite jobIcon;
	protected Color jobColor;

	public Job(string taskName, string jobDescription, float jobTime, int scoreValue, Sprite jobIcon)
	{
		this.taskName = taskName;
		this.jobDescription = jobDescription;
		this.jobStartTime = Time.realtimeSinceStartup;
		this.jobTime = jobTime;
		this.scoreValue = scoreValue;
		this.jobIcon = jobIcon;
		this.jobColor = new Color (Random.Range (0.0F, 1.0F), Random.Range (0.0F, 1.0F), Random.Range (0.0F, 1.0F), 1);
	}

	// Getter
	public bool GetStatus() {
		return this.completed;
	}

	public string GetTaskName()
	{
		return this.taskName;
	}

	public string GetJobDescription()
	{
		return this.jobDescription;
	}

	public float GetJobStartTime(){
		return this.jobStartTime;
	}

	public float GetJobTime(){
		return this.jobTime;
	}

	public int GetScoreValue() {
		return this.scoreValue;
	}

	public Color GetJobColor() {
		return this.jobColor;
	}

	public Sprite GetJobIcon() {
		return this.jobIcon;
	}

	// Setter
	public void SetStatus( bool status)
	{
		this.completed = status;
	}

	public void SetTaskName( string taskName)
	{
		this.taskName = taskName;
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

	abstract public void init ();

	abstract public void cleanup ();
}
