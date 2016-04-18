using UnityEngine;
using System.Collections;

public class SquareShape : Shape {

	public float forceCooldown = 1.5f;
	private float cooldownTimeStamp;
	private Lunge lunge;

	override public void CustomUpdate () {
	}

	override public void CustomStart (){
		base.CustomStart ();
		// Change sprite and animation.
		this.GetComponent<Animator> ().Play("Square");

		// Add a placeholder sprite so we can create a polygoncollider.
		this.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Art/player/grey/squareGrey5");

		// Add a collider to the shape.
		this.gameObject.AddComponent<BoxCollider2D> ();

		// Add custom code.
		gameObject.AddComponent<Lunge> ();
		lunge = this.GetComponent<Lunge> ();

	}

	override public void CustomEnd () {
		Destroy (this.gameObject.GetComponent<BoxCollider2D> ());
		Destroy (this.gameObject.GetComponent<Lunge>());
		base.CustomEnd ();
	}

	override public void RightBumperPressed(){
		base.RightBumperPressed ();
		if (Time.time <= this.cooldownTimeStamp) {
			Debug.Log ("Skill on cooldown");
			return;
		}
		lunge.LungeCharge ();
	}

	override public void RightBumperUp() {
		base.RightBumperUp ();
		if (lunge.isCharging) {
			lunge.LungeStart ();
			this.cooldownTimeStamp = Time.time + this.forceCooldown;
		}
	}

	override public void MoveShape () {
		if (!lunge.isLunging) {
			if (Mathf.Abs (direction.x) > 0.2f) {
				this.rigidBody.AddForce(new Vector2(direction.x * acceleration, 0));
			}
		}
	}
}