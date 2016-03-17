using UnityEngine;
using System.Collections;

public class DoorOpenClose : MonoBehaviour {
	// Public variables
	public Transform doorTransform;
	public Transform playerTransform;
	public AudioSource doorSFX;

	// Private variables
	private bool doorOpen = false, opening = false;
	private int count = 0;
	private int dist = 8;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		bool playerCloseEnough = closeEnough();

		if (Input.GetKeyDown(KeyCode.E) && !doorOpen && playerCloseEnough)  {
			opening = true;
			 if(GameObject.Find("EscapeMenu").GetComponent<EscapeMenu>().sfxOn)
				doorSFX.Play();
		}
		else if (Input.GetKeyDown(KeyCode.E) && doorOpen && playerCloseEnough) {
			opening = false;
			 if(GameObject.Find("EscapeMenu").GetComponent<EscapeMenu>().sfxOn)
				doorSFX.Play();
		}

		if (opening && !doorOpen) {
			doorTransform.Rotate (new Vector3 (0, (doorTransform.rotation.y - 0.5f), 0)); 

			if (doorTransform.localRotation.y >= 0.98f) {
				doorOpen = true;
			}
		} else if (!opening && doorOpen) {
			doorTransform.Rotate (new Vector3 (0, -(doorTransform.rotation.y - 0.5f), 0)); 

			if (doorTransform.localRotation.y <= 0.72f) {
				doorOpen = false;
			}
		}
	}

	private bool closeEnough() {
		float playerx = playerTransform.localPosition.x;
		float playerz = playerTransform.localPosition.z;
		float doorx = doorTransform.localPosition.x;
		float doorz = doorTransform.localPosition.z;

		double xDistAway = Mathf.Abs (doorx - playerx);
		double zDistAway = Mathf.Abs (doorz - playerz);

		if (xDistAway <= dist && zDistAway <= dist) {
			return true;
		} else {
			return false;
		}
	}
}