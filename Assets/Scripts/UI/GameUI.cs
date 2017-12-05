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
	[SerializeField]
	private GameObject questPanel;
	[SerializeField]
	private GameObject questQueueItemPrefab;

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
		int i = 0;
		foreach (Job j in jobList) {
			List<QuestQueueItem> lrt = new List<QuestQueueItem> (questPanel.GetComponentsInChildren<QuestQueueItem> ());
			Debug.Log ("count " + lrt.Count);
			if (lrt.Count == 0 || lrt.Find (x => x.GetJob() != null && x.GetJob () != j) == null) {
				GameObject go = Instantiate (questQueueItemPrefab, questPanel.transform);
				go.GetComponent<QuestQueueItem> ().SetJob (j);
				go.transform.localPosition = new Vector3 (0, -i * ((RectTransform)questQueueItemPrefab.transform).rect.width);
				Debug.Log ("Added Hint");
			}
			i++;
		}
		foreach (QuestQueueItem rt in questPanel.GetComponentsInChildren<QuestQueueItem>()) {
			if (jobList.Find (x => x == rt.GetJob ()) == null) {
				Debug.Log ("Removed Hint for job: " + rt.GetJob());
				Destroy (rt.gameObject);
			}
		}
	}
}
