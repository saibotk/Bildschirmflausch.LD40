using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour {
	public static GameUI instance;

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
	[SerializeField]
	private GameObject LowCoffeeHint;

	public GameUI() {
		GameUI.instance = this;
	}

	public void SetPocketImage(Sprite sp) {
		pocket.GetComponentInChildren<UnityEngine.UI.Image> ().sprite = sp;
	}

	public void SetLeftHandImage(Sprite sp) {
		left.GetComponentInChildren<UnityEngine.UI.Image> ().sprite = sp;
	}

	public void SetRightHandImage(Sprite sp) {
		coffeePot.GetComponentInChildren<UnityEngine.UI.Image> ().sprite = sp;
	}

	public void SetCoffeeWarningVisible(bool b) {
		LowCoffeeHint.SetActive (b);
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
			if (lrt.Count == 0 || lrt.Find (x => x.GetJob () != null && x.GetJob () == j) == null) {
				GameObject go = Instantiate (questQueueItemPrefab, questPanel.transform);
				go.GetComponent<QuestQueueItem> ().SetJob (j);
				go.transform.localPosition = new Vector3 (0, -i * ((RectTransform)questQueueItemPrefab.transform).rect.height);
			}
			i++;
		}


		foreach (QuestQueueItem rt in questPanel.GetComponentsInChildren<QuestQueueItem>()) {
			if (jobList.Find (x => x == rt.GetJob ()) == null) {
				DestroyImmediate (rt.gameObject);
			}
		}
			
		int i2 = 0;
		foreach (QuestQueueItem rt in questPanel.GetComponentsInChildren<QuestQueueItem>()) {
			rt.gameObject.transform.localPosition = new Vector3 (0, -i2 * ((RectTransform)questQueueItemPrefab.transform).rect.height);
			i2++;
		}

	}
}
