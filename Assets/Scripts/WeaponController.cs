using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	public float variance;
	public int ammo;
	public Projectile shotObject;
	public float fireRate = 0.5F;
	public LayerMask canBeShot;
	public int projSpeed;

	public Transform firePoint;

	protected float nextFire = 0.0F;
	protected Vector3 shootDir;

	// Use this for initialization
	void Start () {
		firePoint = GameObject.Find ("firePoint").transform;

		if (firePoint == null) {
			Debug.Log ("No firePoint found");
		}
	}

	public virtual void Fire (){
	}
}
