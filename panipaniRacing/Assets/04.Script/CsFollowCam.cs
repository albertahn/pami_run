using UnityEngine;
using System.Collections;

public class CsFollowCam : MonoBehaviour {
	public Transform target;
	public float dist;
	public float height;
	public float dampRotate;

	private Transform tr;

	public void setTarget(Transform a){
		target = a;
	}

	
	void Start(){
		dist = 10.0f;
		height = 5.0f;
		dampRotate = 15.0f;
		
		tr = transform;
	}
	
	void LateUpdate(){
		float currYAngle = Mathf.LerpAngle (tr.eulerAngles.y
		                                    ,target.eulerAngles.y
		                                    ,dampRotate*Time.deltaTime);
		Quaternion rot = Quaternion.Euler (0, currYAngle, 0);
		
		tr.position = target.position - (rot * Vector3.forward * dist) + (Vector3.up * height);
		
		
		Vector3 pointer= target.position;
		pointer.y = 4.0f;
		
		//tr.LookAt (pointer);
	}
}