﻿using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {
	private GameObject _camera;
	private Transform tr;
	private Transform cameraTr;
	//public Transform target;
	
	//float zdepth;
	
	// Use this for initialization
	void Start () {
		_camera = GameObject.Find ("Main Camera");
		tr = GetComponent<Transform> ();
		cameraTr = _camera.transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		tr.LookAt (_camera.transform);
		tr.Rotate (0,180,0,Space.Self);
		//tr.rotation= Quaternion.Euler(60,0,tr.rotation.z);
	}
}
