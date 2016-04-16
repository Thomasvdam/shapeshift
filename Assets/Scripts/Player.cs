using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int coins;
	public int deaths;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		// Check the collision.
		if (other.gameObject.tag == "Coin") {
			this.OnCoinTrigger (other.gameObject);
		}
	}

	void OnCoinTrigger (GameObject coin) {
		// Add coin point.
		this.coins = this.coins + 1;

		// Play coin collect sound.
		// TODO

		// Get rid of coin object.
		Destroy(coin);
	}
}
