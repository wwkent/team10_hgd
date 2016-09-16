using UnityEngine;
using System.Collections;

public class ShootController : MonoBehaviour {

	public float variance;
	public int ammo;
	public Projectile shotObject;
	public float fireRate = 0.5F;
	public LayerMask canBeShot;
	public float projSpeed = 100F;

	Transform firePoint;

	private float nextFire = 0.0F;
	private Vector3 shootDir;

	// Use this for initialization
	void Start () {
		firePoint = GameObject.Find ("firePoint").transform;

		if (firePoint == null) {
			Debug.Log ("No firePoint found");
		}
	}
		
	public void Fire() {
		if (ammo > 0) {
			if (Time.time > nextFire) {
				// For calculating Variance in shooting to emulate bullet spread
				// Multiply by 0.01 so that the values can be changed easily
				shootDir = firePoint.right;
				shootDir.y += Random.Range (-1 * variance * 0.01F, variance * 0.01F);

				nextFire = Time.time + 1 / fireRate;
				ammo--;
				if (!shotObject)
					shootRay ();
				else
					shootProjectile ();
			}
		}
	}

	void shootRay () {
		// Ray2D rShot = new Ray2D (firePoint.position, firePoint.right * 100);
		Debug.DrawRay (firePoint.position, firePoint.right * 100, Color.red);

		RaycastHit2D hit = Physics2D.Raycast (firePoint.position, shootDir, 100, canBeShot);
		if (hit) {
			print (hit.transform.gameObject.name);
			if (LayerMask.LayerToName(hit.transform.gameObject.layer) == "Platforms") {
				// Do things to the enemy
				print("I hit floor");
			}
		}
	}

	void shootProjectile () {
		Projectile clone = (Projectile) Instantiate (shotObject, firePoint.position, firePoint.rotation);
		clone.GetComponent<Rigidbody2D> ().AddForce (shootDir * projSpeed);
	}
}
