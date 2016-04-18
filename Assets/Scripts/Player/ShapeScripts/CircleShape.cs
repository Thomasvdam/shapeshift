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
		this.gameObject.GetComponent<GrappleBoosterProjectile> ().fire ();

	}

	public override void CustomStart ()
	{
		base.CustomStart ();
		this.gameObject.AddComponent<GrappleBoosterProjectile>();
	}

	public override void CustomEnd() {
		Destroy(this.gameObject.GetComponent<GrappleBoosterProjectile> ());
		base.CustomEnd ();
	}

}
