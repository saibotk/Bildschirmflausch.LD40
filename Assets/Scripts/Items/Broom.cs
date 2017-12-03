using System.Collections;
using System.Collections.Generic;

public class Broom : Item {
	public readonly Job job;

	public Broom (Job job) : base ("Broom") {
		this.job = job;
	}


}
