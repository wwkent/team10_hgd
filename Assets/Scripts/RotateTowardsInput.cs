using UnityEngine;
using System.Collections;

public class RotateTowardsInput : MonoBehaviour {

    public Vector3 inputVector;
    public Vector3 mousePos;
	public bool isFlipped;

	// Use this for initialization
	void Start () {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
		inputVector = mousePos - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		isFlipped = transform.root.localScale.x < 1;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        inputVector = mousePos - transform.position;
        rotate();
	}

    public void rotate()
    {
        float angle = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

		float scaleX, scaleY;

		if (isFlipped)
			scaleX = -Mathf.Abs(transform.localScale.x);
		else
			scaleX = Mathf.Abs(transform.localScale.x);
		
		if (Mathf.Abs(angle) > 90f)
			scaleY = -Mathf.Abs(transform.localScale.y);
        else
			scaleY = Mathf.Abs(transform.localScale.y);

		transform.localScale = new Vector2 (scaleX, scaleY);
    }
}
