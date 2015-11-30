using UnityEngine;
using System.Collections;

public class Game5_Ball : MonoBehaviour
{
	private bool hit;	//Is we hit
	
	void Update()
	{
		//Has we been hit and are the velocity bigger than 0.5
		if(hit && rigidbody.velocity.magnitude < 0.5)
		{
			//Start DestroyGo
			StartCoroutine("DestroyGo");
		}
	}
	
	IEnumerator DestroyGo()
	{
		//Wait 1 second
		yield return new WaitForSeconds(1);
		//Kill
		Destroy(gameObject);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		//We are hit
		hit = true;
	}
}
