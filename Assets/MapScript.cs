using UnityEngine;
using System.Collections;

public class MapScript : MonoBehaviour {

	public bool inProgress = false;
	public bool showingMap = false;
	private Vector3 FINAL_MAP_LOCATION = new Vector3(0, 0, 0.54F);
	private Vector3 START_MAP_LOCATION = new Vector3(0, -.53F, 0.54F);
	private const float MOVE_SPEED = 0.09f;
	public Transform mapTrans;

	//camera controlls
	public Light mapLight;
	public Camera mapCam;
	public Camera mainCam;
	public Light tabLight;
	public Light pLight;

	//start off map inactive
	void Start () {
		gameObject.GetComponent<Renderer>().enabled = false;
		mapCam.enabled = false;
		tabLight.enabled = false;
		pLight.enabled = false;
	}


	void Update () {

		//based on button press
		if (Input.GetKeyDown(KeyCode.M) && !inProgress) {

			showingMap = !showingMap;
			inProgress = true;
		}

		if (showingMap && inProgress) {

			gameObject.GetComponent<Renderer>().enabled = true;
			tabLight.enabled = true;
			mapTrans.localPosition = Vector3.MoveTowards (mapTrans.localPosition, FINAL_MAP_LOCATION, MOVE_SPEED);

			//map up
			if (mapTrans.localPosition.y >= FINAL_MAP_LOCATION.y) {
				mainCam.farClipPlane = 1;
				mapLight.range = 68;
				inProgress = false;
				mapCam.enabled = true;
				pLight.enabled = true;
			}

		} else if (!showingMap && inProgress) {
			
			mapTrans.localPosition = Vector3.MoveTowards (mapTrans.localPosition, START_MAP_LOCATION, MOVE_SPEED);
			mapCam.enabled = false;
			pLight.enabled = false;
			mapLight.range = 0;
			mainCam.farClipPlane = 500;

			//map down
			if (mapTrans.localPosition.y <= START_MAP_LOCATION.y) {

				tabLight.enabled = false;
				gameObject.GetComponent<Renderer>().enabled = false;
				inProgress = false;
			}
		}
	}
}