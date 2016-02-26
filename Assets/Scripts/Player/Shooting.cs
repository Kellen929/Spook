using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {
	public GameObject bulletPrefab;
	public Camera cam;

	private const float bulletImpulse = 35.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")) {
			GameObject bullet = (GameObject)Instantiate(bulletPrefab, cam.transform.position + cam.transform.forward, cam.transform.rotation * bulletPrefab.transform.rotation);
			bullet.GetComponent<Rigidbody>().AddForce(cam.transform.forward * bulletImpulse, ForceMode.Impulse);
		}
	}
}
