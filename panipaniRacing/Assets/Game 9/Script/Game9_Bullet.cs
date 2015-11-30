using UnityEngine;
using System.Collections;

public class Game9_Bullet : MonoBehaviour
{
	public float speed;			//Move speed
	public float destroyTime;	//Time it takes to destroy
	public int damage = 20;		//Damage
	
	void Start ()
	{
		//Start DestroyGo
		StartCoroutine("DestroyGo");
	}
	
	void Update ()
	{
		//Move bullet
		transform.Translate(new Vector3(0,1,0) * speed * Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider other)
	{
		//If we are in a enemy trigger
		if (other.tag == "Enemy")
		{
			//Hit enemy
			other.GetComponent<Game9_Enemy>().Hit(damage);
			//Destroy
			Destroy(gameObject);
		}
	}

	IEnumerator DestroyGo()
	{
		//Wait
        yield return new WaitForSeconds(destroyTime);
		//Destroy
		Destroy(gameObject);
    }
}
