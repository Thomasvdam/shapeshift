using UnityEngine;
using System.Collections;

public class Shape : MonoBehaviour {

	public float jumpStrength = 5f;
	public float acceleration = 20f;
	public float maxMovementSpeed = 20f;

	protected Vector2 direction;
	protected Rigidbody2D rigidBody;
	private float sqrMaxMovementSpeed;

	private bool grounded = false;

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

	virtual public void MoveShape () {
		// Move character sideways only when joystick is used.
		if (Mathf.Abs (direction.x) > 0.2f) {
			this.rigidBody.AddForce(new Vector2(direction.x * acceleration, 0));
		}
	}

	//Call this when shapeshifting to remove components
	virtual public void CustomEnd() {
		Destroy (this);
	}


	virtual public void CustomStart () {

	}

	virtual public void CustomUpdate () {

	}

	virtual public void Move (float x, float y) {
		this.direction = new Vector2 (x, y);
	}

	virtual public void SquareHeld () {
		
	}

	virtual public void SquareUp () {
		
	}

	virtual public void CrossPressed () {
		if (grounded) {
			this.rigidBody.AddForce (Vector2.up * jumpStrength * 100);
		}
	}

	virtual public void LeftBumperPressed () {

	}

	virtual public void LeftTriggerPressed () {

	}

	virtual public void RightBumperPressed () {
		
	}

	virtual public void RightBumperUp () {

	}

	virtual public void RightTriggerPressed () {
		
	}

	void OnCollisionStay2D (Collision2D other) {
		if (other.gameObject.tag == "Platform") {
			if (Vector2.Angle (other.contacts [0].normal, Physics2D.gravity) > 100f) {
				this.grounded = true;
			} else {
				this.grounded = false;
			}
		}
	}

	void OnCollisionExit2D (Collision2D other) {
		if (other.gameObject.tag == "Platform") {
			this.grounded = false;
		}
	}
}
