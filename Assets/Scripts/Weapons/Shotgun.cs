using UnityEngine;
using System.Collections;

public class Shotgun : WeaponController {

	public GameObject bulletTrailPrefab;
	public int spread = 7;

	public override void Fire() {
		if (Time.time > nextFire) {
			if (ammo > 0) {
				ammo--;
				for (int i = 0; i < spread; i++) {
					// For calculating Variance in shooting to emulate bullet spread
					// Multiply by 0.01 so that the values can be changed easily
					shootDir = firePoint.right;
					shootDir.y += Random.Range (-1 * variance * 0.01F, variance * 0.01F);

					nextFire = Time.time + 1 / fireRate;
					if (!shotObject) {
						shootRay ();
					} else {
						shootProjectile ();
					}
				}
			}
		}
	}

	void shootRay () {
		// Ray2D rShot = new Ray2D (firePoint.position, firePoint.right * 100);
		Debug.DrawRay (firePoint.position, shootDir * 100, Color.red);

		RaycastHit2D hit = Physics2D.Raycast (firePoint.position, shootDir, 100, canBeShot);
		if (hit) {
			generateTrail (hit.distance);
			print ("Hit: " + hit.transform.name);
			if (LayerMask.LayerToName (hit.transform.gameObject.layer) == "Platforms") {
				// Do things to the enemy
			} else if (LayerMask.LayerToName (hit.transform.gameObject.layer) == "Enemies") {

			}
		} else {
			generateTrail ();
		}
	}

	void generateTrail(float distance = -1F)
	{
		float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
		Quaternion lookRotation = Quaternion.AngleAxis (angle, Vector3.forward);
		GameObject bulletObj = Instantiate (bulletTrailPrefab, firePoint.position, lookRotation) as GameObject;
		BulletTrail trail = bulletObj.GetComponent<BulletTrail> ();
		trail.distance = distance;
		trail.startPos = firePoint.position;
	}

	void shootProjectile () {
		Projectile clone = (Projectile) Instantiate (shotObject, firePoint.position, firePoint.rotation);
		clone.GetComponent<Rigidbody2D> ().AddForce (shootDir * projSpeed);
	}
}
