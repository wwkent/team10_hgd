using UnityEngine;
using System.Collections;

// Required components for pickups
[RequireComponent (typeof (SpriteRenderer))]
[RequireComponent (typeof (BoxCollider2D))]

public class PickUpController : MonoBehaviour {

	public GameObject attachedObject;
	private SpriteRenderer myRenderer;

	// Use this for initialization
	void Start () {
		myRenderer = GetComponent<SpriteRenderer> ();
		myRenderer.sprite = attachedObject.GetComponent<SpriteRenderer> ().sprite;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.transform.tag == "Player") {
			applyPickUp (col.gameObject.GetComponent<PlayerController>());
		}
	}

	public virtual void applyPickUp(PlayerController player)
	{ }
}
