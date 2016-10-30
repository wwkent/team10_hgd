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

			//Delete any previous segments
			foreach (Transform child in transform.Find("LaserHead").GetComponentInChildren<Transform>()) {
				LeanPool.Destroy (child.gameObject);
			}

			if(state == 1)
				propogateLaser (laserSight);
			if (state == 2) {
				propogateLaser (laserBeam);
				source.PlayOneShot (laserSound, 0.5f);
			}
		}
	}

	void propogateLaser(Collider2D obj) {
		//Create a laser sight/beam segment
		GameObject segment = LeanPool.Spawn(obj.gameObject, 
			new Vector3 (0, -0.5f, 0), Quaternion.identity, transform.Find("LaserHead"));

		float distance = Vector3.Distance (transform.Find("LaserHead").position, transform.Find("LaserEnd").position);

		Vector3 prev = segment.transform.localScale;
		segment.transform.localScale = new Vector3(prev.x, distance, prev.z);
		//If this segment is colliding with something, delete it and break the loop
		//if (segment.GetComponent<Collider2D>().IsTouchingLayers (LayerMask.NameToLayer("Platforms"))) {
//		if (segment.GetComponent<Collider2D>().IsTouchingLayers (LayerMask.NameToLayer("Platforms"))) {
//			LeanPool.Despawn (segment.gameObject);
//			break;
//		}
	}
}
