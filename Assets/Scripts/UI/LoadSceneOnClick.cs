﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneOnClick : MonoBehaviour {
	public void LoadSceneByIndex(int index) {
		UnityEngine.SceneManagement.SceneManager.LoadScene (index);
	}
}
