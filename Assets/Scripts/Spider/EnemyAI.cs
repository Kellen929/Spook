using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	public float patrolSpeed = 1f;                                             
	public float patrolWaitTime = 1f;                       
	public Transform[] patrolWayPoints;
	private GameObject player;
	private SphereCollider spiderCollider;
	private float minRange;

	private NavMeshAgent nav;                                                             
	private float patrolTimer;                              
	private int wayPointIndex;    



	void Awake ()
	{
		nav = GetComponent<NavMeshAgent> ();
		player = GameObject.Find ("Player");
		spiderCollider = GetComponent<SphereCollider> ();
		minRange = spiderCollider.radius - 1;
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
				GetComponentInChildren<Animator> ().SetBool ("PlayerInSight", true);
				return true;
			} else {
				GetComponentInChildren<Animator> ().SetBool ("PlayerInSight", false);
			}
		}
		return false;
	}

	void Engaging () {
		float distance = Vector3.Distance(transform.position, player.transform.position);

		// Moving towards player
		if (distance > minRange) {
			nav.destination = player.transform.position;
			GetComponentInChildren<Animator> ().SetBool ("NextToPlayer", false);
		} 
		// Next to player
		else {
			Attacking ();
		}
	}

	void Attacking () {
		GetComponentInChildren<Animator>().SetBool("NextToPlayer", true);
		Health health = player.GetComponent<Health> ();
		health.decreaseHealth (1);
	}

	void OnParticleCollision(GameObject hitThing) {
		if(hitThing.tag == "Player")	{
			hitThing.GetComponent<Health>().decreaseHealth(1);
			Destroy(gameObject);
		}
	}

	void Patrolling ()
	{
		nav.speed = patrolSpeed;
			
		if(nav.remainingDistance < nav.stoppingDistance)
		{
			patrolTimer += Time.deltaTime;
			if(patrolTimer >= patrolWaitTime)
			{
				if(wayPointIndex == patrolWayPoints.Length - 1)
					wayPointIndex = 0;
				else
					wayPointIndex++;

				patrolTimer = 0;
			}
		}
		else
			patrolTimer = 0;

		nav.destination = patrolWayPoints[wayPointIndex].position;
	}
}