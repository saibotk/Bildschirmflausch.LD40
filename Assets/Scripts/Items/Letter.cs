﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : Item {
	public readonly Job job;

	public Letter (Job job) : base ("Letter", Resources.Load<Sprite>("letter")) {
			this.job = job;
	}

	public override bool Equals (object obj)
	{	
		if (obj is Letter)
			return (job != null) ? job.Equals (((Letter)obj).job) : base.Equals (obj);
		return base.Equals (obj);
	}
}
