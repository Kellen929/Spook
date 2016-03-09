using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public int health = 150;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void decreaseHealth(int damage) {
		health -= damage;

		if (health <= 0)
			killSelf ();
	}

	private void killSelf() {
		//End condition for game in defeat
		Application.Quit ();
	}
}
