using UnityEngine;
using System.Collections;

public class CreatorController : MonoBehaviour {

	public GameObject[] availableObjs;
	public float moveSpeed;
	// Used to get reference to see who is the current player
	private GameController game;

	// Use this for initialization
	void Start () {
		// game = GameObject.Find ("UI").GetComponent<GameController> ();
		// print (game.player);
	}
	
	// Update is called once per frame
	void Update () {
		float inputXAmount = Input.GetAxis ("L_XAxis_1");
		float inputYAmount = Input.GetAxis ("L_YAxis_1");
		// Calculate how much the velocity should change based on xAccel
		Vector3 direction = new Vector3(inputXAmount, -inputYAmount, 0.0f);
		transform.Translate (moveSpeed * direction * Time.deltaTime);
	}
}
