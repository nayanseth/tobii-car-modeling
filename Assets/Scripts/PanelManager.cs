using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour {
	GameObject panel;

	void Awake() {
		panel = GameObject.Find ("Panel");
	}

	// Use this for initialization
	void Start () {

		panel.GetComponent<Image> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
