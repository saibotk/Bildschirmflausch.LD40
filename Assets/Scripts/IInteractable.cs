using System;
using UnityEngine;

public interface IInteractable {
	void Interact(GameObject player);
	bool CanInteract(GameObject player);
}