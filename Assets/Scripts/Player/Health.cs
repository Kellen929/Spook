using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	public int health = 2000;
	public Slider healthSlider;

	// Use this for initialization
	void Start () {
		GameObject canvas = GameObject.Find ("HUDCanvas"); // The HUDCanvas
		GameObject ui = canvas.transform.Find ("HealthUI").gameObject; // The UI object

		if (ui != null) {
			// This is kinda hacky - maybe there's a better way to do this?
			healthSlider = ui.transform.GetChild(1).gameObject.GetComponent<Slider>();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void decreaseHealth(int damage) {
		health -= damage;

		healthSlider.value = health;

		if (health <= 0)
			killSelf ();
	}

	private void killSelf() {
		//End condition for game in defeat
		GameObject.Find("EndGame").GetComponent<EndGame>().GameOver();
	}
}
