using UnityEngine;
using System.Collections;

public class Game4_Camera : MonoBehaviour
{
	public Transform target;	//The target
	
	void  Update ()
	{
		//Set position
		transform.position = new Vector3(target.position.x,10,target.position.z);
	}
}
