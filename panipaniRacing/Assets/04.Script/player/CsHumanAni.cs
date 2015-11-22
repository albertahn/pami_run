using UnityEngine;
using System.Collections;

public class CsHumanAni : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void jump(){
		animation.Play ("jump_pose");
	}
	
	public void run(){
		animation.Play ("run");
	}
	
	public void felt(){
		animation.Play("idle");
	}
}
