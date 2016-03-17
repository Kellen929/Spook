using UnityEngine;
using System.Collections;

public class ClickToContinue : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Cursor.visible = true;
		Screen.lockCursor = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void goToMainMenu() {
		Application.LoadLevel("menu");
	}
}

