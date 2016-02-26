using UnityEngine;
using System.Collections;

public class BulletParticle : MonoBehaviour {
	private float lifespan = 3.0f;
	public GameObject particleEffect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		lifespan -= Time.deltaTime;

		if(lifespan <= 0)
			Destroy(gameObject);
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Enemy")	{
			Instantiate(particleEffect, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
