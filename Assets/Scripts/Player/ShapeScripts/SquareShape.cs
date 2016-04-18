using UnityEngine;
using System.Collections;

public class SquareShape : Shape {

	public float forceCooldown = 1.5f;
	private float cooldownTimeStamp;
	private Lunge lunge;

	override public void CustomUpdate () {
		if (lunge.isLunging && (Mathf.Abs(lunge.squareRb.velocity.x) <= 3)) {
			lunge.isLunging = false;
		}
	}

	override public void CustomStart (){
		lunge = this.GetComponent<Lunge> ();
	}

	override public void RightBumperPressed(){
		if (Time.time <= this.cooldownTimeStamp) {
			return;
		}
		lunge.LungeCharge ();
	}

	override public void RightBumperUp() {
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