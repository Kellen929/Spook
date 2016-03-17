using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {
	public Transform[] points;
	public GameObject enemy; 
	public int waveNumber = 1;
	public int numEnemies = 0; // Current existing enemies
	public int spawnedSoFar = 0; // How many we've spawned thus far
	public int score = 0;
	private bool isOriginal = true;
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
		int index = isOriginal ? 0 : 1;
			
		// Only spawn 3 waves
		if (waveNumber <= 4) {
			GameObject spider = Instantiate (enemy, points [index].position, points [index].rotation) as GameObject;

			EnemyAI enemyAI = spider.GetComponent<EnemyAI> ();
			Debug.Log (enemyAI);

			if (isOriginal) { // Assign this the original way points
				isOriginal = false;
				enemyAI.patrolWayPoints = GameObject.Find("wayPoints").GetComponentsInChildren<Transform>();
				
			} else { // Assign this the new way points
				isOriginal = true;
				enemyAI.patrolWayPoints = GameObject.Find("wayPoints2").GetComponentsInChildren<Transform>();
			}

			numEnemies++;
			spawnedSoFar++;
		} else {
			// All waves are done
			Debug.Log ("Waves are spawned");
		}

		// Once 4 enemies are spawned, it's a new wave
		if (spawnedSoFar % 4 == 0) {
			waveNumber++;
			if (waveNumber <= 4)
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
