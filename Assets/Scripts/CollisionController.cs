using UnityEngine;
using System.Collections;

public class CollisionController : MonoBehaviour {

	public PlayerController player;

	void Start() {
		player = GetComponent<PlayerController> ();
	}

	//For objects that aren't triggers
	void OnCollisionStay2D(Collision2D other) {
		switch (other.gameObject.tag) {

		case "LaserBeam":
			player.applyDamage (1f);
			break;
		}
	}

	//For objects set as triggers
	void OnTriggerStay2D(Collider2D other) {
		switch (other.gameObject.tag) {

		case "Obstacle_Damage":
			player.applyDamage (10f);
			break;
		case "PowerUp":
			player.applyPowerUp ();
			break;
		case "WeaponPickUp":
			// WeaponController wep = other.gameObject.GetComponent<PickUpWeapon> ().myWeapon;
			// player.pickUpWeapon (wep);
			break;
		case "Saw":
			player.applyDamage (1f);
			break;
		}
	}
}
