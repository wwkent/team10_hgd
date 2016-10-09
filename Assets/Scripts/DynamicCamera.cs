using UnityEngine;
using System.Collections;

public class DynamicCamera : MonoBehaviour {

	public GameObject player;
	private Vector3 change;

	void Start () {
		setChange ();
	}

	void LateUpdate () {
		transform.position = player.transform.position + change;
	}

	public void setChange() {
		change = transform.position - player.transform.position;
	}
}
