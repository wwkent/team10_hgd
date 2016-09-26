﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController: MonoBehaviour {

	// For movement control
	public float xAccel = 0.75f;
	public float maxSpeed = 30f;
	public float jumpForce = 2000f;
	bool facingRight = true;

	// References
	Animator[] anims; // This is currently not used because I do not have animations yet
	Rigidbody2D rBody;
	public WeaponController currentWeapon;
	public WeaponController defaultWeapon;

	// For Ground collision
	bool onGround = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;

	// Use this for initialization
	void Start () {
		anims = GetComponentsInChildren<Animator> ();
		rBody = GetComponent<Rigidbody2D> ();

		if (defaultWeapon == null) {
			Debug.LogError ("No Default Weapon Set in PlayerController");
		}

		if (currentWeapon == null)
			currentWeapon = Instantiate (defaultWeapon);
		setWeapon ();
	}
	
	// Update is called once per frame
	void Update () {
		if (onGround && (Input.GetButtonDown("A_1") || Input.GetKeyDown("space"))) {
			rBody.AddForce (new Vector2 (0f, jumpForce));
			onGround = false;
		}
		// Shooting
		if (currentWeapon != null && Input.GetAxis ("TriggersR_1") < 0) {
			currentWeapon.Fire ();
			if (currentWeapon.ammo == 0)
				setCurrentWeapon (Instantiate(defaultWeapon));
		}

		if (Input.GetKey("a"))
			transform.Translate(-Vector3.right * maxSpeed * Time.deltaTime);
		if (Input.GetKey("d"))
			transform.Translate(-Vector3.left * maxSpeed * Time.deltaTime);
	}

	void FixedUpdate () {
		onGround = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		float inputDirection = Input.GetAxis ("L_XAxis_1");
		// Calculate how much the velocity should change based on xAccel
		float velChange = inputDirection * xAccel;
		float newXVelocity;
		if (velChange != 0f) {
			// Add to the current velocity
			newXVelocity = rBody.velocity.x + velChange;
			if (onGround)
				PlayAnimation ("Walking");
		} else { 
			// Stop completely if there's no input
			newXVelocity = 0f;
			if (onGround)
				PlayAnimation ("Idle");
		}
		// Limit the max velocity
		newXVelocity = Mathf.Clamp(newXVelocity, -maxSpeed, maxSpeed);

		// Apply the new velocity
		rBody.velocity = new Vector2 (newXVelocity, rBody.velocity.y);

		// Update the speed of the walking animation
		for (int i=0; i<anims.Length; i++)
			anims[i].SetFloat ("Speed", Mathf.Abs(newXVelocity*2)/maxSpeed);

		// Make sure the character is facing the right direction
		if (inputDirection > 0 && !facingRight)
			Flip ();
		else if (inputDirection < 0 && facingRight)
			Flip ();

		// Should start falling animation?
		if (!onGround)
			PlayAnimation ("Falling");
	}

	void PlayAnimation(string name) {
		if(!anims[0].GetCurrentAnimatorStateInfo(0).IsName(name))
			for (int i=0; i<anims.Length; i++)
				anims[i].Play (name, 0, i*0.5f); //Use i*0.5 to offset the second foot
	}
		
	void Flip () {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	// Getters and Setters
	public int getDirectionFacing () {
		if (facingRight)
			return 1;
		else
			return -1;
	}

	public void setCurrentWeapon (WeaponController newWeapon) {
		currentWeapon = newWeapon;
		setWeapon ();
	}

	public void setWeapon () {
		Transform hands = transform.Find ("hands");
		for (int i = 0; i < hands.childCount; i++) {
			Transform weapon = hands.GetChild (i);
			weapon.parent = null;
			Destroy (weapon.gameObject);
		}
		currentWeapon.transform.position = hands.position;
		currentWeapon.transform.rotation = hands.rotation;
		currentWeapon.transform.parent = hands;
		// Make sure that the localScale after you attach the weapon 1, 1, 1
		//  Relative to the parent which is the hands
		currentWeapon.transform.localScale = new Vector3(1,1,1);
	}
}
