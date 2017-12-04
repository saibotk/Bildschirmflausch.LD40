using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IAvailable {
	void SetAvailable (bool b);
	bool IsAvailable(int floor);
}
