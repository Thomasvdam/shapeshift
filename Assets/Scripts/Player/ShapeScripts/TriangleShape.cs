using UnityEngine;
using System.Collections;

public class TriangleShape : Shape {

	public float forceCooldown = 1.5f;
	private float cooldownTimeStamp;

	private ForcePush force;

	override public void CustomStart () {
		// Change sprite and animation.
		this.GetComponent<Animator> ().Play("Triangle");

		// Add a placeholder sprite so we can create a polygoncollider.
		this.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Art/player/grey/triangleGrey5");

		// Add a collider to the shape.
		this.gameObject.AddComponent<PolygonCollider2D> ();

		// Add force push component to the player.
		this.force = this.gameObject.AddComponent<ForcePush> () as ForcePush;
		this.force.forceProjectile = Resources.Load ("Prefabs/ForceProjectile") as GameObject;
	}

	public override void CustomEnd() {
		Destroy(this.gameObject.GetComponent<ForcePush> ());
		Destroy(this.gameObject.GetComponent<PolygonCollider2D> ());
		base.CustomEnd ();
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
