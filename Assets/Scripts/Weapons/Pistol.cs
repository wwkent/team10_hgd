using UnityEngine;
using System.Collections;

public class Pistol : WeaponController {

	public GameObject bulletTrailPrefab;

	public override void Fire() {
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
		Debug.DrawRay (firePoint.position, shootDir * 100, Color.red);

		RaycastHit2D hit = Physics2D.Raycast (firePoint.position, shootDir, 100, canBeShot);
		if (hit) {
			Effect (hit.distance);
			print (hit.transform.gameObject.name);
			if (LayerMask.LayerToName (hit.transform.gameObject.layer) == "Platforms") {
				// Do things to the enemy
				print ("I hit floor");
			}
		} else {
			Effect ();
		}
	}

	void Effect(float distance = 0.0F)
	{
		Vector2 direction = (shootDir - transform.position).normalized;
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
