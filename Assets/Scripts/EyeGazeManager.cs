using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tobii.EyeTracking;
//using UnityEngine.UI;

public class EyeGazeManager : MonoBehaviour
{

	GameObject rotateButton, inventoryButton, closeInventoryButton, blueButton, redButton, blackButton;
	GazeAware rotateButtonGazeAware, inventoryButtonGazeAware, closeInventoryButtonGazeAware, redButtonGazeAware, blueButtonGazeAware, blackButtonGazeAware;
	GameObject car;
	bool rotateCar, inventoryPanel, implodeFlag, gazeStarted;
	public bool bringCarPartToCenter;
	Animator carAnimator;
	VariableManager vm;
	string implode;
	GameObject carPart, engine, interior, bodyFront, bodyRear;
	GazeAware engineGazeAware, interiorGazeAware, bodyFrontGazeAware, bodyRearGazeAware;
	Material[] originalMaterial;

	float startTime;

	// Use this for initialization
	void Start()
	{


		vm = GameObject.Find("Managers").GetComponent<VariableManager>();

		rotateCar = false;
		inventoryPanel = false;

		rotateButton = GameObject.Find("Rotate");
		rotateButtonGazeAware = rotateButton.GetComponent<GazeAware>();

		inventoryButton = GameObject.Find("Inventory");
		inventoryButtonGazeAware = inventoryButton.GetComponent<GazeAware>();

		closeInventoryButton = GameObject.Find("Close Inventory");
		closeInventoryButtonGazeAware = closeInventoryButton.GetComponent<GazeAware>();

		vm.SetButton(closeInventoryButton, false);

		blueButton = GameObject.Find("Blue");
		vm.SetButton(blueButton, false);

		blackButton = GameObject.Find("Black");
		vm.SetButton(blackButton, false);

		redButton = GameObject.Find("Red");
		vm.SetButton(redButton, false);

		blueButtonGazeAware = blueButton.GetComponent<GazeAware> ();
		redButtonGazeAware = redButton.GetComponent<GazeAware> ();
		blackButtonGazeAware = blackButton.GetComponent<GazeAware> ();

		car = GameObject.Find("Audi R8");
		carAnimator = car.GetComponent<Animator>();



		// Car Parts

		engine = GameObject.Find ("Engine");
		engineGazeAware = engine.GetComponent<GazeAware> ();

		interior = GameObject.Find ("Interior");
		interiorGazeAware = interior.GetComponent<GazeAware> ();

		bodyFront = GameObject.Find ("BodyFront");
		bodyFrontGazeAware = bodyFront.GetComponent<GazeAware> ();

		bodyRear = GameObject.Find ("BodyRear");
		bodyRearGazeAware = bodyRear.GetComponent<GazeAware> ();

		implodeFlag = false;
		bringCarPartToCenter = false;
		gazeStarted = false;
	}

	// Update is called once per frame
	void Update() {




		if (rotateCar) {
			car.transform.Rotate(0, 30 * Time.deltaTime, 0);
		}


		// FUNCTIONALITY

		if (rotateButtonGazeAware.HasGazeFocus) {
			gazeTimer ("Rotate");
		} 
		else if (inventoryButtonGazeAware.HasGazeFocus) {
			gazeTimer ("Inventory");

		}
		else if (engineGazeAware.HasGazeFocus) {
			gazeTimer ("Car Body");

		} /*else if (interiorGazeAware.HasGazeFocus) {
				carPart = interior;
				modelCar = true;
				inventoryPanel = false;
			} else if (bodyFrontGazeAware.HasGazeFocus || bodyRearGazeAware.HasGazeFocus) {
				carPart = bodyFront;
				modelCar = true;
				inventoryPanel = false;
			}*/

		//print (temp);

		else if (redButtonGazeAware.HasGazeFocus) {//if (carPart != null && redButtonGazeAware.HasGazeFocus) {
			gazeTimer("Red");

		} else if (blueButtonGazeAware.HasGazeFocus) {//else if (carPart!=null && blueButtonGazeAware.HasGazeFocus)
			gazeTimer("Blue");
		} else if (blackButtonGazeAware.HasGazeFocus) {//else if (carPart!=null && blackButtonGazeAware.HasGazeFocus)
			gazeTimer("Black");
		} else if (closeInventoryButtonGazeAware.HasGazeFocus) {
			gazeTimer ("Close Inventory");
		}
		else {
			gazeStarted = false;
		}

		if (carPart!=null && bringCarPartToCenter) {
			vm.ModelCar (carAnimator, carPart);
			vm.SetButton (closeInventoryButton, true);
			vm.SetButton (blueButton, true);
			vm.SetButton (redButton, true);
			vm.SetButton (blackButton, true);
		}

	}



	public void SetImplodePart(string value) {
		implode = value;
	}


	//button actions
	public void colorButtonClick(string color){
		if(carPart != null)
			vm.SetMaterial (carPart, color);
	}

	public void closeInventoryButtonClick(){
		vm.SetButton (blueButton, false);
		vm.SetButton (redButton, false);
		vm.SetButton (blackButton, false);
		if (!implodeFlag && implode == "Engine") {
			carAnimator.SetInteger ("Engine", 0);
			implodeFlag = true;
			inventoryPanel = true;
		} else if (implodeFlag) {
			vm.SetButton (closeInventoryButton, false);
			vm.SetButton (inventoryButton, true);
			carAnimator.SetBool ("Explode", false);
			carAnimator.SetBool ("Implode", true);
			implodeFlag = false;
			implode = null;
		}
	}

	public void rotateButtonClick(){
		rotateCar = !rotateCar;
	}

	public void inventoryButtonClick(){
		inventoryPanel = true;
		vm.SetButton (inventoryButton, false);
		carAnimator.SetBool("Explode", true);
		carAnimator.SetBool("Implode", false);
	}

	public void carBodyClick(string strCarPart){
		if (inventoryPanel && strCarPart=="carPartEngine") {
			carPart = engine;
			bringCarPartToCenter = true;
			inventoryPanel = false;
		}
	}


	//Gaze Timer

	public void gazeTimer(string functionName) {
		if (!gazeStarted) {
			startTime = Time.time;
			gazeStarted = true;
		}

		if (gazeStarted && Time.time > startTime + 2.0f) {
			gazeStarted = false;

			switch (functionName) {

			case "Rotate":
				rotateButtonClick ();
				break;
			case "Inventory":
				inventoryButtonClick ();
				break;
			case "Car Body":
				carBodyClick ("carPartEngine");
				break;

			case "Red":
				colorButtonClick ("Red");
				break;

			case "Blue":
				colorButtonClick ("Blue");
				break;

			case "Black":
				colorButtonClick ("Black");
				break;
			case "Close Inventory":
				closeInventoryButtonClick ();
				break;
			
			default:
				print ("Could not find the correct option!");
				break;
			
			}

		}
	}

}
