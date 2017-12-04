using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IAvailable {
	void SetAvailable (bool b);
	/** @param floor The current level of the player */
	bool IsAvailable(int floor);
}
