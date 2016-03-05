using UnityEngine;
using System.Collections;

public class lightScript : MonoBehaviour {

	public bool map = false;
	public float wait = 0.25f;
	private Light myLight;

	void Start () {

		myLight = GetComponent<Light> ();
	}


	void Update () {

		if (Input.GetKeyDown(KeyCode.M)) {

			//MoveTowardsTarget ();
			StartCoroutine(open());
		}
	}

	IEnumerator open() {

		// yes map -> put away map
		if (map) {

			yield return new WaitForSeconds(wait);
			myLight.enabled = false;
			map = false;

		} 
		// no map -> take out map
		else if (!map) {

			yield return new WaitForSeconds(wait);
			myLight.enabled = true;
			map = true;
		}
	}

	//move towards a target at a set speed.
	private void MoveTowardsTarget() {
		//the speed, in units per second, we want to move towards the target
		float speed = 1;
		//move towards the center of the world (or where ever you like)
		Vector3 targetPosition = new Vector3(0, 0.65F, 0);

		Vector3 currentPosition = this.transform.position;
		//first, check to see if we're close enough to the target
		if(Vector3.Distance(currentPosition, targetPosition) > .01f) { 
			Vector3 directionOfTravel = targetPosition - currentPosition;
			//now normalize the direction, since we only want the direction information
			directionOfTravel.Normalize();
			//scale the movement on each axis by the directionOfTravel vector components

			this.transform.Translate(
				(directionOfTravel.x * speed * Time.deltaTime),
				(directionOfTravel.y * speed * Time.deltaTime),
				(directionOfTravel.z * speed * Time.deltaTime),
				Space.World);
		}
	}
}
