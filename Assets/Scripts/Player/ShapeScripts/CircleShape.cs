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
			Debug.Log ("ola we glide");
			gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.01f;
		} else {
			Debug.Log ("we no glido");
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
		gameObject.AddComponent<CircleCollider2D> ();
		gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Art/circle-basic");
		this.gameObject.AddComponent<GrappleBooster>();
	}

	public override void CustomEnd() {
		Destroy(this.gameObject.GetComponent<GrappleBooster> ());
		base.CustomEnd ();
	}

}
