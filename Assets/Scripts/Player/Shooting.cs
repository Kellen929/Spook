using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {
	public GameObject bulletPrefab;
	public Camera cam;
	public AudioSource sfx;
	public AudioClip gunshot;
	public AudioClip shellFall;

	private const float bulletImpulse = 35.0f;
	private const float SHELL_DELAY = 0.2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")) {
			sfx.PlayOneShot(gunshot);
			StartCoroutine(PlayShellSFX(SHELL_DELAY));
			GameObject bullet = (GameObject)Instantiate(bulletPrefab, cam.transform.position + cam.transform.forward, cam.transform.rotation * bulletPrefab.transform.rotation);
			bullet.GetComponent<Rigidbody>().AddForce(cam.transform.forward * bulletImpulse, ForceMode.Impulse);
		}
	}

	IEnumerator PlayShellSFX(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		sfx.PlayOneShot(shellFall);
	}
}
