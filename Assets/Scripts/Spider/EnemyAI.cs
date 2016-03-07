using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	public float patrolSpeed = 2f;                                             
	public float patrolWaitTime = 1f;                       
	public Transform[] patrolWayPoints;
	private GameObject player;
	private SphereCollider collider;

	private NavMeshAgent nav;                                                             
	private float patrolTimer;                              
	private int wayPointIndex;    



	void Awake ()
	{
		nav = GetComponent<NavMeshAgent> ();
		player = GameObject.Find ("Player");
	}


	void Update ()
	{
		if (InSight ())
			Attacking ();
		else 
			Patrolling();
	}

	bool InSight () {
		RaycastHit hit;
		var rayDirection = player.transform.position - transform.position;

		/*if (Physics.Raycast(transform.position + transform.up, rayDirection.normalized, out hit, player)) {
			if(hit.collider.gameObject == player)
			{
				return true;
			}
		}*/
		return false;
	}

	void Attacking() {
		Debug.Log ("ATTACKING");
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