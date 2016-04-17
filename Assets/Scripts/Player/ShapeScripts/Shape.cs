using UnityEngine;
using System.Collections;

public class Shape : MonoBehaviour {

	public float jumpModifier;
	public float acceleration = 20f;
	public float maxMovementSpeed = 20f;

	protected Vector2 direction;
	protected Rigidbody2D rigidBody;
	private float sqrMaxMovementSpeed;

	// Use this for initialization
	void Start () {
		// Cache RB.
		this.rigidBody = this.GetComponent<Rigidbody2D> ();

		// Calculate the sqrMaxMovementSpeed.
		this.sqrMaxMovementSpeed = maxMovementSpeed * maxMovementSpeed;

		// Allow custom start code.
		this.CustomStart ();
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
			this.rigidBody.AddForce(new Vector2(direction.x * acceleration, 0));
		}
	}


	virtual public void CustomStart () {

	}

	virtual public void CustomUpdate () {

	}

	virtual public void Move (float x, float y) {
		this.direction = new Vector2 (x, y);
	}

	virtual public void SquarePressed () {

	}

	virtual public void TrianglePressed () {

	}

	virtual public void CirclePressed () {

	}

	virtual public void CrossPressed () {
		if (ShapeUtils.IsOnPlatform (this.gameObject)) {
			this.rigidBody.AddForce (Vector2.up * jumpModifier);
		}
	}

	virtual public void LeftBumperPressed () {

	}

	virtual public void LeftTriggerPressed () {

	}

	virtual public void RightBumperPressed () {
		
	}

	virtual public void RightTriggerPressed () {
		
	}
}
