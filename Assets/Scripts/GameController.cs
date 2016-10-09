using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public Text scoreText;
	public Text timerText;
	public Text roundText;
	public Text ammoText;
	public Text countdownText;
	private float countdownTimer;
	/* 
	 * This is the rect transform for the green health because that is what
	 * 	we are modifying
	 */
	public RectTransform healthBar;

	// To determine which controller is which player
	// This is for the purpose of swapping roles
	// public int whoIsPlayer;
	// public int whoIsCreator;

	private int score = 0;
	private float timer = 10.0F;
	/*
	 * Tells whether the Player or the Creator has control
	 * 0 - Creator
	 * 1 - Between
	 * 2 - Player
	 * 3 - End of Round
	 */
	private int state;
	private int round;

	private float width;
	private float startMaxXPos;

	public CreatorController creatorPrefab;
	public PlayerController playerPrefab;
	private CreatorController creator;
	private PlayerController player;
	private WeaponController shoot;

	void Start () {
		state = 0;
		round = 1;
		width = healthBar.rect.width;
		startMaxXPos = healthBar.offsetMax.x;
		//Create the creator
		creator = Instantiate (creatorPrefab);
	}

	void Update () {
		//TODO A switch statement might be better here
		if (state == 0) {
			updateTimer ();
			if (timer <= 0) {
				DestroyObject (creator.gameObject);
				createPlayer ();
				timer = 120.0F;
				state = 1;
			}
		} else if (state == 1) {
			
		} else {
			if (timer >= 0 && player.currentHealth > 0) {
				// Displays current statistics for player in UI labels:
				scoreText.text = score.ToString ();
				updateTimer ();
				roundText.text = "Round: " + round;
				ammoText.text = player.currentWeapon.ammo.ToString ();
			} else {
				// Removes everything from UI:
				scoreText.text = "";
				timerText.text = "";
				roundText.text = "";
				ammoText.text = "";
			}
		}
	}

	private void createPlayer() {
		player = Instantiate (playerPrefab);
		//Maybe get a reference to this beforehand?
		DynamicCamera cam  = GameObject.Find("Main Camera").GetComponent<DynamicCamera>();
		cam.player = player.gameObject;
		cam.setChange ();
	}

	private void updateTimer() {
		timer = timer - Time.deltaTime;
		timerText.text = (int)((timer + 1) / 60) + ":" + (int)(((timer + 1) % 60) / 10) + (int)(((timer + 1) % 60) % 10);

	}

	public void updateHealth () {
		float playerHealth = player.currentHealth / player.startingHealth;
		healthBar.localScale = new Vector2 (Mathf.Clamp(playerHealth, 0F, 1F), healthBar.localScale.y);
	}
}
