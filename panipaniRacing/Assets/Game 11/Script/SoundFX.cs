using UnityEngine;
using System.Collections;

public class SoundFX : MonoBehaviour {
	public AudioClip impactExplode;

	public AudioClip arrowShootSound;

	public AudioClip arrowHitSound;

	public AudioClip jumpsound;

	public AudioClip horseNigh;

	public AudioClip personScream;

	public AudioClip coinClip;


	AudioSource audiosource;
	// Use this for initialization

	void Start(){

		audiosource = GetComponent<AudioSource>();
		explodeAudio();
		horseNighPlay();
	}//start

	public void explodeAudio(){

		audio.PlayOneShot(impactExplode, 0.7F);

	}

	public void shootArrow(){

		audio.PlayOneShot(arrowShootSound, 0.7F);
	}//

	public void arrowHitPlay(){

		audio.PlayOneShot(arrowHitSound);
	}


	public void horseNighPlay(){


		audio.PlayOneShot(horseNigh, 0.7F);
	}

	public void playJumpSound(){

		audio.PlayOneShot(jumpsound, 0.7F);
	}

	public void arrowExplode(){

		audio.PlayOneShot(impactExplode, 0.7F);
	}//arrow


	public void personHit(){

		audio.PlayOneShot(personScream, 0.7F);
	}

	public void coinSound(){

		audio.PlayOneShot(coinClip, 0.7F);
	}

}
