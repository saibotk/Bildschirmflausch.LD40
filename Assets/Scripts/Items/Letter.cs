using System.Collections;
using System.Collections.Generic;

public class Letter : Item {
	public readonly Job job;

	public Letter (Job job) : base ("Letter") {
			this.job = job;
	}
}
