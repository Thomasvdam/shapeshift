using UnityEngine;
using System.Collections;

public class ForcePush : MonoBehaviour {

	public GameObject forceProjectile;
	public float angle = 45f;
	public int projectiles = 50;

	public float jumpForce = 5f;

	private float step;

	void Start () {
		this.step = this.angle / this.projectiles;
	}

	public void FireInDirection (Vector2 direction) {

		// 90 correction to account for sprite 'forward' being right
		float directionAngle = (Mathf.Atan2 (direction.x, direction.y) * Mathf.Rad2Deg) - 90f;
		float currentAngle = directionAngle - (this.angle / 2f);

		// Adjust angle slightly for all individual projectiles.
		Quaternion rotation;
		for (int x = 0; x < this.projectiles; x++) {
			rotation = Quaternion.AngleAxis (currentAngle, Vector3.forward);

			GameObject projectile = Instantiate (forceProjectile, this.transform.position, rotation) as GameObject;
			projectile.GetComponent<ForcePushProjectile> ().Fire ();

			Physics2D.IgnoreCollision (this.GetComponent<Collider2D> (), projectile.GetComponent<Collider2D> ());

			// Increment for next iteration.
			currentAngle += step;
		}

		// Give the player a slight boost when pushing downwards.
		if (direction.y > 0.3f) {
			Vector2 newDirection = new Vector2 (-1 * direction.x, direction.y);
			this.GetComponent<Rigidbody2D> ().AddForce (newDirection * 100 * jumpForce);
		}
	}
}
