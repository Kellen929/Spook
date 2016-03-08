using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public Transform[] points;
	public GameObject enemy;

	// Use this for initialization
	void Start () {
		//InvokeRepeating ("Spawn", 360f, 360f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Spawn () {
		int index = Random.Range (0, points.Length);

		Instantiate (enemy, points [index].position, points [index].rotation);
	}
}
