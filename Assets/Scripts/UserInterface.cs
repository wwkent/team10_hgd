using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
	public Image heartFull;
	private int score;
	private float timer;
	private int health;
	private int ammo;
	private int round;
	private GameObject player;
	private ShootController shoot;

	void Start () {

		// Starting values for UI statistics:
		score = 0/*PlayerPrefs.GetInt("score")*/;
		timer = 120.0f;
		health = 100;
		round = 1;
		/*if (PlayerPrefs.GetInt ("round") == 0) {
			round = 1;
		} else {
			round = PlayerPrefs.GetInt("round");
		}*/
		player = GameObject.Find ("PlayerCharacter");
		shoot = player.GetComponent<ShootController> ();
		ammo = shoot.ammo;

	}

	void Update () {
		
		if (timer >= 0 && health > 0) {

			// Displays current statistics for player in UI labels:
			scoreText.text = "Score:  " + score;
			timer = timer - Time.deltaTime;
			timerText.text = "Timer:  " + (int)((timer + 1) / 60) + ":" + (int)(((timer + 1) % 60) / 10) + (int)(((timer + 1) % 60) % 10);
			healthText.text = "Health:";
			ammoText.text = "Ammo:  " + shoot.ammo;
			roundText.text = "Round " + round;

			//health--;
			//score++;

			// Player health determines number of hearts:
			if (health <= 0) {
				heart1.sprite = heartEmpty.sprite;
				heart2.sprite = heartEmpty.sprite;
				heart3.sprite = heartEmpty.sprite;
				heart4.sprite = heartEmpty.sprite;
			} else if (health <= 25) {
				heart1.sprite = heartFull.sprite;
				heart2.sprite = heartEmpty.sprite;
				heart3.sprite = heartEmpty.sprite;
				heart4.sprite = heartEmpty.sprite;
			} else if (health <= 50) {
				heart1.sprite = heartFull.sprite;
				heart2.sprite = heartFull.sprite;
				heart3.sprite = heartEmpty.sprite;
				heart4.sprite = heartEmpty.sprite;
			} else if (health <= 75) {
				heart1.sprite = heartFull.sprite;
				heart2.sprite = heartFull.sprite;
				heart3.sprite = heartFull.sprite;
				heart4.sprite = heartEmpty.sprite;
			} else {
				heart1.sprite = heartFull.sprite;
				heart2.sprite = heartFull.sprite;
				heart3.sprite = heartFull.sprite;
				heart4.sprite = heartFull.sprite;
			}

		} else {
			
			// Removes everything from UI:
			scoreText.text = "";
			timerText.text = "";
			healthText.text = "";
			ammoText.text = "";
			roundText.text = "";
			heart1.GetComponent<Image> ().color = Color.clear;
			heart2.GetComponent<Image> ().color = Color.clear;
			heart3.GetComponent<Image> ().color = Color.clear;
			heart4.GetComponent<Image> ().color = Color.clear;

			// Save player score and current round: (doesn't delete after exiting game)
			//PlayerPrefs.SetInt ("score", score);
			//PlayerPrefs.SetInt ("round", round + 1);

			// Reload scene for next round: (for testing)
			//SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);

		}
	}
}
