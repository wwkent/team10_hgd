using UnityEngine;
using System.Collections;

public class CollisionController : MonoBehaviour {

	int times = 0; //Debugging

	//For objects that aren't triggers
	void OnCollisionStay2D(Collision2D other) {
		switch (other.gameObject.tag) {
			case "LaserBeam":
				//TODO Edit health variables & whatnot
				Debug.Log("Ouch! " + (times++));
				break;
		}
	}

	//For objects set as triggers
	void OnTriggerStay2D(Collider2D other) {
		switch (other.gameObject.tag) {
			case "Spike":
				//TODO Edit health variables & whatnot
				Debug.Log("Ouch! " + (times++));
				break;
		}
	}
}
