using UnityEngine;
using System.Collections;

public class AmmoSpawner : MonoBehaviour {
	// Public variables
	public GameObject AmmoBoxPrefab;
	public Transform[] spawnLocations;

	// Private variables
	private bool ammoBoxOnMap = false;
	private GameObject box;

	// Constants
	private const float AMMO_SPAWN_DELAY = 10.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!ammoBoxOnMap) {
			StartCoroutine("spawnAmmoBox", AMMO_SPAWN_DELAY);
			ammoBoxOnMap = true;
		}
	}

	IEnumerator spawnAmmoBox(float time) {
		yield return new WaitForSeconds(time);

		// Spawn the ammo box
		box = Instantiate(AmmoBoxPrefab);
		box.transform.position = spawnLocations[Random.Range(0, 3)].position;
	}

	public void pickupAmmoBox() {
		Destroy(box);
		ammoBoxOnMap = false;
	}
}
