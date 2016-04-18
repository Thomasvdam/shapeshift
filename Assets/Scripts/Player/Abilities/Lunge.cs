using UnityEngine;
using System.Collections;

public class Lunge : MonoBehaviour {

	public bool isLunging = false;
	public bool isCharging = false;

	public Rigidbody2D squareRb;

	private float chargeStart;
	private float chargeEnd;
	private float maxCharge = 5;
	private float chargeValue;
	private static int lungeForce = 1000;
	private float lungeDuration = 1f;
	private float fallTime = 0.4f;

	void Start () {
		squareRb = GetComponent<Rigidbody2D> ();
	}
		
	public void LungeCharge () {
		isCharging = true;
		squareRb.isKinematic = true;
		chargeStart = Time.time;
	}

	public void LungeStart () {

		if (isCharging) {
			isCharging = false;
			isLunging = true;
			chargeEnd = Time.time;
			squareRb.isKinematic = false;
			chargeValue = (chargeEnd - chargeStart) > maxCharge ? maxCharge : (chargeEnd - chargeStart);

			float x = Input.GetAxisRaw ("joystick 1 X axis");
			x = x > 0 ? 1 : -1;
			squareRb.AddForce (new Vector3 (lungeForce * x * chargeValue, 0.0f, 0.0f));

			StartCoroutine( LungeTime ());
		}
	}

	// For the duration of the lunge
	// -	the square cannot (joystick) move
	// -	other players's colliders are disabled for fallTime seconds when hit by lunging square
	public IEnumerator LungeTime () {
		
		yield return new WaitForSeconds (lungeDuration);
		isLunging = false;
	}

	public IEnumerator FallDuration (Collision2D col) {
		
		yield return new WaitForSeconds (fallTime);
		col.collider.enabled = true;
		isLunging = false;
	}
}

