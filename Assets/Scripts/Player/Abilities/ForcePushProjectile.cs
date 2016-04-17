using UnityEngine;
using System.Collections;

public class ForcePushProjectile : MonoBehaviour
{

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
		this.expireTime = Time.time + lingerTime;
	}
}

