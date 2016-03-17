using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {
	public Transform[] points;
	public GameObject enemy; 
	public int waveNumber = 3;
	public int numEnemies = 0; // Current existing enemies
	public int spawnedSoFar = 0; // How many we've spawned thus far
	public int score = 0;
	private Text scoreCount;
	private Text waveCount;

	// Use this for initialization
	void Start () {
		Transform scoreUI = GameObject.Find ("HUDCanvas").transform.GetChild (3);
		scoreCount = scoreUI.Find ("ScoreCount").GetComponent<Text> ();
		waveCount = scoreUI.Find ("WaveCount").GetComponent<Text> ();
		scoreCount.text = "0";
		waveCount.text = "1";
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
			spawnedSoFar++;
		} else {
			// All waves are done
			Debug.Log ("Waves are spawned");
		}

		// Once 4 enemies are spawned, it's a new wave
		if (spawnedSoFar % 4 == 0) {
			waveNumber++;
			waveCount.text = waveNumber.ToString ();
		}

	}

	public void decreaseNumEnemies () {
		numEnemies--;
		score++;
		scoreCount.text = score.ToString ();

		if(numEnemies <= 0 && waveNumber == 3) {
			print("END GAME");
			GameObject.Find("EndGame").GetComponent<EndGame>().WinGame();
		}

		Debug.Log (numEnemies);
	}
}
