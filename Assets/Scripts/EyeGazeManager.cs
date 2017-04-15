using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.EyeTracking;
//using UnityEngine.UI;

public class EyeGazeManager : MonoBehaviour {

	GameObject rotateButton, inventoryButton, closeInventoryButton;
	GazeAware rotateButtonGazeAware, inventoryButtonGazeAware, closeInventoryButtonGazeAware;
	GameObject car;
	bool rotateCar;
	bool inventoryPanel;

	VariableManager vm;

	// Use this for initialization
	void Start () {

		vm = GameObject.Find ("Managers").GetComponent<VariableManager> ();

		rotateCar = false;
		inventoryPanel = false;

		rotateButton = GameObject.Find ("Rotate");
		rotateButtonGazeAware = rotateButton.GetComponent<GazeAware> ();

		inventoryButton = GameObject.Find ("Inventory");
		inventoryButtonGazeAware = inventoryButton.GetComponent<GazeAware> ();

		closeInventoryButton = GameObject.Find ("Close Inventory");
		closeInventoryButtonGazeAware = closeInventoryButton.GetComponent<GazeAware> ();

		car = GameObject.Find("Audi R8");
	}
	
	// Update is called once per frame
	void Update () {

		if (rotateButtonGazeAware.HasGazeFocus) {
			rotateCar = !rotateCar;
		}

		if(rotateCar){
			car.transform.Rotate (0,30*Time.deltaTime,0);
		}

		if (inventoryButtonGazeAware.HasGazeFocus) {
			ExplodeCar ();
			//vm.SetPanel (true);
		} 

		if (closeInventoryButtonGazeAware.HasGazeFocus) {
			vm.SetPanel (false);
		}


	}

	void ExplodeCar() {
		GameObject.Find ("Engine").transform.localPosition = Vector3.MoveTowards (GameObject.Find ("Engine").transform.localPosition, new Vector3 (0f, -0.5f, -2.52f),1.0f*Time.deltaTime);
	}


}
