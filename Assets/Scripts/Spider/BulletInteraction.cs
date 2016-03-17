using UnityEngine;
using System.Collections;

public class BulletInteraction : MonoBehaviour {
	// Public variables
	public int health = 10;
	public GameObject spiderDieParticle;

	// Private variables
	private GameObject spawner;
	private AudioSource sfx;

	// Use this for initialization
	void Start () {
		spawner = GameObject.Find("EnemySpawner");
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
		// This call can be used to decrease number of enemies
		spawner = GameObject.Find("EnemySpawner");
		spawner.GetComponent<EnemySpawner>().decreaseNumEnemies();
		if(GameObject.Find("EscapeMenu").GetComponent<EscapeMenu>().sfxOn) {
			sfx = GameObject.Find("SpiderSFX").GetComponent<AudioSource>();
			sfx.Play();
		}
		// Particle effect spider death call
		spiderDieParticle.transform.position = transform.position;
		Instantiate(spiderDieParticle);
		Destroy(gameObject);
	}
}
