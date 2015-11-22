using UnityEngine;
using System.Collections;

public class CsPangpangAni : MonoBehaviour {
	public Animator _anim;

	void Start(){
		_anim = GetComponentInChildren<Animator> ();
	}

	void Update(){

	}

	public void jump(){

	}

	public void run(){
		_anim.SetBool("setFelt",false);
	}

	public void felt(){
		_anim.SetBool("setFelt",true);
	}

}