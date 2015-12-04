using UnityEngine;
using System.Collections;

public class SoundFX : MonoBehaviour {
	public AudioClip impactExplode;
	AudioSource audiosource;
	// Use this for initialization

	void Start(){

		audiosource = GetComponent<AudioSource>();
		explodeAudio();

	}//start

	public void explodeAudio(){

		audio.PlayOneShot(impactExplode, 0.7F);

	}

}
