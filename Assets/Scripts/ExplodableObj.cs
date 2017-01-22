using UnityEngine;
using System.Collections;

public class ExplodableObj : MonoBehaviour {
	//Transform explosionPoint;
	public float maxR = 1000;
	public float force = 40;
	public float dropOffRate = 1;
	public LayerMask mask;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void doExplode() {
		print ("explode triggered");
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.gameObject.transform.position, this.maxR, this.mask);
		print ("Found " + hitColliders.Length + " colliders");
		//apply force to each object
		foreach (Collider2D c in hitColliders) {
			Rigidbody2D phy = c.GetComponent<Rigidbody2D> ();
			print ("the phy for object " + c.gameObject.name + " is " + phy);
			if (phy == null) continue; //not physics object so skip
			//add the force
			print("found game object: " + c.gameObject.name);
			Vector3 calc = (c.gameObject.transform.position - transform.position).normalized;
			calc.z = 0f;
			print (transform.up);
			phy.AddForce (transform.up * this.force, ForceMode2D.Impulse);
			print ("reg: " + (transform.position - c.gameObject.transform.position));
			print ("norm: " + (transform.position - c.gameObject.transform.position).normalized);
			print ("force: " + (transform.position - c.gameObject.transform.position).normalized * this.force);
			print ("calc: " + calc);
			print ("calcF: " + calc * this.force);
		}
	}
}
