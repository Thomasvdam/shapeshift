using UnityEngine;
using System.Collections;

public class Shape : MonoBehaviour {

	public float jumpModifier;
	public float acceleration = 20f;
	public float maxMovementSpeed = 20f;

	private Vector2 direction;
	private Rigidbody2D rigidBody;
	private float sqrMaxMovementSpeed;

	// Use this for initialization
	void Start () {
		// Cache RB.
		this.rigidBody = this.GetComponent<Rigidbody2D> ();

		// Calculate the sqrMaxMovementSpeed.
		this.sqrMaxMovementSpeed = maxMovementSpeed * maxMovementSpeed;
	}

	// Update is called once per frame
	void Update () {
		this.MoveShape ();

		// Allow room for custom class code on Update
		this.CustomUpdate ();
	}

	void FixedUpdate () {
		Vector2 velocity = rigidBody.velocity;

		if (velocity.sqrMagnitude > sqrMaxMovementSpeed) {
			this.rigidBody.velocity = velocity.normalized * maxMovementSpeed;
		}
	}

	public void MoveShape () {
		// Move character sideways only when joystick is used.
		if (Mathf.Abs (direction.x) > 0.2f) {
//			Vector2 velocity = this.GetComponent<Rigidbody2D> ().velocity;
//			this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (direction.x * movementSpeed * Time.deltaTime, velocity.y);
			this.rigidBody.AddForce(new Vector2(direction.x * acceleration, 0));
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
			this.rigidBody.AddForce (Vector2.up * jumpModifier);
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
