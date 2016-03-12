using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	// Public Variables
	public GameObject bulletPrefab;
	public Camera cam;
	public Transform slideTrans;
	public Transform triggerTrans;
	public Transform bodyTrans;
	public ParticleSystem muzzleFlash;
	public GameObject gun;
	public bool isPaused;

	// Audio
	public AudioClip gunshot;
	public AudioClip shellFall;
	public AudioClip clickSFX;
	public AudioSource sfx;

	private const float bulletImpulse = 35.0f;

	private const float SHELL_DELAY = 0.2f;
	private const float STATIC_SLIDE_Y = 3.1473f;
	private const float EXTENDED_SLIDE_Y = -23.0f;
	private const float SLIDE_SPEED = 13f;

	private       Vector3 STATIC_BODY_POS;
	private       Vector3 EXTENDED_BODY_POS;
	private const float BODY_SPEED = 0.4f;

	// Private variables
	private bool animating = false;
	private bool slideReturn = false;
	private bool slideExpand = false;
	private bool bodyReturn = false;
	private bool bodyExpand = false;
	private bool slideDone = false;
	private bool bodyDone = false;
	private int ammoCount = 45;

	// Use this for initialization
	void Start () {
		STATIC_BODY_POS = bodyTrans.localPosition;
		EXTENDED_BODY_POS = new Vector3(bodyTrans.localPosition.x + 0.5f,
										bodyTrans.localPosition.y - 0.2f,
										bodyTrans.localPosition.z + 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if(!isPaused) {
			if(Input.GetButtonDown("Fire1")) {
				if(ammoCount > 0) {
					ammoCount--;
					animating = true;
					slideExpand = true;
					bodyExpand = true;
					PlayMuzzleParticle();
					sfx.PlayOneShot(gunshot);
					StartCoroutine(PlayShellSFX(SHELL_DELAY));
					// REMOVED BULLET
					//GameObject bullet = (GameObject)Instantiate(bulletPrefab, cam.transform.position + cam.transform.forward, cam.transform.rotation * bulletPrefab.transform.rotation);
					//bullet.GetComponent<Rigidbody>().AddForce(cam.transform.forward * bulletImpulse, ForceMode.Impulse);
				}
				else {
					sfx.PlayOneShot(clickSFX);
				}
			}

			// Gun Animation
			if(animating) {
				if(slideDone && bodyDone) {
					animating = false;
					slideDone = false;
					bodyDone = false;
				}

				// Slide
				if(slideExpand && slideTrans.localPosition.y > EXTENDED_SLIDE_Y) {
					Vector3 tmp = slideTrans.localPosition;
					tmp.y -= SLIDE_SPEED;
					slideTrans.localPosition = tmp;
					
					// If just got fully extended, go back
					if(slideTrans.localPosition.y <= EXTENDED_SLIDE_Y) {
						slideReturn = true;
						slideExpand = false;
					}
				}
				else if(slideReturn && slideTrans.localPosition.y < STATIC_SLIDE_Y) {
					Vector3 tmp = slideTrans.localPosition;
					tmp.y += SLIDE_SPEED;
					slideTrans.localPosition = tmp;

					// If finished returning slide to original static state
					if(slideTrans.localPosition.y >= STATIC_SLIDE_Y) {
						slideReturn = false;
						slideDone = true;
					}
				}

				// Body
				if(bodyExpand) {
					bodyTrans.localPosition = Vector3.MoveTowards(bodyTrans.localPosition, EXTENDED_BODY_POS, BODY_SPEED);

					// If just got fully extended, go back
					if(bodyTrans.localPosition.Equals(EXTENDED_BODY_POS)) {
						bodyReturn = true;
						bodyExpand = false;
					}
				}
				else if(bodyReturn) {
					bodyTrans.localPosition = Vector3.MoveTowards(bodyTrans.localPosition, STATIC_BODY_POS, BODY_SPEED);

					// If finished returning slide to original static state
					if(bodyTrans.localPosition.Equals(STATIC_BODY_POS)) {
						bodyReturn = false;
						bodyDone = true;
					}
				}
			}
		}
	}

	IEnumerator PlayShellSFX(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		sfx.PlayOneShot(shellFall);
	}

	void PlayMuzzleParticle() {
		ParticleSystem tmpFlash = (ParticleSystem)(Instantiate(muzzleFlash, gun.transform.position, gun.transform.rotation));
		tmpFlash.transform.parent = gun.transform;
		tmpFlash.transform.localPosition = new Vector3(0.1000208f, 232.3f, 46.6f);
		tmpFlash.transform.localRotation = Quaternion.Euler(new Vector3(-90, 90, 0));
		tmpFlash.transform.localScale = new Vector3(1, 1, 1);
		tmpFlash.Stop();
		tmpFlash.Play();
		StartCoroutine(DestroyBulletFlash(2.0f, tmpFlash.gameObject));
	}

	IEnumerator DestroyBulletFlash(float waitTime, GameObject bulletFlash) {
		yield return new WaitForSeconds(waitTime);
		Destroy(bulletFlash);
	}

	public void togglePaused() {
		isPaused = !isPaused;
	}

	public void updateAmmo(int byThisMany) {
		ammoCount += byThisMany;
	}

	public int getAmmoCount() {
		return ammoCount;
	}
}
