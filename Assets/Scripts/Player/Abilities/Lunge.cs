using UnityEngine;
using System.Collections;

public class Lunge : MonoBehaviour {

	public bool isLunging = false;
	public bool isCharging = false;
	public static int lungeForce = 1000;

	public Rigidbody2D squareRb;

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
			
			isCharging = false;
			isLunging = true;
			chargeEnd = Time.time;
			squareRb.isKinematic = false;

			float x = Input.GetAxisRaw ("joystick 1 X axis");
			x = x > 0 ? 1 : -1;
			squareRb.AddForce (new Vector3 (lungeForce * x * (chargeEnd - chargeStart), 0.0f, 0.0f));

//			yield return new WaitForSeconds (0.4f);
//			isLunging = false;

		}
	}

	public IEnumerator LungeDuration (Collision2D col) {
		
		yield return new WaitForSeconds (0.4f);
		col.collider.enabled = true;
		isLunging = false;
		Debug.Log ("colback");

	}

}

