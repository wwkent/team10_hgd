using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public Transform target;
	private float direction;
	public int enemySpeed;

	bool onGround = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	//public LayerMask whatIsGround;

	Rigidbody2D rBody;
	private bool facingRight = true;
	// Use this for initialization
	void Start () {
		rBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		direction = (target.position.x - transform.position.x);
		transform.LookAt (target);
		transform.position += transform.forward * enemySpeed * Time.deltaTime;
		//rBody.velocity = new Vector2 (direction, 0);
		//onGround = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		if (direction > 0 && !facingRight)
			Flip ();
		else if (direction < 0 && facingRight)
			Flip ();
	}

	void Flip () {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
