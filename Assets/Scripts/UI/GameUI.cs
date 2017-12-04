﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour {
	[SerializeField]
	private GameObject pocket;
	[SerializeField]
	private GameObject left;
	[SerializeField]
	private GameObject right;

	public void SetPocketImage(Sprite sp) {
		pocket.GetComponentInChildren<UnityEngine.UI.Image> ().sprite = sp;
	}

	public void SetLeftHandImage(Sprite sp) {
		left.GetComponentInChildren<UnityEngine.UI.Image> ().sprite = sp;
	}

	public void SetRightHandImage(Sprite sp) {
		right.GetComponentInChildren<UnityEngine.UI.Image> ().sprite = sp;
	}
}