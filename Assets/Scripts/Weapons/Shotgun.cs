using UnityEngine;
using System.Collections;

public class Shotgun : WeaponController {

	public int spread = 7;

	public override void Fire() {
		if (Time.time > nextFire) {
			if (ammo > 0) {
				source.PlayOneShot (gunshot, 1.0f);
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
}
