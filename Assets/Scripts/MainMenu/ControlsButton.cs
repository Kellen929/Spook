using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlsButton : MonoBehaviour {
	public Text controlsText;
	public Canvas mainScreen;
	public Button playGameButton; 
	public Button quitGameButton;
	public Button creditsButton;
	public Button backButton;
	public Button controlsButton;
	public Text creditsText;
	// Use this for initialization
	void Start () {
		hideControls();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void showControls(){
		controlsText.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); 
	}

	public void hideControls() {
		controlsText.transform.localScale = Vector3.zero;
	}

	public void hideMainMenuButtons(){
		// hide the main menu buttons
		playGameButton.transform.localScale = Vector3.zero;
		quitGameButton.transform.localScale = Vector3.zero;
		creditsButton.transform.localScale = Vector3.zero;
		controlsButton.transform.localScale = Vector3.zero;

		backButton.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
	}

	public void showMainMenuButtons(){
		// show the main menu buttons
		playGameButton.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		quitGameButton.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		creditsButton.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		controlsButton.transform.localScale = new Vector3(1.0f,1.0f,1.0f);

		backButton.transform.localScale = Vector3.zero;
	}
}
