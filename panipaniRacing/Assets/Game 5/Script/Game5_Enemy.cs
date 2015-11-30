using UnityEngine;
using System.Collections;

public class Game5_Enemy : MonoBehaviour
{
	public int hp = 3;				//Hit points
	public int score;				//The score the player
	public Texture2D[] tex;			//Textures
	public GameObject mesh;			//Mesh
	public AudioClip audioDead;		//Dead sound
	public GameObject number500;	//Number prefab
	private GameObject player;		//The player
	private bool hit;				//Is hit
	private bool dead;				//Are dead
	
	void Start()
	{
		//Find player
		player = GameObject.Find("Player");	
	}

	void OnCollisionEnter(Collision collision)
	{
		//Are we dead
		if (dead)
		{
			return;
		}
		//If are not been hit
		if (!hit)
		{
			//Set hit to true
			hit = true;
			return;
		}
		//If relative velocity is bigger than 10
        if (collision.relativeVelocity.magnitude > 10)
		{
			//Remove 3 from hp
			hp-=3;
		}
		//If relative velocity is bigger than 5
		else if (collision.relativeVelocity.magnitude > 5f)
		{
			//Remove 2 from hp
			hp-=2;
		}
		//If relative velocity is bigger than 2
		else if (collision.relativeVelocity.magnitude > 2f)
		{
			//Remove 1 from hp
			hp-=1;
		}
		
		//If hp is 2
		if (hp == 2)
		{
			//Set main texture
			mesh.renderer.material.mainTexture = tex[0];
		}
		//If hp is 1
		else if (hp == 1)
		{
			//Set main texture
			mesh.renderer.material.mainTexture = tex[1];
		}
		//If hp is 0
		else if (hp <= 0)
		{
			//Play dead sound
			audio.clip = audioDead;
			audio.Play();
			//We are dead
			dead = true;
			//Add score
			player.GetComponent<Game5_Player>().AddScore(score);
			//Remove enemy from player
			player.GetComponent<Game5_Player>().RemoveEnemy();
			//Spawn number prefab
			Instantiate(number500,transform.position,Quaternion.identity);
			//Start DestroyGO
			StartCoroutine("DestroyGO");
		}
    }
	
	IEnumerator DestroyGO()
	{
		//Wait 0.1 second
		yield return new WaitForSeconds(0.1f);
		//Kill
		Destroy(gameObject);
	}
}
