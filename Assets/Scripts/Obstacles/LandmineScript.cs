using UnityEngine;
using System.Collections;

public class LandmineScript : MonoBehaviour {
	public GameObject explosion;
	private Transform explodePoint;
	public float damage = 30f;
	// Use this for initialization
	void Start () {
		explodePoint = transform.Find ("explodePoint").transform;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		print ("Got coll on landmine");
		//Instantiate (explosion, this.explodePoint);
		this.SendMessage("doExplode");
		print ("called the thing");
		coll.gameObject.SendMessage ("applyDamage", this.damage, UnityEngine.SendMessageOptions.DontRequireReceiver);
		//now remove yourself
		Destroy(this.gameObject);
	}
}
