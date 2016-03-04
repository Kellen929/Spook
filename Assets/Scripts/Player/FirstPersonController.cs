using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour {
	// Public Variables
	public CharacterController playerController;
	public Camera mainCamera;
	public Light spotLight;
	
	public AudioSource sfx;
	public AudioClip step1SFX;
	public AudioClip step2SFX;
	public AudioClip step3SFX;
	public AudioClip step4SFX;
	public AudioClip jumpSFX;
	public AudioClip landSFX;

	// Private Variables
	private float verticalRotation = 0;
	private Vector3 jumpVector = Vector3.zero;
	private bool isCurrentlyCrouching = false;
	private bool crouchState = false;
	private bool isFalling = false;
	private int stepIdx = 0;

	// Private Constants
	private const float MOVE_FASTER_MULTIPLIER = 6.0f;
	private const float MOUSE_SENSITIVITY = 3.0f;
	private const float PITCH_RANGE = 65.0f;
	private const float JUMP_FORCE = 7.0f;
	private const float GRAVITY = 8.0f;
	private const float CROUCH_SPEED = .1f;
	private const float MAX_CAMERA = 1.75f;
	private const float MIN_CAMERA = 0.85f;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		// Rotation
		transform.Rotate(0, Input.GetAxis("Mouse X") * MOUSE_SENSITIVITY, 0);

		verticalRotation -= Input.GetAxis("Mouse Y") * MOUSE_SENSITIVITY;
		verticalRotation = Mathf.Clamp(verticalRotation, -PITCH_RANGE, PITCH_RANGE);
		mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
		spotLight.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

		
		// Jumping
		if (playerController.isGrounded) {
			if(isFalling) {
				sfx.PlayOneShot(landSFX);
				isFalling = false;
			}
			if (Input.GetButton("Jump")) {
				sfx.PlayOneShot(jumpSFX);
				jumpVector.y = JUMP_FORCE;
				isFalling = true;
			}
		}

		jumpVector.y -= GRAVITY * Time.deltaTime;
		playerController.Move(jumpVector * Time.deltaTime);


		// Movement	
		float forwardSpeed = Input.GetAxis("Vertical");
		float sideSpeed = Input.GetAxis("Horizontal");

		Vector3 velocity = new Vector3(sideSpeed * MOVE_FASTER_MULTIPLIER, 0, forwardSpeed * MOVE_FASTER_MULTIPLIER);
		velocity = transform.rotation * velocity;
		playerController.SimpleMove(velocity);

		// Movement SFX
		if(velocity != Vector3.zero) {
			if(!sfx.isPlaying) {
				switch(stepIdx) {
					case 0:
						sfx.PlayOneShot(step1SFX);
						break;
					case 1:
						sfx.PlayOneShot(step2SFX);
						break;
					case 2:
						sfx.PlayOneShot(step3SFX);
						break;
					case 3:
						sfx.PlayOneShot(step4SFX);
						break;
				}
				stepIdx = stepIdx >= 3 ? 0 : stepIdx + 1; 
			}
		}

		// Crouching
		if (playerController.isGrounded && Input.GetKeyDown(KeyCode.C)) {
			isCurrentlyCrouching = !isCurrentlyCrouching;
			crouchState = true;
		}

		if (crouchState && isCurrentlyCrouching && mainCamera.transform.position.y > MIN_CAMERA) {
			Vector3 tmpCamPos = new Vector3 (mainCamera.transform.position.x, mainCamera.transform.position.y - CROUCH_SPEED, mainCamera.transform.position.z);
			mainCamera.transform.position = tmpCamPos;
			Vector3 tmpLightPos = new Vector3 (spotLight.transform.position.x, spotLight.transform.position.y - CROUCH_SPEED, spotLight.transform.position.z);
			spotLight.transform.position = tmpLightPos;

			// Crouching means quieter movement
			sfx.volume = 0.2f;
		}
		else if (crouchState && !isCurrentlyCrouching && mainCamera.transform.position.y < MAX_CAMERA) {
			Vector3 tmpCamPos = new Vector3 (mainCamera.transform.position.x, mainCamera.transform.position.y + CROUCH_SPEED, mainCamera.transform.position.z);
			mainCamera.transform.position = tmpCamPos;
			Vector3 tmpLightPos = new Vector3 (spotLight.transform.position.x, spotLight.transform.position.y + CROUCH_SPEED, spotLight.transform.position.z);
			spotLight.transform.position = tmpLightPos;

			// Standing means louder movement
			sfx.volume = 0.5f;
		}
		else if (mainCamera.transform.position.y <= MIN_CAMERA || mainCamera.transform.position.y >= MAX_CAMERA) {
			crouchState = false;
		}
	}
}
