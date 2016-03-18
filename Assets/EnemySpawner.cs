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

	public GameObject tablet;
	public Text story;


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
		txt ();
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
			waveCount.text = waveNumber.ToString ();
			tablet.GetComponent<MapScript> ().setPrompt (true);
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

	public void txt(){

		//tablet.inProgress = true;
		//tablet.showingPro = true;
		switch (waveNumber) {

		case 1:
			story.text = "Why did the nurse leave me a gun next to my bed? Where am I? What happened to me? " +
				"What happened here? Is my wife ok? Vanessa? Jamie? I miss my little princesses.\n(Press 'p' to close)";
			break;
		case 2:
			story.text = "Oww my legs. \nOhhh yeah... A car hit me. Last I saw of my darling Audrey was next to my stretcher. " +
				"But where is she now? Where is everyone? How long have I been here? I need to find my daughters. And whats with all these hostile spiders? \n(Press 'p' to close)";
			break; 
		case 3:
			story.text = "I remember hearing a nurse playing with them. Hopefully she's taking care of them. " +
				"I hope none of them are hurt. I need to find them now!\n(Press 'p' to close)";
			break;
		case 4:
			story.color = Color.white;
			story.fontSize = 16;
			story.text = "From: St. Capoli Emergency Notification\nTo:undisclosed-recipients" +
				"\n\nPossible virus outbreak seen at approx. 0.19:42 hours near 14th floor at rm. 1408." +
				"\nLast seen spreading to the 8th floor and 16th floor." +
				"\nRefer to HPD website for precautions to take.(Press 'p' to close)";
			break;
		case 5:
			story.color = Color.green;
			story.fontSize = 20;
			story.text = "Arg. I can barely bend my leg. That driver looked a bit strange before he hit me.\n(Press 'p' to close)";
			break;
		case 6:
			story.text = "This place feels like a maze, I feel like im walking in circles. " +
				"How am i going to find my family. I pray that they are ok.\n(Press 'p' to close)";
			break;
		case 7:
			story.text = "Maybe there is a secret exit somewhere.\n(Press 'p' to close)";
			break;
		case 9:
			story.text = "This is impossible, my legs are about to give out. I can't stand this pain anymore!\n(Press 'p' to close)";
			break;

		default:
			story.text = "Story time...\n(Press 'p' to close)";
			break;
		}
	}
}
