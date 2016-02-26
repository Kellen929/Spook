using UnityEngine;
using System.Collections;

public class Lifetime : MonoBehaviour {
	private float lifespan = 3.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		lifespan -= Time.deltaTime;
		if(lifespan <= 0)
			Destroy(gameObject);
	}
}
