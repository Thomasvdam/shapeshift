using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance = null;

	AudioSource audio = null;

	void Awake () {
		//Check if there is already an instance of SoundManager
		if (instance == null) {
			//if not, set it to this.
			instance = this;
			audio = GetComponent<AudioSource> ();
		}
		//If instance already exists:
		else if (instance != this) {
			//Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
			Destroy (gameObject);
		}

		//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
		DontDestroyOnLoad (gameObject);
	}

	public void PlaySound (AudioClip sound) {
		if (sound) {
			audio.PlayOneShot (sound, 1f);
		}
	}
}
