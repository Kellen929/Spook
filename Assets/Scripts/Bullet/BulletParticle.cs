using UnityEngine;
using System.Collections;

public class BulletParticle : MonoBehaviour {
	private float lifespan = 3.0f;
	public GameObject particleEffect;
	private float damage = 1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		lifespan -= Time.deltaTime;

		if(lifespan <= 0)
			Destroy(gameObject);
	}

	void OnParticleCollision(GameObject hitThing) {
		if(hitThing.tag == "Enemy")	{
			hitThing.GetComponent<BulletInteraction>().decreaseHealth(damage);
			Instantiate(particleEffect, hitThing.transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
