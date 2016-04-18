using UnityEngine;
using System.Collections;

public class CircleShape : Shape {

	public override void CustomUpdate ()
	{
		base.CustomUpdate ();
		checkForGlide ();
	}

	private void checkForGlide() {
		if (!ShapeUtils.IsOnPlatform (this.gameObject) && GetComponent<Controller> ().getKey (Controller.LEFT_BUMPER_PS4)) {
			gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.01f;
		} else {
			gameObject.GetComponent<Rigidbody2D>().gravityScale = 2f;
		}
	}

	public override void RightBumperPressed ()
	{
		base.RightBumperPressed ();
		this.gameObject.GetComponent<GrappleBooster> ().fire ();

	}

	public override void CustomStart ()
	{
		base.CustomStart ();
		// Change sprite and animation.
		this.GetComponent<Animator> ().Play("Circle");

		// Add a placeholder sprite so we can create a polygoncollider.
		this.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Art/player/grey/circleGrey5");

		// Add a collider to the shape.
		this.gameObject.AddComponent<CircleCollider2D> ();

		// Add grappling hook.
		this.gameObject.AddComponent<GrappleBooster>();
	}

	public override void CustomEnd() {
		Destroy(this.gameObject.GetComponent<GrappleBooster> ());
		base.CustomEnd ();
	}

}
