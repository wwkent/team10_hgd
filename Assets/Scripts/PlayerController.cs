using UnityEngine;
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
	public GameController ui;

	// For Ground collision
	bool onGround = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;

	// Player Info
	public float startingHealth = 100F;
	public float currentHealth;

	// Power Up
	public bool hasPowerUp;
	public float powerUpTimer;
	public string powerUpName;
	private int rnd;
	//public Text powerUpText;
	private WeaponController prevWeapon;
	public int speedMultiplier;
	public int jumpMultiplier;
	public WeaponController powerUpWeapon;

	private bool canTakeDamage = true;
	public float blink_speed = 0.3f;
	public float invincible_duration = 3f;

	// Use this for initialization
	void Start () {
		anims = GetComponentsInChildren<Animator> ();
		rBody = GetComponent<Rigidbody2D> ();
		ui = GameObject.Find ("UI").GetComponent<GameController>();

		currentHealth = startingHealth;
		ui.updateHealth ();

		if (defaultWeapon == null) {
			Debug.LogError ("No Default Weapon Set in PlayerController");
		}

		if (currentWeapon == null)
			currentWeapon = Instantiate (defaultWeapon);
		setWeapon ();

		hasPowerUp = false;
		powerUpTimer = 0;
		powerUpName = "";
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



		/*if (powerUpTimer > 0) {
			Debug.Log ("Here");
			powerUpTimer = powerUpTimer - Time.deltaTime;
			powerUpText.text = powerUpName + " " + (int)((powerUpTimer + 1) / 60) + ":" + (int)(((powerUpTimer + 1) % 60) / 10) + (int)(((powerUpTimer + 1) % 60) % 10); 
		} else */
		if (powerUpTimer <= 0 && hasPowerUp == true) {
			powerUpTimer = 0;
			if (powerUpName.Equals("Upgraded Weapon")) {
				setCurrentWeapon (Instantiate (prevWeapon));
			} else if (powerUpName.Equals("Speed Boost")) {
				maxSpeed /= speedMultiplier;
			} else if (powerUpName.Equals("Jump Boost")) {
				jumpForce /= jumpMultiplier;
			}
			hasPowerUp = false;
			powerUpName = "";
			//powerUpText.text = "";
		}
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

	public void applyDamage(float damage) {
		if (canTakeDamage) {
			currentHealth = Mathf.Clamp (currentHealth - damage, 0, startingHealth);
			StartCoroutine ("takenDamage");
			if (ui)
				ui.updateHealth ();
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "PowerUp") {
			//col.gameObject.GetComponent<PlayerController> ();
			Destroy (col.gameObject);
			getPowerUp ();
			//Destroy (col.gameObject);
		}
	}

	void getPowerUp() {
		if (!hasPowerUp) {
			rnd = Random.Range (0, 3);
			rnd = 0;
			if (rnd == 0) {
				prevWeapon = currentWeapon;
				setCurrentWeapon (Instantiate (powerUpWeapon));
				powerUpName = "Upgraded Weapon";
				Debug.Log ("Weapon");
			} else if (rnd == 1) {
				maxSpeed *= speedMultiplier;
				powerUpName = "Speed Boost";
				Debug.Log ("Speed");
			} else if (rnd == 2) {
				jumpForce *= jumpMultiplier;
				powerUpName = "Jump Boost";
				Debug.Log ("Jump");
			} //else {
			//Invincibility, Score Multiplier, Flying?
			//}
			hasPowerUp = true;
			powerUpTimer = 15f;
		}
	}

	// Coroutine for showing damage
	IEnumerator takenDamage()
	{
		SpriteRenderer[] char_sprites = GetComponentsInChildren<SpriteRenderer> ();
		Color color_with_opacity = new Color (1f, 1f, 1f, 0.7f);
		Color color_without_opacity = new Color (1f, 1f, 1f, 1f);
		int number_of_blinks = (int) (invincible_duration / blink_speed);
		canTakeDamage = false;

		foreach (SpriteRenderer rend in char_sprites) {
			rend.color = Color.red;
		}
		yield return new WaitForSeconds(0.3f);
		foreach (SpriteRenderer rend in char_sprites) {
			rend.color = Color.white;
		}
		int count = 0;
		while (count < number_of_blinks) {
			foreach (SpriteRenderer rend in char_sprites) {
				rend.color = color_with_opacity;
			}
			yield return new WaitForSeconds(blink_speed);
			foreach (SpriteRenderer rend in char_sprites) {
				rend.color = color_without_opacity;
			}
			yield return new WaitForSeconds(blink_speed);
			count++;
		}
		canTakeDamage = true;
	}
}
