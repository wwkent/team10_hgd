using UnityEngine;
using System.Collections;

public class SentryController : WeaponController {

	private Transform target;
	public float rotateSpeed;
	public float range = 100f;
	private Quaternion lookRotation;
	private Vector3 direction;

	void Start() {
		if (rotateSpeed < 1f)
			rotateSpeed = 1f;
		if (GameObject.Find ("PlayerEnt"))
			target = GameObject.Find ("PlayerEnt").transform;
		source = GetComponent<AudioSource> ();
	}

	void Update(){
		direction = (target.position - transform.position).normalized;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		lookRotation = Quaternion.AngleAxis (angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime * rotateSpeed);
		playerCheck ();
	}
		
	void playerCheck(){
		RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.right, range, canBeShot);
		if (hit && LayerMask.LayerToName (hit.transform.gameObject.layer) == "Character") {
			Fire ();
		}
	}
}
