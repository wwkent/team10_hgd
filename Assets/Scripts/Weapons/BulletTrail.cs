using UnityEngine;
using System.Collections;

public class BulletTrail : MonoBehaviour {

	public int moveSpeed;
	public float distance;
	public Vector3 startPos;

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * Time.deltaTime * moveSpeed);
		// We need to subtract by 2.7 because the position of the bullet trail is at the center
		//  So if we just base it off of that then the bullettrail will destroy too late
		//  Yea this is weird better fix later plz
		if (distance != 0.0F && Vector3.Distance (startPos, transform.position) > distance - 2.7)
			Destroy (gameObject);
		else
			Destroy (gameObject, 1);
	}
}
