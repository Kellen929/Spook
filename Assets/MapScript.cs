using UnityEngine;
using System.Collections;

public class MapScript : MonoBehaviour {
	
	// Public variables
	public bool inProgress = false;
	public bool showingMap = false;
	public bool showingPro = false;
	public bool isPaused = false;
	public Transform mapTrans;
	public AudioSource sfx;
	public AudioClip mapUpSFX;
	public AudioClip mapDownSFX;

	// Private variables
	private Vector3 FINAL_MAP_LOCATION = new Vector3(0, 0, 0.54F);
	private Vector3 START_MAP_LOCATION = new Vector3(0, -.53F, 0.54F);
	private float MOVE_SPEED;

	//camera controlls
	public Light mapLight;
	public Camera mapCam;
	public Camera mainCam;
	public Light tabLight;
	public Light pLight;

	//story text
	public Canvas txt; 

	//start off map inactive
	void Start () {
		gameObject.GetComponent<Renderer>().enabled = false;
		mapCam.enabled = false;
		tabLight.enabled = false;
		pLight.enabled = false;
		MOVE_SPEED = 0.05f;
	}


	void Update () {

		//map
		if (Input.GetKeyDown(KeyCode.M) && !inProgress && !showingPro) {

			showingMap = !showingMap;
			MOVE_SPEED = 0.04f;
			inProgress = true;
			txt.enabled = false;
		}

		//open map
		if (showingMap && inProgress && !showingPro) {

			MOVE_SPEED -= 0.001f;
			gameObject.GetComponent<Renderer>().enabled = true;
			tabLight.enabled = true;
			mapTrans.localPosition = Vector3.MoveTowards (mapTrans.localPosition, FINAL_MAP_LOCATION, MOVE_SPEED);

			//map up
			if (mapTrans.localPosition.y >= FINAL_MAP_LOCATION.y) {
				
				mainCam.farClipPlane = 1;
				mapLight.range = 68;
				sfx.PlayOneShot(mapUpSFX);
				inProgress = false;
				mapCam.enabled = true;
				pLight.enabled = true;
			}

			//close map
		} else if (!showingMap && inProgress && !showingPro) {
			
			MOVE_SPEED -= 0.001f;
			mapTrans.localPosition = Vector3.MoveTowards (mapTrans.localPosition, START_MAP_LOCATION, MOVE_SPEED);
			mapCam.enabled = false;
			pLight.enabled = false;
			mapLight.range = 0;
			mainCam.farClipPlane = 500;

			//map down
			if (mapTrans.localPosition.y <= START_MAP_LOCATION.y) {

				sfx.PlayOneShot(mapDownSFX);
				tabLight.enabled = false;
				gameObject.GetComponent<Renderer>().enabled = false;
				inProgress = false;
			}
		}



		//prompt
		if (Input.GetKeyDown(KeyCode.P) && !inProgress && !showingMap) {
			unPauseGame();
			showingPro = !showingPro;
			MOVE_SPEED = 0.04f;
			inProgress = true;
		}

		//open prompt
		if (showingPro && inProgress &&!showingMap) {
			pauseGame();
			MOVE_SPEED -= 0.001f;
			gameObject.GetComponent<Renderer> ().enabled = true;
			tabLight.enabled = true;
			mapTrans.localPosition = Vector3.MoveTowards (mapTrans.localPosition, FINAL_MAP_LOCATION, MOVE_SPEED);

			//prompt up
			if (mapTrans.localPosition.y >= FINAL_MAP_LOCATION.y) {
				mainCam.farClipPlane = 1;
				mapLight.range = 68;
				sfx.PlayOneShot(mapUpSFX);
				txt.enabled = true;
				inProgress = false;
			}

			//close prompt
		} else if (!showingPro && inProgress && !showingMap) {

			MOVE_SPEED -= 0.001f;
			mapTrans.localPosition = Vector3.MoveTowards (mapTrans.localPosition, START_MAP_LOCATION, MOVE_SPEED);
			mapLight.range = 0;
			txt.enabled = false;
			mainCam.farClipPlane = 500;

			//prompt down
			if (mapTrans.localPosition.y <= START_MAP_LOCATION.y) {

				sfx.PlayOneShot(mapDownSFX);
				tabLight.enabled = false;
				gameObject.GetComponent<Renderer>().enabled = false;
				inProgress = false;
			}
		}

		//change to prompt from map
		if (Input.GetKeyDown (KeyCode.P) && !inProgress && showingMap) {

			mapCam.enabled = false;
			pLight.enabled = false;
			txt.enabled = true;
			showingMap = false;
			showingPro = true;
		}

		//change from map to prompt
		if (Input.GetKeyDown (KeyCode.M) && !inProgress && showingPro) {

			mapCam.enabled = true;
			pLight.enabled = true;
			txt.enabled = false;
			showingMap = true;
			showingPro = false;
		}

	}

	public void setPrompt(bool truth){
		inProgress = truth;
		showingPro = truth;
		mapCam.enabled = false;
		pLight.enabled = false;
		showingMap = false;
		MOVE_SPEED = 0.04f;
	}

	private void pauseGame() {
		isPaused = true;
		Time.timeScale = 0;
	}

	private void unPauseGame() {
		isPaused = false;
		Time.timeScale = 1;
	}
}