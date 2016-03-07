using UnityEngine;
using System.Collections;

public class lightScript : MonoBehaviour {

	private bool showingMap = false;
	private Light myLight;

	void Start () {

		myLight = GetComponent<Light> ();
	}


	void Update () {

		if (Input.GetKeyDown(KeyCode.M)) {

			showingMap = !showingMap;
		}

	}

	void open() {

		// yes map -> put away map
		if (showingMap) {

			myLight.enabled = false;
			showingMap = false;

		} 
		// no map -> take out map
		else if (!showingMap) {

			myLight.enabled = true;
			showingMap = true;
		}
	}

}