using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariableManager : MonoBehaviour {

	EyeGazeManager e;

	void Awake() {
		e = GameObject.Find ("Managers").GetComponent<EyeGazeManager> ();
	}

	public void SetButton(GameObject button, bool value) {
		button.GetComponent<Image>().enabled = value;
		button.GetComponent<Button>().enabled = value;
		button.GetComponentInChildren<Text>().enabled = value;

	}

	public void ModelCar(Animator a, GameObject carPart) {
		if (carPart.name == "Engine") {
			a.SetInteger ("Engine", 1);
		}
			
		GameObject.Find ("Managers").GetComponent<EyeGazeManager> ().modelCar = false;
	}

	public void SetMaterial(GameObject carPart, string materialColor) {
		Color c = Color.white;
		if (materialColor == "Red")
			c = Color.red;
		else if (materialColor == "Blue")
			c = Color.blue;
		else if (materialColor == "Black")
			c = Color.black;
		
		Renderer r = carPart.GetComponent < Renderer >();
		Material[] originalMaterials = r.materials;
		int length = originalMaterials.Length;

		for (int i = 0; i < length; i++) {
			r.materials [i].color = c;
		}

		e.SetImplodePart (carPart.name);
	}

}
