using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreditsButton : MonoBehaviour {

	public Canvas mainScreen;
	public Button playGameButton; 
	public Button quitGameButton;
	public Button creditsButton;
	public Button backButton;
	public Text creditsText;
	// Use this for initialization

	void Start () {
		hideCredits();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void showCredits() {
		// show the credit text
		creditsText.transform.localScale = new Vector3(1.0f,1.0f,1.0f);

	}

	public void hideCredits() {
		// hide the credit text
		backButton.transform.localScale = Vector3.zero;
		creditsText.transform.localScale = Vector3.zero;

	}

	public void showMainMenuButtons(){
		// show the main menu buttons
		playGameButton.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		quitGameButton.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		creditsButton.transform.localScale = new Vector3(1.0f,1.0f,1.0f);

		backButton.transform.localScale = Vector3.zero;
	}

	public void hideMainMenuButtons(){
		// hide the main menu buttons
		playGameButton.transform.localScale = Vector3.zero;
		quitGameButton.transform.localScale = Vector3.zero;
		creditsButton.transform.localScale = Vector3.zero;

		backButton.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
	}
}
