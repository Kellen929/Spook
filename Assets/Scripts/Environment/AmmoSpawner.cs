using UnityEngine;
using System.Collections;

public class AmmoSpawner : MonoBehaviour {
	// Public variables
	public GameObject AmmoBoxPrefab;
	public Transform[] spawnLocations;

	// Private variables
	private bool ammoBoxOnMap = false;
	private GameObject box;
	private float spawnCountdown;

	// Constants
	private const float AMMO_SPAWN_DELAY = 12.0f;


	// Use this for initialization
	void Start () {
		spawnCountdown = AMMO_SPAWN_DELAY;
	}
	
	// Update is called once per frame
	void Update () {
		spawnCountdown -= Time.deltaTime;

		if(spawnCountdown <= 0) {
			spawnAmmoBox();
			spawnCountdown = AMMO_SPAWN_DELAY;
		}
	}

	private void spawnAmmoBox() {
		// Spawn the ammo box
		box = Instantiate(AmmoBoxPrefab);
		box.transform.position = spawnLocations[Random.Range(0, spawnLocations.Length - 1)].position;
	}

	public void pickupAmmoBox() {
		Destroy(box);
	}
}
