using UnityEngine;
using System.Collections;

public class GrappleBoosterProjectile : MonoBehaviour {

	public GameObject GrappleBoosterPrefab;
	public Sprite grappled;
	private Controller controller;

	void Awake() {
		Debug.Log ("Awake");
		controller = GetComponent<Controller> ();
		Sprite grappled = Resources.Load <Sprite> ("Art/hook/hook1");
		GrappleBoosterPrefab = Resources.Load<GameObject> ("Prefabs/GrappleBooster");
	}

	//Fires a grapple booster
	public void fire() {
		Vector2 direction = new Vector2 (controller.getXAxisRaw (), controller.getYAxisRaw ());
		GameObject instantiated = (GameObject) Instantiate(GrappleBoosterPrefab, getDirectionLeftOrRight(direction), Quaternion.identity);
		instantiated.GetComponent<Rigidbody2D> ().velocity = direction;
		       
	}

	private Vector3 getDirectionLeftOrRight(Vector2 direction) {
		if (direction.x > 0) {
			//right
			return new Vector3(direction.x + (GrappleBoosterPrefab.GetComponent<SpriteRenderer>().bounds.size.x/2f), direction.y, 0f);
		} else {
			//left
			return new Vector3(direction.x - (GrappleBoosterPrefab.GetComponent<SpriteRenderer>().bounds.size.x/2f), direction.y, 0f);
		}
	
	}
}
