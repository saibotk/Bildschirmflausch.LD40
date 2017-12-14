using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper {
	
	public static List<GameObject> GetAvailable(List<GameObject> li) {
		if (li.Count == 0) {
			return li;
		} else {
			return li.FindAll (x => 
				x.GetComponent (typeof(IAvailable)) != null &&
				(x.GetComponent (typeof(IAvailable)) as IAvailable).IsAvailable (GameController.instance.GetFloor ()));
		}
	}

}


