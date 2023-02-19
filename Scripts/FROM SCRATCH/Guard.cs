using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{

	public static event System.Action OnGuardHasSpottedPlayer;

	public float speed = 5;
	public float waitTime = .3f;
	public float turnSpeed = 90;
	public float timeToSpotPlayer = .5f;

	public Light spotlight;
	public float viewDistance;
	public LayerMask viewMask;

	float viewAngle;
	float playerVisibleTimer;

	public GameObject PathGameObject;
	public GameObject enemy;

	[SerializeField]
	public float timeToStopChase = 3f;

	//public Transform pathHolder;
	Transform player;
	Color originalSpotlightColour;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		viewAngle = spotlight.spotAngle;
		originalSpotlightColour = spotlight.color;


	}

	void Update()
	{
		if (CanSeePlayer())
		{
			enemy.GetComponent<PathFinding>().chasing = true;
			timeToStopChase = 9f;
			playerVisibleTimer += Time.deltaTime;
			/*			//Chase player if seen
						foreach (GameObject enemy in PathGameObject.GetComponent<PoV_node_edges>().enemies)
						{
							if(Vector3.Distance(enemy.transform.position, player.position) < viewDistance)
							{
								enemy.GetComponent<PathFinding>().chasing = true;
							}
						}			
						timeToStopChase = 9f;
						playerVisibleTimer += Time.deltaTime;*/
		}
		else
		{
			playerVisibleTimer -= Time.deltaTime;
			timeToStopChase -= Time.deltaTime;
			if(timeToStopChase <= 0)
            {
				enemy.GetComponent<PathFinding>().chasing = false;

				/*foreach (GameObject enemy in PathGameObject.GetComponent<PoV_node_edges>().enemies)
				{
					enemy.GetComponent<PathFinding>().chasing = false;		
				}*/
			}
		}
		playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
		spotlight.color = Color.Lerp(originalSpotlightColour, Color.red, playerVisibleTimer / timeToSpotPlayer);

		if (playerVisibleTimer >= timeToSpotPlayer)
		{
			if (OnGuardHasSpottedPlayer != null)
			{
				OnGuardHasSpottedPlayer();
			}
		}


	}

	bool CanSeePlayer()
	{
		if (Vector3.Distance(transform.position, player.position) < viewDistance)
		{
			Vector3 dirToPlayer = (player.position - transform.position).normalized;
			float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
			if (angleBetweenGuardAndPlayer < viewAngle / 2f)
			{
				if (!Physics.Linecast(transform.position, player.position, viewMask))
				{
					return true;
				}
			}
		}
		return false;
	}

	IEnumerator FollowPath(Vector3[] waypoints)
	{
		transform.position = waypoints[0];

		int targetWaypointIndex = 1;
		Vector3 targetWaypoint = waypoints[targetWaypointIndex];
		transform.LookAt(targetWaypoint);

		while (true)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
			if (transform.position == targetWaypoint)
			{
				targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
				targetWaypoint = waypoints[targetWaypointIndex];
				yield return new WaitForSeconds(waitTime);
				yield return StartCoroutine(TurnToFace(targetWaypoint));
			}
			yield return null;
		}
	}

	IEnumerator TurnToFace(Vector3 lookTarget)
	{
		Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
		float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

		while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
		{
			float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
			transform.eulerAngles = Vector3.up * angle;
			yield return null;
		}
	}
/*
	void OnDrawGizmos()
	{
		Vector3 startPosition = pathHolder.GetChild(0).position;
		Vector3 previousPosition = startPosition;

		foreach (Transform waypoint in pathHolder)
		{
			Gizmos.DrawSphere(waypoint.position, .3f);
			Gizmos.DrawLine(previousPosition, waypoint.position);
			previousPosition = waypoint.position;
		}
		Gizmos.DrawLine(previousPosition, startPosition);

		Gizmos.color = Color.red;
		Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
	}*/

}