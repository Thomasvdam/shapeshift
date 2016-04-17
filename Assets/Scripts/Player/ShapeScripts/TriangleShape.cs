using UnityEngine;
using System.Collections;

public class TriangleShape : Shape {

	public float forceCooldown = 1.5f;
	private float cooldownTimeStamp;

	private ForcePush force;

	override public void CustomStart () {
		this.force = this.GetComponent<ForcePush> ();
	}

	override public void RightBumperPressed () {
		if (Time.time <= this.cooldownTimeStamp) {
			return;
		}

		// Normalize direction.
		Vector2 normDirection = direction.normalized;

		// Fire the force push.
		force.FireInDirection (normDirection);

		// Set cooldown.
		this.cooldownTimeStamp = Time.time + this.forceCooldown;
	}
}
