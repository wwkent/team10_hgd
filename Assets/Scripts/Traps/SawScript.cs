using UnityEngine;
using System.Collections;

public class SawScript : MonoBehaviour {
	public int speed = 20;

	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.forward * -90, 25 * Time.deltaTime * speed);
	}
}
