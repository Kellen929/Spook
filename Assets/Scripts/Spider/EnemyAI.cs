using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	public Transform[] patrolWayPoints; // The list of patrol points the spider will cycle through
	public int attackDamage = 50;
	public float radiusRange;
	private GameObject player; // The player GameObject
	private SphereCollider spiderCollider; // The sphere collider attached to the spider
	private NavMeshAgent nav; // Used to navigate the spider                                                           
	private float patrolTimer;                               
	private int index = 0; // Index of the array to patrol
	private bool isAttacking; 

	private float minRange;
	private float patrolSpeed = 1f; // How fast the spider will move while patrolling        
	private float engageSpeed = 4f; // How fast the spider will engage
	private float rotationSpeed = 1f; // How fast it takes to rotate to face the player

	void Awake ()
	{
		nav = GetComponent<NavMeshAgent> ();
		player = GameObject.Find ("Player");
		spiderCollider = GetComponent<SphereCollider> ();
		minRange = spiderCollider.radius;
		radiusRange = 2f;
		//patrolWayPoints = (Transform[])GameObject.Find("wayPoints").GetComponents<Transform>();
	}

	void Update ()
	{
		if (InSight ())
			Engaging ();
		else 
			Patrolling();
	}

	bool InSight () {
		RaycastHit hit;
		var rayDirection = player.transform.position - transform.position;

		if (Physics.Raycast(transform.position + transform.up, rayDirection.normalized, out hit, spiderCollider.radius * 2f)) {
			if (hit.collider.gameObject == player) {
				GetComponentInChildren<Animator> ().SetBool ("PlayerInSight", true); // Animate
				return true;
			}  else {
				GetComponentInChildren<Animator> ().SetBool ("PlayerInSight", false);
			}
		}
		return false;
	}

	void Engaging () {
		float distance = Vector3.Distance(transform.position, player.transform.position);

		// Moving towards player
		if (distance > radiusRange) {
			nav.destination = player.transform.position;
			nav.speed = engageSpeed;

			// Rotate towards the player
			transform.rotation = Quaternion.Slerp (transform.rotation,
				Quaternion.LookRotation (player.transform.position - transform.position), rotationSpeed * Time.deltaTime);

			GetComponentInChildren<Animator> ().SetBool ("NextToPlayer", false);
		}  
		// Next to player
		else if (distance <= radiusRange) {

			// Rotate towards the player
			transform.rotation = Quaternion.Slerp (transform.rotation,
				Quaternion.LookRotation (player.transform.position - transform.position), rotationSpeed * Time.deltaTime);
			Attacking ();
		}
	}

	private bool isDamaging = false;
	void Attacking () {
		// Can attack
		if (!isDamaging) {
			isDamaging = true;

			GetComponentInChildren<Animator>().SetBool("NextToPlayer", true);

			Health health = player.GetComponent<Health>();
			health.decreaseHealth(attackDamage);

			StartCoroutine ("DamageCooldown", 1.5f);
		}
	}

	IEnumerator DamageCooldown(float time) {
		yield return new WaitForSeconds (time);
		isDamaging = false;
	}

	void Patrolling ()
	{
		nav.speed = patrolSpeed;
		if(nav.remainingDistance < nav.stoppingDistance)
		{
			index = (index + 1) % 2;
			patrolTimer += Time.deltaTime;
		}

		nav.destination = patrolWayPoints[index + 1].position;
	}
}



