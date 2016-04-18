using UnityEngine;
using System.Collections;

public class GrappleBoosterProjectile : MonoBehaviour {

	private Sprite grappledSprite;
	//private bool hasGrappledPlayer = false;
	Vector2 direction;
	GameObject grappledPlayer;

	public void setDirection(Vector2 direction) {
		this.direction = direction;
		flip (direction);
	}

	private void flip(Vector2 direction) {
		if (direction.x <= 0)
		{
			transform.localRotation = Quaternion.Euler(0, 180, 0);
		}
	}

	void OnCollisionEnter2D (Collision2D other) {
		Sprite grappled = Resources.Load <Sprite> ("Art/hook/hook1");

		if (other.gameObject.tag == "Player") {
			GetComponent<SpriteRenderer>().sprite = grappledSprite;
			//hasGrappledPlayer = true;
			grappledPlayer = other.gameObject;
			grappledPlayer.GetComponent<Rigidbody2D>().AddForce(-direction * 700);
			GetComponent<PolygonCollider2D>().enabled = false;
			GetComponent<Rigidbody2D>().isKinematic = true;
			transform.parent = grappledPlayer.transform;
		} else {
			Destroy(this.gameObject);
		}
	}

	/*void Update() {
		if (hasGrappledPlayer) {

		}
	}*/
}
