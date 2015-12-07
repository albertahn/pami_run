using UnityEngine;
using System.Collections;

public class CsExtra : MonoBehaviour {
	public GameObject smoke;
	GameObject m_smoke;	
	Quaternion quatAng;

	enum myState
	{
		idle,
		hit,
		fault,
	};
	myState m_state;

	float timer;
	// Use this for initialization
	void Start () {	
		m_state = myState.idle;
		animation["hit"].speed=2;
	}
	
	// Update is called once per frame
	void Update () {
		switch(m_state){
			case myState.idle :
				if(!animation.isPlaying){
					if(Random.Range(0,2)==1)
						animation.Play("idle1");
					else
						animation.Play("idle2");
				}
			break;
			case myState.hit:
				if(Time.time - timer >1.0f){
					animation.Play("fault");
					m_state = myState.fault;
				}
			break;
		}
	}

	void OnCollisionEnter(Collision coll){
		if(coll.transform.tag=="Player" || coll.transform.tag=="ARROW" ){

			GameObject.Find("SoundStuff").GetComponent<SoundFX>().personHit();

			int dir =  (int)(transform.parent.eulerAngles.y/90);
			switch(dir){
			case 0:	rigidbody.velocity =new Vector3(Random.Range(-5,5),10,20); break;
			case 1:	rigidbody.velocity =new Vector3(20,10,Random.Range(-5,5));break;
			case 2:	rigidbody.velocity =new Vector3(Random.Range(-5,5),10,-20);break;
			case 3:	rigidbody.velocity =new Vector3(-20,10,Random.Range(-5,5));break;
			}
			
			quatAng.eulerAngles =transform.parent.eulerAngles + new Vector3(0,180,0);
			m_smoke = Instantiate(smoke,transform.position+new Vector3(0,1,0),quatAng) as GameObject;
			m_state = myState.hit;
			animation.Play("hit");
			timer = Time.time;


		}		
	}
}
