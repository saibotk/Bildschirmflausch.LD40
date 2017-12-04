using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour {
	[SerializeField]
	private GameObject pocket;
	[SerializeField]
	private GameObject left;
	[SerializeField]
	private GameObject right;
	[SerializeField]
	private GameObject score;
	[SerializeField]
	private GameObject coffeePot;
	[SerializeField]
	private GameObject coffeePotFill;
	[SerializeField]
	private GameObject gameOverPanel;

	public void SetPocketImage(Sprite sp) {
		pocket.GetComponentInChildren<UnityEngine.UI.Image> ().sprite = sp;
	}

	public void SetLeftHandImage(Sprite sp) {
		left.GetComponentInChildren<UnityEngine.UI.Image> ().sprite = sp;
	}

	public void SetRightHandImage(Sprite sp) {
		coffeePot.GetComponentInChildren<UnityEngine.UI.Image> ().sprite = sp;
	}

	public void UpdateScore(int score) {
		this.score.GetComponentInChildren<UnityEngine.UI.Text>().text = score.ToString();
	}

	public void SetCoffeePotEnabled(bool b) {
		coffeePot.SetActive (b);
	}

	public void SetCoffeePotFillImage(Sprite sp) {
		coffeePotFill.GetComponentInChildren<UnityEngine.UI.Image> ().sprite = sp;
	}

	public void showGameOver() {
		gameOverPanel.SetActive (true);
	}

	public void UpdateJobListUI(List<Job> jobList) {

	}
}
