using UnityEngine;
using System.Collections;

public class Shape : MonoBehaviour {

	public float jumpModifier;
	public float movementSpeed;

	private Vector2 direction;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		this.MoveShape ();

		// Allow room for custom class code on Update
		this.CustomUpdate ();
	}

	public void MoveShape () {
		// Move character sideways only when joystick is used.
		if (Mathf.Abs (direction.x) > 0.2f) {
			Vector2 velocity = this.GetComponent<Rigidbody2D> ().velocity;
			this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (direction.x * movementSpeed * Time.deltaTime, velocity.y);
		}
	}

	public void CustomUpdate () {

	}

	public void Move (float x, float y) {
		this.direction = new Vector2 (x, y);
	}

	public void SquarePressed () {

	}

	public void TrianglePressed () {

	}

	public void CirclePressed () {

	}

	public void CrossPressed () {
		if (ShapeUtils.IsOnPlatform (this.gameObject)) {
			this.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jumpModifier);
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
