using UnityEngine;
using System.Collections;

public class BulletInteraction : MonoBehaviour {
	// Public variables
	public int health = 10;
	public AudioSource sfx;

	// Private variables
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void decreaseHealth(int damage) {
		health -= damage;

		if(health <= 0)
			killSelf();
	}

	private void killSelf() {
	//	sfx.Play();
		Destroy(gameObject);
	}
}
