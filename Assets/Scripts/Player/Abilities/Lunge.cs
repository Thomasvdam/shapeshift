using UnityEngine;
using System.Collections;

public class Lunge : MonoBehaviour {

	public bool isLunging = false;
	public Rigidbody2D squareRb;

	private int lungeForce = 1000;

	// Use this for initialization
	void Start () {
		squareRb = GetComponent<Rigidbody2D> ();
	}

	public void LungeSide(){

		float x = Input.GetAxisRaw ("joystick 1 X axis");
		x = x > 0 ? 1 : -1;
		squareRb.AddForce (new Vector3 (lungeForce * x, 0.0f, 0.0f));
		isLunging = true;

	}

	public IEnumerator LungeDuration (Collision2D col) {
		
		yield return new WaitForSeconds (0.4f);
		col.collider.enabled = true;

	}

}

