using UnityEngine;
using System.Collections;

public class GrappleBooster : MonoBehaviour {

	private GameObject grappleBoosterPrefab;
	private Controller controller;

	void Start() {
		Debug.Log ("Start");
		controller = GetComponent<Controller> ();
		string path = "Prefabs/" + "GrappleBooster";
		grappleBoosterPrefab = Resources.Load<GameObject>(path);
	}

	//Fires a grapple booster
	public void fire() {
		Vector2 direction = new Vector2 (controller.getXAxisRaw (), controller.getYAxisRawInverted ());
		GameObject instantiated = (GameObject) Instantiate(grappleBoosterPrefab, getDirectionLeftOrRight(direction), Quaternion.identity);
		instantiated.GetComponent<GrappleBoosterProjectile> ().setDirection (direction);
		instantiated.GetComponent<Rigidbody2D> ().AddForce(direction * 700);
	}

	private Vector3 getDirectionLeftOrRight(Vector2 direction) {
		if (direction.x < -0.5f) {
			//left
			return new Vector3 (gameObject.transform.position.x - grappleBoosterPrefab.GetComponent<SpriteRenderer> ().bounds.size.x, gameObject.transform.position.y, 0f);
		} else if (direction.y > 0.5f) {
			return new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + grappleBoosterPrefab.GetComponent<SpriteRenderer> ().bounds.size.y, 0f);
		} else if (direction.y < -0.5f) {
			return new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y - grappleBoosterPrefab.GetComponent<SpriteRenderer> ().bounds.size.y, 0f);
		} else {
			return new Vector3 (gameObject.transform.position.x + grappleBoosterPrefab.GetComponent<SpriteRenderer> ().bounds.size.x, gameObject.transform.position.y, 0f);
		}
	
	}
}
