using UnityEngine;
using System.Collections;


public class PlayGameButtonClick : MonoBehaviour {
	AsyncOperation levelAsync;

	// Use this for initialization
	void Start () {
		//levelAsync = Application.LoadLevelAdditiveAsync("leve1");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadGame() {
		Application.LoadLevel("level1");
	}
}
