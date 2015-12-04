using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour {
	public GameObject arrow;

	public GameObject boom;
	public GameObject soundStuff;
	public Transform firePos;
	public Transform[] SFirePos= new Transform[12];
	public AudioClip fireSfx;
	//specialaudio
	public AudioClip spfireSfx;

	private Transform tr;

	public float time,time2,time3,time4;//time은 기본공격, time2는 특수공격.
	private float basicAttackCool=1.1f;
	public float specialAttackCool = 10;
	private float specialAttack2Cool = 1;
	public float quickCool=10.0f;

	private float space = 0.0f;

	public bool skillBool1;
	public bool skillBool2;
	private float dashPeriod=1;

	// Use this for initialization
	// Update is called once per frame


	void Start(){
		time = Time.time;

		tr = GetComponent<Transform> ();




		//anime

		Fire();

	
	}

	void Update () {

		Fire ();

	}//end





	
private bool fireBool;


	public void Fire(){



	fireBool = true;
	
	if (Time.time - time > basicAttackCool) {

		time = Time.time;
		StartCoroutine(this.CreateArrow());


			soundStuff.GetComponent<SoundFX>().shootArrow();
		
	}


}//end fire
	void No_Fire(){
		fireBool = false;
	}


	IEnumerator CreateArrow(){
		Instantiate (arrow, firePos.position, firePos.rotation);
		yield return null;
	}



	IEnumerator PlaySfx(AudioClip _clip){

				yield return null;
	}

}
