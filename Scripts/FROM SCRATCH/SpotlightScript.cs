using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightScript : MonoBehaviour
{
	public static event System.Action OnGuardHasSpottedPlayer;

	public float speed = 5;
	public float waitTime = .3f;
	public float turnSpeed = 90;
	public float timeToSpotPlayer = .5f;
	public GameObject PathGameObject; // set this in the inspector

	public Light spotlight;
	public float viewDistance;
	public LayerMask viewMask;

	float viewAngle;
	float playerVisibleTimer;

	private bool movingLeft;
	private Vector3 dirx = Vector3.left;
	private Vector3 dirz = Vector3.up;

	[SerializeField]
	int posx1, posx2;

	[SerializeField]
	int posz1, posz2;

	//public Transform pathHolder;
	Transform player;
	Color originalSpotlightColour;

	bool caught;
	float caughtTimer = 7f;
	void Start()
	{	
		Vector3 v3 = transform.position;
		movingLeft = true;

		player = GameObject.FindGameObjectWithTag("Player").transform;
		viewAngle = spotlight.spotAngle;
		originalSpotlightColour = spotlight.color;

	}

	void Update()
	{
		if (gameObject.tag.Equals("vertical"))
		{
			transform.Translate(dirz * speed * Time.deltaTime);
			if (transform.position.z >= posz1)
			{
				dirz = Vector3.down;
			}
			else if (transform.position.z <= posz2)
			{
				dirz = Vector3.up;
			}
		}
		if (gameObject.tag.Equals("horizontal"))
		{
			transform.Translate(dirx * speed * Time.deltaTime);
			if (transform.position.x <= posx1)
			{
				dirx = Vector3.left;
			}
			else if (transform.position.x >= posx2)
			{
				dirx = Vector3.right;
			}
		}

        if (CanSeePlayer())
		{
			playerVisibleTimer += Time.deltaTime;
		}
		else
		{
			playerVisibleTimer -= Time.deltaTime;
		}
		playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, timeToSpotPlayer);
		spotlight.color = Color.Lerp(originalSpotlightColour, Color.red, playerVisibleTimer / timeToSpotPlayer);

		if (playerVisibleTimer >= timeToSpotPlayer)
		{
			if(OnGuardHasSpottedPlayer != null)
            {
				caught = true;
				caughtTimer = 7f;	
			}
/*
						if (OnGuardHasSpottedPlayer != null)
						{
							OnGuardHasSpottedPlayer();
						}*/
		}
		if(caught)
        {
			caughtTimer -= Time.deltaTime;
			foreach (GameObject enemy in PathGameObject.GetComponent<PoV_node_edges>().enemies)
			{

				enemy.GetComponent<PathFinding>().chasing = true;
			}
		}
		if(caughtTimer <= 0)
        {
			foreach (GameObject enemy in PathGameObject.GetComponent<PoV_node_edges>().enemies)
			{

				enemy.GetComponent<PathFinding>().chasing = false;
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

}
