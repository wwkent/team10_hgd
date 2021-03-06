﻿using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

	// For movement control
	public float maxSpeed = 10f;
	public float jumpForce = 700f;
	bool facingRight = true;

	// References
	Animator anim; // This is currently not used because I do not have animations yet
	Rigidbody2D rBody;

	// For Ground collision
	bool onGround = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (onGround && Input.GetButtonDown("A_1")) {
			rBody.AddForce (new Vector2 (0f, jumpForce));
			onGround = false;
		}
	}

	void FixedUpdate () {
		onGround = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		// This might be a better option than the above depending on how the math works
		// Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		float inputDirection = Input.GetAxis ("L_XAxis_1");

		rBody.velocity = new Vector2 (inputDirection * maxSpeed, rBody.velocity.y);

		// Make sure the character is facing the right direction
		if (inputDirection > 0 && !facingRight)
			Flip ();
		else if (inputDirection < 0 && facingRight)
			Flip ();
	}


	void Flip () {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
