using UnityEngine;
using System.Collections;

public class DynamicCamera : MonoBehaviour {

	public GameObject player;
	private Vector3 change;

	void Start () {
		change = transform.position - player.transform.position;
	}

	void LateUpdate () {
		transform.position = player.transform.position + change;
	}
}
