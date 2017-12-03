using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryJob : Job {

 
	public DeliveryJob() : base ("Delivery", "Deliver the item!", 30f) {
		init ();
	}

	override public void init() {

	}

	override public void cleanup() {

	}
	
}
