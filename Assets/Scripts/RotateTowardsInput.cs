using UnityEngine;
using System.Collections;

public class RotateTowardsInput : MonoBehaviour {

    public Vector3 inputVector;
    public Vector3 mousePos;

	// Use this for initialization
	void Start () {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        inputVector = mousePos - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        inputVector = mousePos - transform.position;
        rotate();
	}

    public void rotate()
    {
        float angle = Mathf.Atan2(inputVector.y, inputVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        if (Mathf.Abs(angle) > 90f)
        {
            transform.localScale = new Vector2(transform.localScale.x, -Mathf.Abs(transform.localScale.y));
        }
        else
        {
            transform.localScale = new Vector2(transform.localScale.x, Mathf.Abs(transform.localScale.y));
        }
    }
}
