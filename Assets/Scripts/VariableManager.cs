using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariableManager : MonoBehaviour {
	GameObject panel;

	void Awake() {
		panel = GameObject.Find ("Panel");
		panel.GetComponent<Image> ().enabled = false;
	}

	public void SetPanel(bool value) {
		panel.GetComponent<Image> ().enabled = value;
	}


	
	// Update is called once per frame
	void Update () {
		
	}
}
