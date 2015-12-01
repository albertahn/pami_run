using UnityEngine;
using System.Collections;

public class ArrowCtrl : MonoBehaviour {
	public int damage = 20;
	public float speed = 1000.0f; //7000.0f;
	public Collider childCollider;
	public float flytime;
	private SphereCollider spherecollider;
	private TrailRenderer trailrender;

	private bool flying = true;

	private float stopTime;
	private Transform anchor;

	// maincommitchange albert test
	void Start () {

		this.stopTime = Time.time + this.flytime;
		Destroy (gameObject, 5.0f);

		//rigidbody.AddRelativeForce(transform.forward * speed); 
		rigidbody.AddForce (transform.forward * speed);
		spherecollider = GetComponent<SphereCollider>();
		trailrender = GetComponent<TrailRenderer> ();

		//this.stopTime = Time.time + this.flytime;

	}
	
	// Update is called once per frame
	void Update () {

		//if(this.stopTime <= Time.time){
		if (flying == false) {

			GameObject.Destroy (rigidbody);

		} else {
		
		}

		//if(this.flying){
	//		this.rotate();
///		}
}//end updtate



	void OnCollisionEnter(Collision collision){


		if (this.flying) {

			this.flying = false;
			this.transform.position = collision.contacts[0].point;
			//this.childCollider.isTrigger = true;

			transform.parent = collision.transform;

			GameObject anchora = new GameObject("ARROW_ANCHOR");
			anchora.transform.position = this.transform.position;
			anchora.transform.rotation = this.transform.rotation;
			anchora.transform.parent = collision.transform;
			this.anchor = anchora.transform;

			Destroy (rigidbody);
			Destroy (spherecollider);
			Destroy (trailrender);
			//GameObject.Destroy(rigidbody);
			collision.gameObject.SendMessage("arrow hit", SendMessageOptions.DontRequireReceiver);


			}
	
	}


}