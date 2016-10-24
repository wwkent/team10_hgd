using UnityEngine;
using System.Collections;

public class CreatorController : MonoBehaviour {

	public GameObject[] availableObjs;
	public float moveSpeed;
	// Used to get reference to see who is the current player
	private GameController game;
	private int currObj;
	private Transform currObjRenderer;
	public Transform snappedEdge;
	public Vector3 snappedEdgePos;
	public int snappedEdgeSide;


	// Use this for initialization
	void Start () {
		// game = GameObject.Find ("UI").GetComponent<GameController> ();
		// print (game.player);
		currObj = 0;
		currObjRenderer = transform.Find ("currentObj");
		setObjRenderer ();
	}
	
	// Update is called once per frame
	void Update () {
		float inputXAmount = Input.GetAxis ("L_XAxis_1");
		float inputYAmount = Input.GetAxis ("L_YAxis_1");

		if (Input.GetButtonDown ("A_1"))
			spawnGameObject ();

		if (Input.GetButtonDown ("RB_1")) {
			if (currObj < availableObjs.Length - 1)
				currObj++;
			else
				currObj = 0;
			setObjRenderer ();
		}
		if (Input.GetButtonDown ("LB_1")) {
			if (currObj > 0)
				currObj--;
			else
				currObj = availableObjs.Length - 1;
			setObjRenderer ();
		}
		// Calculate how much the velocity should change based on xAccel
		Vector3 direction = new Vector3 (inputXAmount, -inputYAmount, 0.0f);
		transform.Translate (moveSpeed * direction * Time.deltaTime);

		GetComponent<CircleCollider2D> ().attachedRigidbody.WakeUp();

		// Reset the snapped object if out of range of any cubes
		LayerMask platforms = LayerMask.GetMask("Platforms");
		if (!GetComponent<CircleCollider2D> ().IsTouchingLayers (platforms) || Input.GetButton ("Y_1")) {
			// Reset the object snapped to
			snappedEdge = null;
			currObjRenderer.localPosition = new Vector3 (0, 0, 0);
			currObjRenderer.eulerAngles = new Vector3 (0, 0, 0);
			snappedEdgeSide = 0;
		} else {
			/* Update currObjectRenderer's position/rotation on the snapped edge
			 * manually, as this may not be called every frame. This prevents the
			 * object from being shaky when moving. */
			OnTriggerStay2D (snappedEdge.gameObject.GetComponent<BoxCollider2D> ());
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		// Don't snap to the edge if Y is held (debug)
		// TODO Remove this when needed
		if (Input.GetButton ("Y_1"))
			return;

		// Get the object that was collided with
		GameObject obj = other.gameObject;

		// Forget this object if it is not a platform object
		if (obj.layer != LayerMask.NameToLayer("Platforms"))
			return;

		// Find the closest point within the platform's bounds
		Vector3 closestPos = other.bounds.ClosestPoint (transform.position);
		// Calculate that point relative to the Creator
		Vector3 relativePos = closestPos - transform.position;
		float distance = relativePos.magnitude;
		// Forget this object if there is a closer platform than this one
		if (snappedEdge != null && snappedEdge != obj.transform && distance > snappedEdgePos.magnitude)
			return;

		// Update snappedEdge
		snappedEdge = obj.transform;
		snappedEdgePos = relativePos;

		// Woah nelly that's a lot of math
		float rotation;
		float objX = obj.transform.position.x;
		float objY = obj.transform.position.y;
		float objWidth = other.bounds.size.x;
		float objHeight = other.bounds.size.y;
		float xDist = transform.position.x - objX;
		float yDist = transform.position.y - objY;
		float xDistFromEdge = Mathf.Abs (xDist) - (objWidth / 2f);
		float yDistFromEdge = Mathf.Abs (yDist) - (objHeight / 2f);

		// Find which side of the platform you are on
		if (xDistFromEdge >= yDistFromEdge) {
			if (xDist >= 0) {
				snappedEdgeSide = 1; // Right side
				rotation = 270f;
			} else {
				snappedEdgeSide = 3; // Left side
				rotation = 90f;
			}
		} else {
			if (yDist >= 0) {
				snappedEdgeSide = 0; // Top side
				rotation = 0f;
			} else {
				snappedEdgeSide = 2; // Bottom side
				rotation = 180f;
			}
		}
		// Update rotation
		currObjRenderer.eulerAngles = new Vector3 (0, 0, rotation);
		
		// Calculate the amount to push the object out from the edge
		Vector3 boundsOffset = new Vector3 (0, 0, 0);
		Bounds currObjBounds = currObjRenderer.GetComponent<SpriteRenderer> ().bounds;
		switch (snappedEdgeSide) {
		case 0:
			boundsOffset = new Vector3 (0, currObjBounds.size.y / 2f, 0);
			break;
		case 1:
			boundsOffset = new Vector3 (currObjBounds.size.x / 2f, 0, 0);
			break;
		case 2:
			boundsOffset = new Vector3 (0, -currObjBounds.size.y / 2f, 0);
			break;
		case 3:
			boundsOffset = new Vector3 (-currObjBounds.size.x / 2f, 0, 0);
			break;
		}

		// Set currObjectRenderer's position to the edge's
		currObjRenderer.localPosition = snappedEdgePos + boundsOffset;
	}

	private void spawnGameObject()
	{
		GameObject spawned = Instantiate (availableObjs [currObj]);
		spawned.transform.position = currObjRenderer.position;
		spawned.transform.rotation = currObjRenderer.rotation;
		spawned.GetComponent<SentryController>().enabled = false;
		Debug.Log ("Creator has created: " + spawned.name);
	}

	private void setObjRenderer()
	{
		SpriteRenderer currSelectedSprite = availableObjs [currObj].GetComponent<SpriteRenderer> ();
		currObjRenderer.GetComponent<SpriteRenderer>().sprite = currSelectedSprite.sprite;
		Color temp = currObjRenderer.GetComponent<SpriteRenderer> ().color;
		temp.a = 0.7f;
		currObjRenderer.GetComponent<SpriteRenderer> ().color = temp;
	}
}
