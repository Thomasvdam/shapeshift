using UnityEngine;
using System.Collections;

public class ForcePushProjectile : MonoBehaviour
{
	public Sprite lingerSprite;

	public float lifeTime;
	public float speed;
	public float growthRate;
	// Time projectile lingers after collision.
	public float lingerTime;

	private float expireTime;

	// Use this for initialization
	void Start () {
		this.expireTime = Time.time + lifeTime;
	}

	// Update is called once per frame
	void Update () {
		this.transform.localScale = this.transform.localScale + new Vector3 (0, 1, 0) * Time.deltaTime * growthRate;

		if (Time.time > this.expireTime) {
			Destroy (this.gameObject);
		}
	}

	public void Fire () {
		this.GetComponent<Rigidbody2D> ().AddForce (this.transform.right * 100 * speed);
	}

	void OnCollisionEnter2D (Collision2D other) {
		// Switch sprite to the inactive sprite
		this.GetComponent<SpriteRenderer> ().sprite = lingerSprite;

		// Make the push 'fizzle' when it hits a platform
		if (other.gameObject.tag == "Platform") {
			// Remove mass from Rigidbody to prevent pushes
			this.GetComponent<Rigidbody2D> ().mass = 0;

			// Remove the object after lingertime
			this.expireTime = Time.time + lingerTime;
		}
	}
}

