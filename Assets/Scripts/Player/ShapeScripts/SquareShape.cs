using UnityEngine;
using System.Collections;

public class SquareShape : Shape {

	private Lunge lunge;

	override public void CustomUpdate () {
		if (lunge.isLunging && (Mathf.Abs(lunge.squareRb.velocity.x) <= 3)) {
			lunge.isLunging = false;
		}
	}

	override public void CustomStart (){
		lunge = this.GetComponent<Lunge> ();
	}

	override public void SquarePressed(){
		lunge.LungeCharge ();
	}

	override public void SquareUp() {
		lunge.LungeStart ();
	}

	override public void MoveShape () {
		if (!lunge.isLunging) {
			if (Mathf.Abs (direction.x) > 0.2f) {
				this.rigidBody.AddForce(new Vector2(direction.x * acceleration, 0));
			}
		}
	}

	void OnCollisionEnter2D (Collision2D col) {
		
		if (col.gameObject.tag == "Player" && lunge.isLunging) {
			col.collider.enabled = false;
			StartCoroutine (lunge.FallDuration (col));
			lunge.squareRb.velocity = Vector2.zero;
		}
	}
}