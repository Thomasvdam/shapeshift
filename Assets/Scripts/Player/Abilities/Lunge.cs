using UnityEngine;
using System.Collections;

public class Lunge : MonoBehaviour {

	public bool isLunging = false;
	public bool isCharging = false;
	public Rigidbody2D squareRb;
	public int lungeForce = 0;

	private float chargeStart;
	private float chargeEnd;

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

			float x = Input.GetAxisRaw ("joystick 1 X axis");
			x = x > 0 ? 1 : -1;
			squareRb.isKinematic = false;
			squareRb.AddForce (new Vector3 (lungeForce * x, 0.0f, 0.0f));
			isCharging = false;
			isLunging = true;

		}
	}

	public IEnumerator LungeDuration (Collision2D col) {
		
		yield return new WaitForSeconds (0.4f);
		col.collider.enabled = true;

	}

}

