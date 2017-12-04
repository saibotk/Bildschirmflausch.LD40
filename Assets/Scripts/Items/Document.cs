using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Document : Item {
	public readonly Job job;

	public Document (Job job) : base ("Document", Resources.Load<Sprite>("document")) {
		this.job = job;
	}

	public override bool Equals (object obj)
	{	
		if (obj is Document)
			return (job != null) ? job.Equals (((Document)obj).job) : base.Equals (obj);
		return base.Equals (obj);
	}
}
