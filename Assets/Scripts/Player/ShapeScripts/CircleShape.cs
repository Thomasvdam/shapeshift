using UnityEngine;
using System.Collections;

public class CircleShape : Shape {

	public override void CustomUpdate ()
	{
		base.CustomUpdate ();
		checkForGlide ();
	}

	private void checkForGlide() {
		if(ShapeUtils.IsOnPlatform(this.gameObject)) {
			this.rigidBody.gravityScale = 2f;
		} else {
			this.rigidBody.gravityScale = 0.01f;
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
		gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Art/circle-basic");
		this.gameObject.AddComponent<GrappleBooster>();
	}

	public override void CustomEnd() {
		Destroy(this.gameObject.GetComponent<GrappleBooster> ());
		base.CustomEnd ();
	}

}
