using UnityEngine;
using System.Collections;

public class DynamicCamera : MonoBehaviour {

	private GameObject following;
	private Vector3 change;

	void Start () {
		following = GameObject.Find ("Player_New");
		setChange ();
	}

	void LateUpdate () {
		transform.position = following.transform.position + change;
	}

	public void setFollowing(GameObject o) {
		following = o;
		setChange ();
	}

	public void setChange() {
		change = transform.position - following.transform.position;
	}
}
