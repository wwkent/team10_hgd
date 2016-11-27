using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	private float countdownTimer;

	// To determine which controller is which player
	// This is for the purpose of swapping roles
	// public int whoIsPlayer;
	// public int whoIsCreator;

	private int[] scores = {0, 0};
	private int currPlayer;
	private int currCreator;
	//Increase this for a longer Creator phase
	private float timer = 10.0F;
	private int state;
	private int round;

	private float width;
	private float startMaxXPos;
	public string[] phaseSwitchMessages = { "Time's Up!", "Get Ready...", "3", "2", "1", "Go!" };
	public float[] phaseSwitchTimes = { 1f, 2f, 0.5f, 0.5f, 0.5f, 0.5f };
	private int phaseSwitchState = 0;

	public GameObject scoreboardCanvas;
	private Scoreboard scoreboard;
	public GameObject creatorPrefab;
	public GameObject playerPrefab;

	private Transform creatorContainer;
	private CreatorController creator;
	private CreatorHud creatorUI;

	private Transform playerContainer;
	private PlayerController player;
	private PlayerHud playerUI;

	private GameObject mapContainer;

	private DynamicCamera camera;

	void Start () {
		state = 0;
		round = 1;
		timer = 10f;
		scores [0] = 0;
		scores [1] = 0;
		currPlayer = 0;
		currCreator = 1;

		camera = GameObject.Find("Main Camera").GetComponent<DynamicCamera>();
		scoreboardCanvas = Instantiate (scoreboardCanvas);
		scoreboard = scoreboardCanvas.transform.FindChild ("Scoreboard").GetComponent<Scoreboard> ();
		scoreboardCanvas.SetActive (false);
	}

	void Update () {
		switch (state) {
		case 0: //Creator
			{
				if (!mapContainer)
					generateMap ();
				
				string timeText;
				timeText = (int)((timer + 1) / 60) + ":" + (int)(((timer + 1) % 60) / 10) + (int)(((timer + 1) % 60) % 10);
				if (!creator) {
					createCreator ();
				}
				creator.ui.updateTimers (timeText);

				if (timer <= 0) {
					creatorContainer.gameObject.SetActive(false);
					scoreboardCanvas.SetActive (true);
					scoreboard.updateScoreboardAll (
						phaseSwitchMessages[0], 
						scores[0], 
						scores[1], 
						currPlayer, 
						currCreator, 
						round);
					phaseSwitchState = 0;
					timer = phaseSwitchTimes[0];
					nextState ();
				}
				break;
			}
		case 1: //Phase Switch
			{
				if (timer <= 0) {
					phaseSwitchState++;
					if (phaseSwitchState >= phaseSwitchMessages.Length) {
						scoreboardCanvas.SetActive (false);
						if (!playerContainer)
							createPlayer ();
						playerContainer.gameObject.SetActive (true);
						camera.setFollowing (player.gameObject);
						timer = 10F; // CHANGE THIS BACK
						nextState ();
					} else {
						scoreboard.updateScoreboardMessage (phaseSwitchMessages[phaseSwitchState]);
						timer = phaseSwitchTimes [phaseSwitchState];
					}
				}
				break;
			}
		case 2: //Player
			{
				string timeText;
				timeText = (int)((timer + 1) / 60) + ":" + (int)(((timer + 1) % 60) / 10) + (int)(((timer + 1) % 60) % 10);
				player.ui.updateTimers (timeText);
				if (timer <= 0 || player.currentHealth <= 0) {
					playerContainer.gameObject.SetActive (false);
					timer = 10f;

					scoreboardCanvas.SetActive (true);
					scoreboard.updateScoreboardAll (
						phaseSwitchMessages[0], 
						scores[0], 
						scores[1], 
						currPlayer, 
						currCreator, 
						round);
					
					nextState ();
				}
				break;
			}
		case 3:
			{
				Destroy (mapContainer);
				string timeText;
				timeText = (int)((timer + 1) / 60) + ":" + (int)(((timer + 1) % 60) / 10) + (int)(((timer + 1) % 60) % 10);
				scoreboard.updateScoreboardMessage (timeText);

				if (timer <= 0) {
					timer = 10f;
					scoreboardCanvas.gameObject.SetActive (false);
					creatorContainer.gameObject.SetActive (true);
					camera.setFollowing (creator.gameObject);
					nextState ();
				}
				break;
			}
		}
		timer -= Time.deltaTime;
	}

	private void nextState()
	{
		if (state >= 3)
			state = 0;
		else
			state = state + 1;
	}

	private void createPlayer() {
		playerContainer = Instantiate (playerPrefab).transform;
		player = playerContainer.Find("PlayerEnt").GetComponent<PlayerController>();
		playerUI = playerContainer.Find("PlayerUI").GetComponent<PlayerHud>();
		camera.setFollowing (player.gameObject);
	}

	private void createCreator() {
		creatorContainer = Instantiate (creatorPrefab).transform;
		creator = creatorContainer.Find("CreatorEnt").GetComponent<CreatorController>();
		creatorUI = creatorContainer.Find("CreatorUI").GetComponent<CreatorHud>();
		camera.setFollowing (creator.gameObject);
	}

	public void generateMap(){
		string rnd = Random.Range (1, 4).ToString();
		string mapPath = "Map" + rnd;
		mapContainer = Instantiate (Resources.Load(mapPath, typeof(GameObject))) as GameObject;
	}
}
