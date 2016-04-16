using UnityEngine;
using System.Collections;

public class Shape : MonoBehaviour {

	public float jumpModifier;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

	}

	public void Move (float x, float y) {
		Debug.Log ("x: " + x + ", y: " + y);
	}

	public void SquarePressed () {

	}

	public void TrianglePressed () {

	}

	public void CirclePressed () {

	}

	public void CrossPressed () {
		if (ShapeUtils.IsOnGround (this.gameObject)) {
			this.GetComponent<Rigidbody2D> ().AddForce (Vector3.up * jumpModifier * Time.deltaTime);
		}
	}

	public void LeftBumperPressed () {

	}

	public void LeftTriggerPressed () {

	}

	public void RightBumperPressed () {

	}

	public void RightTriggerPressed () {

	}
}
