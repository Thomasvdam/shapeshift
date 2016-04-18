using UnityEngine;
using System.Collections;

public class SquareShape : Shape {

	private Lunge lunge;

	override public void CustomStart (){
		lunge = this.GetComponent<Lunge> ();
	}

	override public void SquarePressed(){
		lunge.LungeCharge ();
	}

	override public void SquareUp() {
		lunge.LungeStart ();
	}

	void OnCollisionEnter2D (Collision2D col) {

		if (col.gameObject.tag == "Player" && lunge.isLunging) {
			col.collider.enabled = false;
			StartCoroutine (lunge.LungeDuration (col));
			lunge.squareRb.velocity = Vector2.zero;

		}
	}
}