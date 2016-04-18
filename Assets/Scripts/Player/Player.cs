using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int coins;
	public int deaths;
	public int mId;

	public GameObject particleSystem;

	private Controller controller;
	private Shape currentShape;

	public float shiftCooldown = 3f;
	private float cooldownTimeStamp;

	public AudioClip deathSound;

	// Use this for initialization
	void Start () {
		this.currentShape = this.gameObject.AddComponent<TriangleShape> ();
		this.controller = this.GetComponent<Controller> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SquarePressed () {
		// Don't change into the same shape.
		if (this.currentShape is SquareShape) {
			return;
		}

		if (!this.checkCooldown ()) {
			return;
		}

		// Destroy the current context.
		this.currentShape.CustomEnd ();

		// Add an animation.
		Instantiate(this.particleSystem, this.transform.position, this.transform.rotation);

		// And create new context.
		this.currentShape = this.gameObject.AddComponent<SquareShape> ();

		// Update controller with new shape.
		this.controller.shape = this.currentShape;
	}

	public void TrianglePressed () {
		// Don't change into the same shape.
		if (this.currentShape is TriangleShape) {
			return;
		}

		if (!this.checkCooldown ()) {
			return;
		}

		// Destroy the current context.
		this.currentShape.CustomEnd ();

		// Add an animation.
		Instantiate(this.particleSystem, this.transform.position, this.transform.rotation);

		// And create new context.
		this.currentShape = this.gameObject.AddComponent<TriangleShape> ();

		// Update controller with new shape.
		this.controller.shape = this.currentShape;
	}

	public void CirclePressed () {
		// Don't change into the same shape.
		if (this.currentShape is CircleShape) {
			return;
		}

		if (!this.checkCooldown ()) {
			return;
		}

		// Destroy the current context.
		this.currentShape.CustomEnd ();

		// Add an animation.
		Instantiate(this.particleSystem, this.transform.position, this.transform.rotation);

		// And create new context.
		this.currentShape = this.gameObject.AddComponent<CircleShape> ();

		// Update controller with new shape.
		this.controller.shape = this.currentShape;
	}

	private bool checkCooldown () {
		if (Time.time <= this.cooldownTimeStamp) {
			return false;
		}

		// Set cooldown.
		this.cooldownTimeStamp = Time.time + this.shiftCooldown;
		return true;
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

		// Play death sound
		AudioManager.instance.PlaySound (deathSound);

		// Remove and respawn player
		// TODO
	}
}
