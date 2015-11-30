using UnityEngine;
using System.Collections;

public class Game5_Number : MonoBehaviour
{
	public float speed;			//Move speed
	public float destroyTime;	//Life time
	
	void Start ()
	{
		//Start DestroyGO
		StartCoroutine("DestroyGO");
	}
	
	void Update ()
	{
		//Move up
		transform.Translate(Vector3.up * speed * Time.smoothDeltaTime);
	}
	
	IEnumerator DestroyGO()
	{
		//Wait 
		yield return new WaitForSeconds(destroyTime);
		//Kill
		Destroy(gameObject);
	}
}
