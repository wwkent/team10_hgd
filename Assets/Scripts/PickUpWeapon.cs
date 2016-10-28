using UnityEngine;
using System.Collections;

public class PickUpWeapon : PickUpController {

	public override void applyPickUp(PlayerController player)
	{
		WeaponController wep = Instantiate(attachedObject).GetComponent<WeaponController>();
		player.pickUpWeapon (wep);
		Destroy (gameObject);
	}
}
