using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public Transform[] points;
	public GameObject enemy; 
	public int waveNumber = 3;
	public int numEnemies = 0;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", 1, 10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Spawn () {
		int index = Random.Range (0, points.Length); // Randomly spawn at one of two locations
		// Only spawn 3 waves
		if (waveNumber < 3) {
			Instantiate (enemy, points [index].position, points [index].rotation);
			numEnemies++;
		} else {
			// All waves are done
			Debug.Log ("Waves are spawned");
		}

		// Once 4 enemies are spawned, it's a new wave
		if (numEnemies % 4 == 0) {
			waveNumber++;
		}

	}

	public void decreaseNumEnemies () {
		numEnemies--;
		print("num enemies: " + numEnemies);
		print("wave: " + waveNumber);

		if(numEnemies <= 0 && waveNumber == 3) {
			print("END GAME");
			GameObject.Find("EndGame").GetComponent<EndGame>().WinGame();
		}

		Debug.Log (numEnemies);
	}
}
