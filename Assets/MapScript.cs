using UnityEngine;
using System.Collections;

public class MapScript : MonoBehaviour {

	public bool map = false;
	public bool toggle = false;
	public float wait = 0.25f;
	public float moveSpeed = 0.1f;
	Vector3 TargetPosition = new Vector3 (0, -0.5f, 0.5f);

	void Start () {
		
		gameObject.GetComponent<Renderer>().enabled = true;
	}


	void Update () {

		if (Input.GetKeyDown(KeyCode.M)) {
			toggle = true;
		}

		if (toggle) {
			
			open ();
		}
	}



	void open() {

		// yes map -> put away map
		if (map) {
			
			float step = moveSpeed * Time.deltaTime;
			transform.position = Vector3.Translate (transform.position, TargetPosition, step);

			if(transform.position == TargetPosition){
				gameObject.GetComponent<Renderer> ().enabled = false;
				map = false;
				toggle = false;
			}

		} 
		// no map -> take out map
		else if (!map) {

			float step = moveSpeed * Time.deltaTime;
			transform.position = Vector3.Translate (transform.position, -TargetPosition, step);


			if (transform.position == TargetPosition) {
				gameObject.GetComponent<Renderer> ().enabled = true;
				map = true;
				toggle = false;
			}
		}
	}


}
