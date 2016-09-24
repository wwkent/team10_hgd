using UnityEngine;
using System.Collections;

public class SentryController : MonoBehaviour {

	public Transform Target;
	public float RotateSpeed;
	private Quaternion lookRotation;
	private Vector3 direction;

	void Update(){
		direction = (Target.position - transform.position).normalized;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		lookRotation = Quaternion.AngleAxis (angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime * RotateSpeed);
	}


}
