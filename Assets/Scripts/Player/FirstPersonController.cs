using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {
	// Public Variables
	public CharacterController playerController;
	public Camera mainCamera;
	public Light spotLight;

	// Private Variables
	private float verticalRotation = 0;

	// Private Constants
	private const float MOVE_FASTER_MULTIPLIER = 6.0f;
	private const float MOUSE_SENSITIVITY = 3.0f;
	private const float PITCH_RANGE = 65.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Rotation
		transform.Rotate(0, Input.GetAxis("Mouse X") * MOUSE_SENSITIVITY, 0);

		verticalRotation -= Input.GetAxis("Mouse Y") * MOUSE_SENSITIVITY;
		verticalRotation = Mathf.Clamp(verticalRotation, -PITCH_RANGE, PITCH_RANGE);
		mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
		spotLight.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

		// Movement	
		float forwardSpeed = Input.GetAxis("Vertical");
		float sideSpeed = Input.GetAxis("Horizontal");

		Vector3 velocity = new Vector3(sideSpeed * MOVE_FASTER_MULTIPLIER, 0, forwardSpeed * MOVE_FASTER_MULTIPLIER);
		velocity = transform.rotation * velocity;
		playerController.SimpleMove(velocity);
	}
}
