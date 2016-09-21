using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour {

	public Text scoreText;
	public Text timerText;
	public Text healthText;
	public Text ammoText;
	public Text roundText;
	public Image heart1;
	public Image heart2;
	public Image heart3;
	public Image heart4;
	public Image heartEmpty;
	private int score;
	private float timer;
	private int health;
	private int ammo;
	private int round;

	void Start () {
		score = 0;
		timer = 120.0f;
		health = 100;
		ammo = 5;
		round = 1;
	}

	void Update () {
		if (timer >= 0 && health > 0) {
			scoreText.text = "Score:  " + score;
			timer = timer - Time.deltaTime;
			timerText.text = "Timer:  " + (int)((timer + 1) / 60) + ":" + (int)(((timer + 1) % 60) / 10) + (int)(((timer + 1) % 60) % 10);
			healthText.text = "Health:";
			ammoText.text = "Ammo:  " + ammo;
			roundText.text = "Round " + round;
			if (health <= 0) {
				heart1.sprite = heartEmpty.sprite;
			} else if (health <= 25) {
				heart2.sprite = heartEmpty.sprite;
			} else if (health <= 50) {
				heart3.sprite = heartEmpty.sprite;
			} else if (health <= 75) {
				heart4.sprite = heartEmpty.sprite;
			}
		} else {
			// Game over protocol
			scoreText.text = "";
			timerText.text = "";
			healthText.text = "";
			ammoText.text = "";
			roundText.text = "";
		}
	}
}
