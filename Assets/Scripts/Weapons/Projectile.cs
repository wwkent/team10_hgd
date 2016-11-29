using UnityEngine;
using System.Collections;
using Lean;

public class Projectile : MonoBehaviour {
	public LayerMask canDamage;
	public LayerMask canInteractWith;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 4);
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if (col.gameObject.tag == "Player") {
			col.gameObject.GetComponent<PlayerController> ().applyDamage (10);
			Destroy (gameObject);
		} else if (col.gameObject.tag == "Platforms") {
			Destroy (gameObject);
		} else if (col.gameObject.tag == "Enemies") {
			col.gameObject.GetComponent<SentryController> ().applyDamage (10);
			Destroy (gameObject);
		}
			
	}
	
	// Update is called once per frame
	void Update () {
		// Add collision with enemies
	}
}
