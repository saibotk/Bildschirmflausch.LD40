using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class QuestQueueItem : MonoBehaviour {
	private Job job;

	[SerializeField]
	private GameObject indicator;

	[SerializeField]
	private GameObject timer;

	[SerializeField]
	private Texture2D texTimer;

	[SerializeField]
	private GameObject icon;

	// Use this for initialization
	void Start () {
	}

	void Update() {
		if (job != null) {
			float timestate = (Time.realtimeSinceStartup - job.GetJobStartTime ()) / job.GetJobTime () * 9;
			if (timestate < 1) {
				timer.GetComponent<UnityEngine.UI.Image> ().sprite = Sprite.Create (texTimer, new Rect (0, 0, 15, 15), new Vector2 (0.5f, 0.5f));
			} else if (timestate < 2) {
				timer.GetComponent<UnityEngine.UI.Image> ().sprite = Sprite.Create (texTimer, new Rect (16, 0, 15, 15), new Vector2 (0.5f, 0.5f));
			} else if (timestate < 3) {
				timer.GetComponent<UnityEngine.UI.Image> ().sprite = Sprite.Create (texTimer, new Rect (32, 0, 15, 15), new Vector2 (0.5f, 0.5f));
			} else if (timestate < 4) {
				timer.GetComponent<UnityEngine.UI.Image> ().sprite = Sprite.Create (texTimer, new Rect (48, 0, 15, 15), new Vector2 (0.5f, 0.5f));
			} else if (timestate < 5) {
				timer.GetComponent<UnityEngine.UI.Image> ().sprite = Sprite.Create (texTimer, new Rect (64, 0, 15, 15), new Vector2 (0.5f, 0.5f));
			} else if (timestate < 6) {
				timer.GetComponent<UnityEngine.UI.Image> ().sprite = Sprite.Create (texTimer, new Rect (80, 0, 15, 15), new Vector2 (0.5f, 0.5f));
			} else if (timestate < 7) {
				timer.GetComponent<UnityEngine.UI.Image> ().sprite = Sprite.Create (texTimer, new Rect (96, 0, 15, 15), new Vector2 (0.5f, 0.5f));
			} else if (timestate < 8.5f) {
				timer.GetComponent<UnityEngine.UI.Image> ().sprite = Sprite.Create (texTimer, new Rect (112, 0, 15, 15), new Vector2 (0.5f, 0.5f));
			} else {
				timer.GetComponent<UnityEngine.UI.Image> ().sprite = Sprite.Create (texTimer, new Rect (128, 0, 15, 15), new Vector2 (0.5f, 0.5f));
			}
		}
	}

	public void SetJob(Job j) {
		this.job = j;
		if (this.job != null) {
			indicator.GetComponent<UnityEngine.UI.Image> ().color = job.GetJobColor ();
			icon.GetComponent<UnityEngine.UI.Image> ().sprite = job.GetJobIcon ();
			icon.GetComponent<UnityEngine.UI.Image> ().color = Color.white;
			this.GetComponent<CanvasGroup> ().alpha = 1f;
		} else {
			indicator.GetComponent<UnityEngine.UI.Image> ().color = Color.grey;
			icon.GetComponent<UnityEngine.UI.Image> ().sprite = null;
			icon.GetComponent<UnityEngine.UI.Image> ().color = Color.clear;
			timer.GetComponent<UnityEngine.UI.Image> ().sprite = Sprite.Create (texTimer, new Rect (0, 0, 15, 15), new Vector2 (0.5f, 0.5f));
			this.GetComponent<CanvasGroup> ().alpha = 0.5f;
		}
	}

	public Job GetJob() {
		return this.job;
	}

}
