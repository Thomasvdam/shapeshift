using UnityEngine;
using System.Collections;

public class ParticleScript : MonoBehaviour {

	private ParticleSystem particles;

	// Use this for initialization
	void Start () {
		this.particles = this.gameObject.GetComponent<ParticleSystem> () as ParticleSystem;
	}
	
	// Update is called once per frame
	void Update () {
		if (!particles.IsAlive ()) {
			Destroy (this);
		}
	}
}
