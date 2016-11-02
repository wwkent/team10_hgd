using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum PowerUp {
	jump,
	movement,
	gravity,
	resistance,
	health
}

public class PickUpPower: PickUpController
{
	public PowerUp powerUpSelected;
	public float valueToApply;
	public int powerUpDuration;

	void Start()
	{
		if (powerUpSelected == PowerUp.health)
			powerUpDuration = -1;
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.transform.tag == "Player")
			applyPickUp (col.gameObject.GetComponent<PlayerController> ());
		Destroy (gameObject);
	}

	public override void applyPickUp (PlayerController player)
	{
		switch (powerUpSelected.ToString()) {
		case "jump":
			player.jumpForce = valueToApply;
			break;
		case "movement":
			player.maxSpeed = valueToApply;
			break;
		case "gravity":
			player.GetComponent<Rigidbody2D>().gravityScale = valueToApply;
			break;
		case "resistance":
			// This is a decimal value
			player.resistance = valueToApply;
			break;
		case "health":
			player.modifyHealth (-1 * valueToApply);
			break;
		}

		if (powerUpDuration > 0) 
			player.powerUpUntil(powerUpDuration, GetComponent<SpriteRenderer>().sprite);
	}
}
