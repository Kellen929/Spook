using UnityEngine;
using System.Collections;

public class BulletParticle : MonoBehaviour {
	// Public variables
	public GameObject particleEffect;
	public AudioClip enemyHit;

	// Private variables
	private AudioSource sfx;
	
	// Private variables
	private float lifespan = 3.0f;

	// Constants
	private const int DAMAGE = 1;

	// Use this for initialization
	void Start () {
		AudioSource[] aSources = GameObject.Find("Player").GetComponents<AudioSource>();
		sfx = aSources[1];
	}
	
	// Update is called once per frame
	void Update () {
		lifespan -= Time.deltaTime;

		// Destroys the bullet particle after 3.0 seconds
		if(lifespan <= 0) {
			Destroy(gameObject);
		}
	}

	void OnParticleCollision(GameObject hitThing) {
		if(hitThing.tag == "Enemy")	{
			if(GameObject.Find("EscapeMenu").GetComponent<EscapeMenu>().sfxOn)
				hitSFX();
			hitThing.GetComponent<BulletInteraction>().decreaseHealth(DAMAGE);
			Instantiate(particleEffect, hitThing.transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}

	void hitSFX() {
		sfx.PlayOneShot(enemyHit);
	}
}
