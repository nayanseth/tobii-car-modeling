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

	GameObject temp, engine, interior, bodyFront, bodyRear;
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
			print("yes");
			vm.SetButton (inventoryButton, false);
			carAnimator.SetBool("Explode", true);
			carAnimator.SetBool("Implode", false);

			if (engineGazeAware.HasGazeFocus) {
				temp = engine;
				modelCar = true;
				inventoryPanel = false;
			} else if (interiorGazeAware.HasGazeFocus) {
				temp = interior;
				modelCar = true;
				inventoryPanel = false;
			} else if (bodyFrontGazeAware.HasGazeFocus || bodyRearGazeAware.HasGazeFocus) {
				temp = bodyFront;
				modelCar = true;
				inventoryPanel = false;
			}
        }

		if (modelCar) {
			vm.ModelCar (carAnimator, temp);
			vm.SetButton (closeInventoryButton, true);
			vm.SetButton (blueButton, true);
			vm.SetButton (redButton, true);
			vm.SetButton (blackButton, true);
		}

		if (redButtonGazeAware.HasGazeFocus)
			vm.SetMaterial (temp, "Red");
		else if (blueButtonGazeAware.HasGazeFocus)
			vm.SetMaterial (temp, "Blue");
		else if (blackButtonGazeAware.HasGazeFocus)
			vm.SetMaterial (temp, "Black");




    
	}

	public void SetImplodePart(string value) {
		implode = value;
	}



}
