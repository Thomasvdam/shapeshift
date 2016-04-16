using UnityEngine;
using System.Collections;

public class RemoveSprite : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SpriteRenderer sprite = this.GetComponent<SpriteRenderer> ();
		sprite.sprite = null;
	}
}
