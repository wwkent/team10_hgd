using UnityEngine;
using System.Collections;

public class ArmSpin : MonoBehaviour {
	/*
    RotateTowardsInput rotateScript;
    Vector3 pointOfRotation;
    float armLength;

	// Use this for initialization
	void Start () {
        rotateScript = GetComponent<RotateTowardsInput>();
		pointOfRotation = transform.parent.position;
		pointOfRotation.z = 0;
		armLength = 2f;
	}
	
	// Update is called once per frame
	void Update () {
		pointOfRotation = transform.parent.position;
		pointOfRotation.z = 0;
        rotateScript.inputVector = rotateScript.mousePos - pointOfRotation;
        repositionArm();
        rotateScript.rotate();
    }

    void repositionArm()
    {
        Vector3 vec = rotateScript.inputVector.normalized;
		if (rotateScript.isFlipped)
			vec.x *= -1;
        transform.localPosition = vec * armLength;
    }
    */
}
