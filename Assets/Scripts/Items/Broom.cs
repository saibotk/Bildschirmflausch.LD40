using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : Item {
	public readonly Job job;

	public Broom (Job job) : base ("Broom", Resources.Load<Sprite>("broom")) {
		this.job = job;
	}


}
