using UnityEngine;
using System.Collections;

public class DynamicCamera : MonoBehaviour {

	private GameObject following;
	private Vector3 change;

	void Start () {
		if (!GameObject.Find ("UI"))
			following = GameObject.Find ("Player");
		setChange ();
	}

	void LateUpdate () {
		if (following)
			transform.position = following.transform.position + change;
	}

	public void setFollowing(GameObject o) {
		following = o;
		Vector3 temp = following.transform.position;
		temp.z = transform.position.z;
		transform.position = temp;
		setChange ();
	}

	public void setChange() {
		if (following)
			change = transform.position - following.transform.position;
	}
}
