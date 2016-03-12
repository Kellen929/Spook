using UnityEngine;
using System.Collections;

public class FlashlightFlicker : MonoBehaviour {
	private float timeUntilNextFlicker;
	private bool isFlickering;
	private float FLASHLIGHT_RANGE_MAX;

	private const float OFF_DURATION = 0.2f;

	// Use this for initialization
	void Start () {
		FLASHLIGHT_RANGE_MAX = gameObject.GetComponent<Light>().range;
		isFlickering = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isFlickering) {
			timeUntilNextFlicker = Random.Range(6.0f, 15.0f);
			isFlickering = true;
		}

		timeUntilNextFlicker -= Time.deltaTime;

		// Start the flicker
		if(timeUntilNextFlicker <= 0) {
			StartCoroutine("turnFlashlightOff", OFF_DURATION);
			StartCoroutine("turnFlashlightOn", OFF_DURATION * 2);
			StartCoroutine("turnFlashlightOff", OFF_DURATION * 2.2f);
			StartCoroutine("turnFlashlightOn", OFF_DURATION * 2.8f);
			isFlickering = false;
		}
	}

	IEnumerator turnFlashlightOff(float time) {
		yield return new WaitForSeconds(time);
		gameObject.GetComponent<Light>().range = 0;
	}

	IEnumerator turnFlashlightOn(float time) {
		yield return new WaitForSeconds(time);
		gameObject.GetComponent<Light>().range = FLASHLIGHT_RANGE_MAX;
	}
}
