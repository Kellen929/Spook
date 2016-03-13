using UnityEngine;
using System.Collections;

public class ResumeGameButton : MonoBehaviour {

	public Canvas resumeCanvas;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ResumeGame() {
		resumeCanvas.gameObject.SetActive(false);
		Cursor.visible = false;
		Screen.lockCursor = true;
		Time.timeScale = 1;
	}
}
