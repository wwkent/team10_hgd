using UnityEngine;
using System.Collections;

public class PickUpWeapon : PickUpController {
	
	public GameObject attachedWeaponPrefab;

	void Start()
	{
		if (!attachedWeaponPrefab)
			Debug.LogError ("No Weapon Prefab attached to Pick Up");
		
		myRenderer = GetComponent<SpriteRenderer> ();
		myRenderer.sprite = attachedWeaponPrefab.GetComponent<SpriteRenderer> ().sprite;
	}

	public override void applyPickUp(PlayerController player)
	{
		WeaponController wep = Instantiate(attachedWeaponPrefab).GetComponent<WeaponController>();
		player.pickUpWeapon (wep);
		gameObject.SetActive (false);
	}
}
