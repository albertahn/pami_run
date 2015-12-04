using UnityEngine;
using System.Collections;

public class wallColide : MonoBehaviour {

	public AudioClip hitSound;
	public AudioSource audiosource;

	// Use this for initialization
	void Start () {
		audiosource = GetComponent<AudioSource>();
		//audiosource = GameObject.Find("Camera").GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision){


		Debug.Log ("wall hit man!");

		audiosource.PlayOneShot(hitSound, 0.7F);
	}

}
