using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour {
	public void LoadSceneByIndex(int index) {
		UnityEngine.SceneManagement.SceneManager.LoadScene (index);
	}

	public void quitGame() {
		UnityEngine.Application.Quit ();
	}
}
