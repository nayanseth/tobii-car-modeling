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
	bool rotateCar;
	bool inventoryPanel;
	public bool modelCar;
	Animator carAnimator;
	VariableManager vm;
	string implode;

	GameObject carPart, engine, interior, bodyFront, bodyRear;
	GazeAware engineGazeAware, interiorGazeAware, bodyFrontGazeAware, bodyRearGazeAware;
	Material[] originalMaterial;


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

		modelCar = false;
	}

	// Update is called once per frame
	void Update() {


		if (rotateButtonGazeAware.HasGazeFocus) {
			rotateCar = !rotateCar;
		}

		if (rotateCar) {
			car.transform.Rotate(0, 30 * Time.deltaTime, 0);
		}

		if (inventoryButtonGazeAware.HasGazeFocus) {
			inventoryPanel = true;
			//vm.SetPanel (true);
		}

		if (closeInventoryButtonGazeAware.HasGazeFocus) {
			carAnimator.SetBool("Explode", false);
			carAnimator.SetBool("Implode", true);
		}

		if (inventoryPanel) {
			vm.SetButton (inventoryButton, false);
			carAnimator.SetBool("Explode", true);
			carAnimator.SetBool("Implode", false);

			if (engineGazeAware.HasGazeFocus) {
				carPart = engine;
				modelCar = true;
				inventoryPanel = false;
			} else if (interiorGazeAware.HasGazeFocus) {
				carPart = interior;
				modelCar = true;
				inventoryPanel = false;
			} else if (bodyFrontGazeAware.HasGazeFocus || bodyRearGazeAware.HasGazeFocus) {
				carPart = bodyFront;
				modelCar = true;
				inventoryPanel = false;
			}
		}

		if (modelCar && carPart!=null) {
			vm.ModelCar (carAnimator, carPart);
			vm.SetButton (closeInventoryButton, true);
			vm.SetButton (blueButton, true);
			vm.SetButton (redButton, true);
			vm.SetButton (blackButton, true);
		}

		//print (temp);

		if (carPart!=null && redButtonGazeAware.HasGazeFocus)
			vm.SetMaterial (carPart, "Red");
		else if (carPart!=null && blueButtonGazeAware.HasGazeFocus)
			vm.SetMaterial (carPart, "Blue");
		else if (carPart!=null && blackButtonGazeAware.HasGazeFocus)
			vm.SetMaterial (carPart, "Black");

		print ("Before Close");
		if (closeInventoryButtonGazeAware.HasGazeFocus) {
			vm.SetButton (blueButton,false);
			vm.SetButton (redButton,false);
			vm.SetButton (blackButton,false);

			if(implode=="Engine")
				carAnimator.SetInteger ("Engine", 0);

		}



	}

	public void SetImplodePart(string value) {
		implode = value;
	}



}
