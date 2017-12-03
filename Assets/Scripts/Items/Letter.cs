using System.Collections;
using System.Collections.Generic;

public class Letter : Item {
	public readonly Job job;

	public Letter (Job job) : base ("Letter") {
			this.job = job;
	}

	public override bool Equals (object obj)
	{	
		if (obj.GetType () is Letter)
			return job.Equals (((Letter)obj).job);
		return base.Equals (obj);
	}
}
