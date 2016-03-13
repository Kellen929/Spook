using UnityEngine;
using System.Collections;

public class AmmoBox : MonoBehaviour {
	private const int AMMO_IN_BOX = 30;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Transform ffsPlayerTrans = GameObject.Find("Player").transform;

		if(Mathf.Abs(ffsPlayerTrans.position.x - transform.position.x) < 1 && Mathf.Abs(ffsPlayerTrans.position.z - transform.position.z) < 1)
			FORCECOLLIDE();
	}

	void FORCECOLLIDE() {
		GameObject.Find("AmmoSpawner").GetComponent<AudioSource>().Play();
		GameObject.Find("Player").GetComponent<Shooting>().updateAmmo(AMMO_IN_BOX);
		GameObject.Find("AmmoSpawner").GetComponent<AmmoSpawner>().pickupAmmoBox();
	}
}
