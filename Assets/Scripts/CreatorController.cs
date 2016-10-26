using UnityEngine;
using System.Collections;

public class CreatorController : MonoBehaviour {

	public GameObject[] availableObjs;
	public float moveSpeed;
	// Used to get reference to see who is the current player
	private GameController game;
	private int currObj;
	private Transform currObjRenderer;

	// Use this for initialization
	void Start () {
		// game = GameObject.Find ("UI").GetComponent<GameController> ();
		// print (game.player);
		currObj = 0;
		currObjRenderer = transform.Find ("currentObj");
		setObjRenderer ();
	}
	
	// Update is called once per frame
	void Update () {
		float inputXAmount = Input.GetAxis ("L_XAxis_1");
		float inputYAmount = Input.GetAxis ("L_YAxis_1");

		if (Input.GetButtonDown ("A_1"))
			spawnGameObject ();

		if (Input.GetButtonDown ("RB_1")) {
			if (currObj < availableObjs.Length - 1)
				currObj++;
			else
				currObj = 0;
			setObjRenderer ();
		}
		if (Input.GetButtonDown ("LB_1")) {
			if (currObj > 0)
				currObj--;
			else
				currObj = availableObjs.Length - 1;
			setObjRenderer ();
		}
		// Calculate how much the velocity should change based on xAccel
		Vector3 direction = new Vector3(inputXAmount, -inputYAmount, 0.0f);
		transform.Translate (moveSpeed * direction * Time.deltaTime);
	}

	private void spawnGameObject()
	{
		GameObject spawned = Instantiate (availableObjs [currObj]);
		spawned.transform.position = transform.position;
		spawned.GetComponent<SentryController>().enabled = false;
		Debug.Log ("Creator has created: " + spawned.name);
	}

	private void setObjRenderer()
	{
		SpriteRenderer currSelectedSprite = availableObjs [currObj].GetComponent<SpriteRenderer> ();
		currObjRenderer.GetComponent<SpriteRenderer>().sprite = currSelectedSprite.sprite;
		Color temp = currObjRenderer.GetComponent<SpriteRenderer> ().color;
		temp.a = 0.7f;
		currObjRenderer.GetComponent<SpriteRenderer> ().color = temp;
	}
}
