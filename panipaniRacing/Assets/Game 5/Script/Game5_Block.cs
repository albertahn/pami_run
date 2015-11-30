using UnityEngine;
using System.Collections;

public class Game5_Block : MonoBehaviour
{
	public int hp = 3;					//Hit points
	public int score;					//The score the player
	public Texture2D[] tex;				//Textures
	public GameObject mesh;				//Mesh
	private GameObject player;			//The player
	public AudioClip audioCollision;	//Collision sound
	
	void Start()
	{
		//Find the player
		player = GameObject.Find("Player");	
	}
	
	void OnCollisionEnter(Collision collision)
	{
		//If relative velocity is bigger than 22
        if (collision.relativeVelocity.magnitude > 22)
		{
			//Remove 3 from hp
			hp-=3;
			//Play collision sound
			audio.clip = audioCollision;
			audio.Play();
		}
		//If relative velocity is bigger than 15
		else if (collision.relativeVelocity.magnitude > 15)
		{
			//Remove 2 from hp
			hp-=2;
			//Play collision sound
			audio.clip = audioCollision;
			audio.Play();
		}
		//If relative velocity is bigger than 5
		else if (collision.relativeVelocity.magnitude > 5)
		{
			//Remove 1 from hp
			hp-=1;
			//Play collision sound
			audio.clip = audioCollision;
			audio.Play();
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
			//Set main texture
			mesh.renderer.material.mainTexture = tex[1];
			//Add score
			player.GetComponent<Game5_Player>().AddScore(score);
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
