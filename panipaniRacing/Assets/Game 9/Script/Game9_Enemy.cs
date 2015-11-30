using UnityEngine;
using System.Collections;

public class Game9_Enemy : MonoBehaviour
{
	public GameObject mesh;			//Mesh
	public float speed = 2;			//Move speed
	public int hp = 100;			//Health
	public GameObject explosion;	//Explosion prefab
	public int score;				//Score
	private GameObject player;		//The player
	private bool canBeHit = true;	//Can we be hit

	void Start ()
	{
		//Find player
		player = GameObject.Find("Player");
	}
	
	void Update ()
	{
		//Move enemy
		transform.Translate(Vector3.down * speed * Time.deltaTime);
		
		//If position y is less than -10
		if (transform.position.y < -10)
		{
			//Destroy
			Destroy(gameObject);
		}
	}
	
	public void Hit(int _damage)
	{
		//If we can be hit
		if (canBeHit)
		{
			//Start Hit2
			StartCoroutine("Hit2");
			//Remove _damage value from hp
			hp -= _damage;
			//If hp is less than 1
			if (hp < 1)
			{
				//Instantiate explosion
				Instantiate(explosion,transform.position,Quaternion.identity);
				
				//Add score to player
				player.GetComponent<Game9_Player>().AddScore(score);
				
				//Destroy
				Destroy(gameObject);
			}
		}
	}

	IEnumerator Hit2()
	{
		//We cant be hit
		canBeHit = false;
		
		//Set material color to red
		mesh.renderer.material.color = Color.red;
		
		//Wait 0.1 second
		yield return new WaitForSeconds(0.1f);
		
		//Set material color to white
		mesh.renderer.material.color = Color.white;
		
		//We can be hit
		canBeHit = true;
	}
}