using UnityEngine;
using System.Collections;

public class ExplodableObj : MonoBehaviour {
	//Transform explosionPoint;
	public float maxR = 1000;
	public float force = 2;
	public float dropOffRate = 1;
	public LayerMask mask;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void doExplode() {
		//print ("explode triggered");
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.gameObject.transform.position, this.maxR, this.mask);
		//print ("Found " + hitColliders.Length + " colliders");
		//apply force to each object
		foreach (Collider2D c in hitColliders) {
			Rigidbody2D phy = c.GetComponent<Rigidbody2D> ();
			//print ("the phy for object " + c.gameObject.name + " is " + phy);
			if (phy == null) continue; //not physics object so skip
			//add the force
			//print("found game object: " + c.gameObject.name);
			//was
			//Vector3 calc = (c.gameObject.transform.position - this.gameObject.transform.position).normalized;
			Vector3 calc = (c.gameObject.transform.position - this.gameObject.transform.position);
			calc.z = 0f;
			//print (transform.up);
			//was
			//phy.AddForce ((new Vector3(1,1,1) - calc) * this.force, ForceMode2D.Impulse);
			phy.AddForce (calc * this.force, ForceMode2D.Impulse);
			print ("push vec: " + (calc) * this.force);
		}
	}
}
