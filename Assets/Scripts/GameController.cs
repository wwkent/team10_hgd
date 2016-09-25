using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public Text scoreText;
	public Text timerText;
	public Text roundText;
	/* 
	 * This is the rect transform for the green health because that is what
	 * 	we are modifying
	 */
	public RectTransform healthBar;
	public float startingHealth = 100F;
	public float currentHealth;

	private int score = 0;
	private float timer = 120.0F;
	private int round;

	private float width;
	private float startMaxXPos;

	public PlayerController player;
	private WeaponController shoot;

	void Start () {
		round = 1;

		currentHealth = startingHealth;
		width = healthBar.rect.width;
		startMaxXPos = healthBar.offsetMax.x;
	}

	void Update () {
		if (timer >= 0 && currentHealth > 0) {
			// Displays current statistics for player in UI labels:
			scoreText.text = score.ToString();
			timer = timer - Time.deltaTime;
			timerText.text = ((int)((timer + 1) / 60) + ":" + (int)(((timer + 1) % 60) / 10) + (int)(((timer + 1) % 60) % 10)).ToString();
			roundText.text = "Round: " + round;
		} else {
			
			// Removes everything from UI:
			scoreText.text = "";
			timerText.text = "";
			roundText.text = "";
		}
	}

	public void ApplyDamage (float damage) {
		currentHealth -= damage;
		float healthBarX = startMaxXPos - width * (1 - (currentHealth / startingHealth));
		healthBarX = healthBarX / healthBar.rect.width;
		healthBar.localScale = new Vector2 (Mathf.Clamp(healthBarX, 0F, 1F), healthBar.localScale.y);
	}
}
