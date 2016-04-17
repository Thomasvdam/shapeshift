using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int coins;
	public int deaths;
	public int mId;

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
		} else if (other.gameObject.tag == "Border") {
			this.OnBorderTrigger ();
		}
	}

	void OnCoinTrigger (GameObject coin) {
		// Add coin point.
		this.coins = this.coins + 1;

		// Play coin collect sound.
		// TODO

		// Get rid of coin object.
		coin.GetComponent<CoinSpawner> ().onCoinCollected ();
	}

	void OnBorderTrigger () {
		// Increment deaths.
		this.deaths = this.deaths + 1;

		// Play death sound.
		// TODO

		// Remove and respawn player
		// TODO
	}
}
