using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	public int health = 1000;
	public Slider healthSlider;
	public Image image;
	private bool takingDamage;
	private float damageFlash = 4f;

	// Use this for initialization
	void Start () {
		GameObject canvas = GameObject.Find ("HUDCanvas"); // The HUDCanvas
		GameObject ui = canvas.transform.Find ("HealthUI").gameObject; // The UI object

		if (canvas != null) {
			image = canvas.transform.Find("DamageImage").gameObject.GetComponent<Image>();
		}
		if (ui != null) {
			// This is kinda hacky - maybe there's a better way to do this?
			healthSlider = ui.transform.GetChild(1).gameObject.GetComponent<Slider>();
			healthSlider.value = health;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (takingDamage) {
			image.color = new Color (1f, 0f, 0f, 0.1f);
		} else {
			image.color = Color.Lerp (image.color, Color.clear, damageFlash * Time.deltaTime);
		}
		takingDamage = false;
	}
	public void decreaseHealth(int damage) {
		health -= damage;
		takingDamage = true;
		healthSlider.value = health;
		Debug.Log (health);

		if (health <= 0)
			killSelf ();
	}

	private void killSelf() {
		//End condition for game in defeat
		GameObject.Find("EndGame").GetComponent<EndGame>().GameOver();
	}
}
