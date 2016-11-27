using UnityEngine;
using System.Collections;
using Lean;

public class LaserHead : MonoBehaviour {

	//# of frames to stay in each state
	public int[] stateLength = { 60, 60, 30 };
	//Prefabs to shoot (LaserSight and LaserBeam)
	public Collider2D laserSight;
	public Collider2D laserBeam;
	public AudioClip laserSound;
	private AudioSource source;

	public void Awake () {
		source = GetComponent<AudioSource> ();
	}

	/*
	 * State loops between the following
	 * 0 - Resting
	 * 1 - Preparing laser
	 * 2 - Firing laser
	 */
	int state = 0;
	int stateTime = 0;
	
	// Update is called once per frame
	void Update () {
		stateTime++;

		if (stateTime >= stateLength [state]) {
			stateTime = 0;
			state = ((state + 1) % 3);

			switch (state) {
			case 0:
				LeanPool.Destroy (transform.GetChild(0).gameObject);
				break;
			case 1:
				shootLaser (laserSight);
				break;
			case 2:
				LeanPool.Destroy (transform.GetChild(0).gameObject);
				shootLaser (laserBeam);
				source.PlayOneShot (laserSound, 0.5f);
				break;
			}
		}
	}

	void shootLaser(Collider2D obj) {
		Quaternion rotation = transform.localRotation;
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		Vector3 beginPoint = transform.position + (rotation * Vector3.up);

		// Create a laser sight/beam segment
		GameObject segment = LeanPool.Spawn(obj.gameObject, Vector3.up, Quaternion.Euler(new Vector3(0, 0, 180)), transform);

		// Check to see how far the beam should go
		RaycastHit2D hit = Physics2D.Raycast (beginPoint, rotation * Vector2.up, 20, LayerMask.GetMask ("Platforms"));

		// Stretch the beam to reach the end of the raycast
		Vector3 prev = segment.transform.localScale;
		segment.transform.localScale = new Vector3(prev.x, hit.distance, prev.z);
	}

	// Check if a laser shooting from this position/rotation would hit anything
	public static bool checkLaser(Vector3 position, Quaternion rotation) {
		RaycastHit2D hit = Physics2D.Raycast (position + (rotation * Vector3.up), rotation * Vector2.up, 20, LayerMask.GetMask ("Platforms"));
		return (hit.collider != null);
	}
}
