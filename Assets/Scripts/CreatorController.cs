﻿using UnityEngine;
using System.Collections;

public class CreatorController : MonoBehaviour {

	public Trap[] availableObjs;
	public float moveSpeed;
	public int money;
	// Used to get reference to see who is the current player
	private GameController game;
	private int currObj;
	private Transform currObjRenderer;
	private Transform snappedEdge;
	private Vector3 snappedEdgePos;
	private bool canPlace;

	public CreatorHud ui;

	// Use this for initialization
	void Start () {
		game = GameObject.Find ("Game").GetComponent<GameController> ();
		// print (game.player);
		money = 100; // Change when necessary
		currObj = 0;
		currObjRenderer = transform.Find ("currentObj");
		setObjRenderer ();
	}

	// Update is called once per frame
	void Update () {
		if (!ui) {
			ui = GameObject.Find ("CreatorUI").GetComponent<CreatorHud>();
			ui.updateMoneyText(money);
			setObjRenderer ();
		}
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

		Trap thisObj = availableObjs [currObj];
		// Reset the snapped object if out of range of any cubes
		LayerMask platforms = LayerMask.GetMask("Platforms");
		if (snappedEdge == null ||!GetComponent<CircleCollider2D> ().IsTouchingLayers (platforms) || 
				Input.GetButton ("Y_1") || !thisObj.canPlaceOnWalls) {
			// Reset the object snapped to
			snappedEdge = null;
			currObjRenderer.localPosition = new Vector3 (0, 0, 0);
			currObjRenderer.eulerAngles = new Vector3 (0, 0, 0);
		} else {
			/* Update currObjectRenderer's position/rotation on the snapped edge
			 * manually, as this may not be called every frame. This prevents the
			 * object from being shaky when moving. */
			if(snappedEdge != null)
				OnTriggerStay2D (snappedEdge.gameObject.GetComponent<BoxCollider2D> ());
		}
			
		//Check if you can place this object right now
		canPlace = (thisObj.canPlaceInAir || snappedEdge != null);
		//Check if a laser hits something
		if (thisObj.name.Equals ("Laser") && !LaserHead.checkLaser (currObjRenderer.transform.position, 
			currObjRenderer.transform.rotation))
			canPlace = false;
		
		Color color;
		if(canPlace)
			color = new Color(1f, 1f, 1f, 0.7f);
		else
			color = new Color(1f, 0f, 0f, 0.3f);
		currObjRenderer.GetComponent<SpriteRenderer> ().color = color;
	}

	void OnTriggerStay2D(Collider2D other) {
		// Don't snap to the edge if Y is held (debug)
		// TODO Remove this when needed
		if (Input.GetButton ("Y_1"))
			return;

		// Don't snap to an edge if an object cannot do so
		if (!availableObjs [currObj].canPlaceOnWalls)
			return;
		
		// Get the object that was collided with
		GameObject obj = other.gameObject;

		// Forget this object if it is not a platform object
		if (obj.layer != LayerMask.NameToLayer("Platforms"))
			return;

		// Find the closest point within the platform's bounds
		Vector3 objFacing = obj.transform.rotation * Vector3.up;

		Vector3 closestPos = Vector3.zero;
		Vector3 dir = objFacing;
		for (int i = 0; i < 4; i++) {
			RaycastHit2D hit = Physics2D.Raycast (transform.position, dir, 2, LayerMask.GetMask ("Platforms"));
			if (hit.collider != null) {
				closestPos = hit.point;
				break;
			}
			dir = Quaternion.Euler (0f, 0f, 90f) * dir;
		}

		if (closestPos == Vector3.zero) {
			snappedEdge = null;
			return;
		}

		// Calculate that point relative to the Creator
		Vector3 relativePos = closestPos - transform.position;
		float distance = relativePos.magnitude;
		// Forget this object if there is a closer platform than this one
		if (snappedEdge != null && snappedEdge != obj.transform && distance > snappedEdgePos.magnitude)
			return;

		// Update snappedEdge
		snappedEdge = obj.transform;
		snappedEdgePos = relativePos;

		// Update rotation
		Vector3 dirFace = Quaternion.Euler (0f, 0f, 90f) * dir;
		float rotationAngle = Mathf.Atan2 (dirFace.y, dirFace.x) * Mathf.Rad2Deg;
		currObjRenderer.eulerAngles = new Vector3(0, 0, rotationAngle);

		// Set currObjectRenderer's position to the edge's
		currObjRenderer.localPosition = snappedEdgePos;
	}

	private void spawnGameObject()
	{
		if (canPlace && availableObjs [currObj].cost <= money) {
			GameObject spawned = Instantiate (availableObjs [currObj].gameObject);
			spawned.transform.position = currObjRenderer.position;
			spawned.transform.rotation = currObjRenderer.rotation;
			money -= availableObjs [currObj].cost;
			ui.updateMoneyText (money);
			Debug.Log ("Creator has created: " + spawned.name);
			if (spawned.GetComponent<SentryController> ())
				spawned.GetComponent<SentryController> ().enabled = false;

			game.applyGameObject (spawned);
		}
	}

	private void setObjRenderer()
	{
		SpriteRenderer currSelectedSprite = availableObjs [currObj].gameObject.GetComponent<SpriteRenderer> ();
		currObjRenderer.GetComponent<SpriteRenderer>().sprite = currSelectedSprite.sprite;
		Color temp = currObjRenderer.GetComponent<SpriteRenderer> ().color;
		temp.a = 0.7f;
		currObjRenderer.GetComponent<SpriteRenderer> ().color = temp;
		// print (currObj);
		ui.updateObjectPreview (currObj);
	}
}
