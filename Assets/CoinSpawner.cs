using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour {

	public float timePassed = 0f;
	public float timeUntilCoinSpawn = 10f;

	// Update is called once per frame
	void Update () {
		if (!GetComponent<BoxCollider2D> ().enabled) {
			timePassed += Time.deltaTime;
			if(timePassed >= timeUntilCoinSpawn) {
				spawnCoin();
			}
		}
	}

	public void onCoinCollected() {
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<BoxCollider2D> ().enabled = false;
	}

	private void spawnCoin() {
		timePassed = 0f;
		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<BoxCollider2D> ().enabled = true;
	}
}
