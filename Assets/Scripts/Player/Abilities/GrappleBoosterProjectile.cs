using UnityEngine;
using System.Collections;

public class GrappleBoosterProjectile : MonoBehaviour {

	private bool hasGrappledPlayer = false;
	Vector2 direction;
	GameObject grappledPlayer;
	private float timePassed = 0f;
	private float maxTimePassed = 1f;

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
		string path = "Art/hook/hook0";
		Sprite grappledSprite = Resources.Load <Sprite> (path);

		if (other.gameObject.tag == "Player") {
			hasGrappledPlayer = true;
			GetComponent<SpriteRenderer>().sprite = grappledSprite;
			flip (direction);
			//hasGrappledPlayer = true;
			grappledPlayer = other.gameObject;
			this.gameObject.transform.SetParent(grappledPlayer.transform);
			this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
			this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
			grappledPlayer.GetComponent<Rigidbody2D>().AddForce(-direction * 700);
		} else if(!hasGrappledPlayer) {
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Border") {
			Destroy(this.gameObject);
		}
	}

	void Update() {
		if (hasGrappledPlayer) {
			timePassed += Time.deltaTime;
			if(timePassed > maxTimePassed) {
				Destroy(this.gameObject);
			}
		}
	}
}
