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
	
	// Update is called once per frame
	void Update () {
		// Add collision with enemies
	}
}
