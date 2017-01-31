using UnityEngine;
using System.Collections;

public class SpiderController : MonoBehaviour {

	public int health = 100;
	private Transform target;
	private float direction;
	private float velocity = 2f;
	private bool facingLeft = true;
	private float distanceFromPlayer;
	public float range = 20f;
	private int foundPlayer = 0;
	public LayerMask canBeShot;
	// Use this for initialization
	void Start () {
		if (GameObject.Find ("PlayerEnt")) {
			target = GameObject.Find ("PlayerEnt").transform;
			foundPlayer = 1;
		}
	}

	// Update is called once per frame
	void Update () {
		if (foundPlayer == 0) {
			if (GameObject.Find ("PlayerEnt")) {
				target = GameObject.Find ("PlayerEnt").transform;
				foundPlayer = 1;
			}
		}
		distanceFromPlayer = (transform.position.x - target.position.x);
		if (distanceFromPlayer < 0)
			distanceFromPlayer = distanceFromPlayer * -1;

		//move enemy
		if (distanceFromPlayer < range) {
			if (!facingLeft && distanceFromPlayer > 1) {
				transform.Translate (-Vector3.right * velocity * Time.deltaTime);
			} else if(distanceFromPlayer > 1) {
				transform.Translate (-Vector3.right * velocity * Time.deltaTime);
			}

			//should we change the sprites direction
			direction = (transform.position.x - target.position.x);
			if (direction > 0 && !facingLeft && distanceFromPlayer > 1)
				Flip ();
			else if (direction < 0 && facingLeft && distanceFromPlayer > 1)
				Flip ();
		}
	}

	void Flip () {
		facingLeft = !facingLeft;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void applyDamage(int damage)
	{
		health -= damage;
		if (health <= 0)
			Destroy (gameObject);
	}
	//bool playerInSight(){
	//	RaycastHit2D hit = Physics2D.Raycast(Transform.position.x, target.position.x);
	//	if (hit)
	//		return true;
	//	else
	//		return false;
	//}

}
