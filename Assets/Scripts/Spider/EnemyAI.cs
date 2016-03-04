using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	public float patrolSpeed = 2f;                                             
	public float patrolWaitTime = 1f;                       
	public Transform[] patrolWayPoints;                    

	private NavMeshAgent nav;                                                             
	private float patrolTimer;                              
	private int wayPointIndex;                              


	void Awake ()
	{
		nav = GetComponent<NavMeshAgent> ();
	}


	void Update ()
	{
		Patrolling();
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