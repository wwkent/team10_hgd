using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour {

	protected PlayerController playerCont;
	public WeaponController attachedWeapon;
	private bool hasPowerUp;
	private float timer;
	private string powerUpName;
	private int rnd;
	public Text powerUpText;
	private WeaponController prevWeapon;

	private SpriteRenderer renderer;

	void Start() {
		/*hasPowerUp = false;
		timer = 0f;
		powerUpName = " ";
		rnd = 0;
		powerUpText.text = "";
		renderer = GetComponent<SpriteRenderer> ();
		renderer.sprite = attachedWeapon.GetComponent<SpriteRenderer> ().sprite;
		*/
	}

	void Update() {
		/*Debug.Log (timer);
		if (timer > 0) {
			Debug.Log ("Here");
			timer = timer - Time.deltaTime;
			powerUpText.text = powerUpName + " " + (int)((timer + 1) / 60) + ":" + (int)(((timer + 1) % 60) / 10) + (int)(((timer + 1) % 60) % 10); 
		} else if (timer <= 0 && hasPowerUp == true) {
			timer = 0;
			if (powerUpName.Equals("Upgraded Weapon")) {
				playerCont.setCurrentWeapon (Instantiate (prevWeapon));
			} else if (powerUpName.Equals("x2 Speed")) {
				playerCont.maxSpeed /= 2;
			} else if (powerUpName.Equals("x2 Jump")) {
				playerCont.jumpForce /= 2;
			}
			hasPowerUp = false;
			powerUpText.text = "";
		}*/
	}

	void OnCollisionEnter2D(Collision2D col) {
		/*if (col.gameObject.tag == "Player") {
			hasPowerUp = true;
			playerCont = col.gameObject.GetComponent<PlayerController> ();
			getPowerUp ();
		}*/

		//Destroy (gameObject);
		//gameObject.SetActive(false);
	}

	void getPowerUp() {
		/*rnd = Random.Range (0, 3);
		if (rnd == 0) {
			prevWeapon = playerCont.currentWeapon;
			playerCont.setCurrentWeapon(Instantiate(attachedWeapon));
			powerUpName = "Upgraded Weapon";
			Debug.Log ("Weapon");
		} else if (rnd == 1) {
			playerCont.maxSpeed *= 2;
			powerUpName = "x2 Speed";
			Debug.Log ("Speed");
		} else if (rnd == 2) {
			playerCont.jumpForce *= 2;
			powerUpName = "x2 Jump";
			Debug.Log ("Jump");
		} //else {
			//Invincibility, Score Multiplier, Flying?
		//}
		hasPowerUp = true;
		timer = 30f;*/
	}
}
