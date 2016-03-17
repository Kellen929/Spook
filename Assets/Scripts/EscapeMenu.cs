using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour {

	public Canvas resumeCanvas;
	public GameObject playerGameObj;
	public Button resumeGameButton;
	public Button quitButton;

	// Sound toggles
	public bool sfxOn = true;
	private bool musicOn = true;
	public AudioSource music;

	private bool isPaused = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			isPaused = !isPaused;
			playerGameObj.GetComponent<FirstPersonController>().togglePaused();
			playerGameObj.GetComponent<Shooting>().togglePaused();
		}

		if (isPaused) {
			resumeCanvas.gameObject.SetActive(true);
			Time.timeScale = 0;
			Cursor.visible = true;
			Screen.lockCursor = false;
		} else if (!isPaused) {
			resumeCanvas.gameObject.SetActive(false);
			Cursor.visible = false;
			Screen.lockCursor = true;
			Time.timeScale = 1;
		}
	}

	public void resumeGame() {
		isPaused = !isPaused;
		playerGameObj.GetComponent<FirstPersonController>().togglePaused();
		playerGameObj.GetComponent<Shooting>().togglePaused();
	}

	public void quitGame() {
		Application.Quit();
	}

	public void toggleMusic() {
		musicOn = !musicOn;
		if(musicOn)
			music.UnPause();
		else
			music.Pause();
	}

	public void toggleSFX() {
		sfxOn = !sfxOn;
	}
}
