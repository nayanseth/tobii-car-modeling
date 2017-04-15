using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariableManager : MonoBehaviour {
	//GameObject panel;

	void Awake() {
		//panel = GameObject.Find ("Panel");
		//panel.GetComponent<Image> ().enabled = false;
	}

	public void SetButton(GameObject button, bool value) {
		button.GetComponent<Image>().enabled = value;
		button.GetComponent<Button>().enabled = value;
		button.GetComponentInChildren<Text>().enabled = value;

	}
}
