using UnityEngine;
using System.Collections;

public class Game9_Player : MonoBehaviour
{
	public GUISkin skin;				//GUI skin
	public GameObject mesh;				//Mesh
	public Transform[] bulletSpawnPos;	//Bullet spawn position
	public GameObject bullet;			//Bullet prefab
	public GameObject explosion;		//Explosion prefab
	private int hp = 3;					//Health
	private int score;					//Score
	private float tmpFireTime;			//Tmp fire time
	private bool dead;					//Are we dead
	private Vector3 pos;				//Position
	private bool canBeHit = true;		//Can we be hit
	
	void Start ()
	{
		//Set label color to black
		skin.label.normal.textColor = Color.black;
		
		//Set screen orientation to portrait
		Screen.orientation = ScreenOrientation.Portrait;
		
		//Set sleep time to never
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	void Update()
	{
		//If we are not dead
		if (!dead)
		{
			//Update
			MoveUpdate();
			ShotUpdate();
		}
	}
	
	void MoveUpdate()
	{
		//If the game is running on a android device
		if (Application.platform == RuntimePlatform.Android)
		{
			//Get screen position
			pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 1));
		}
		//If the game is not running on a android device
		else
		{
			//Get screen position
			pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
		}

		transform.position = new Vector3(pos.x,pos.y + 1f,pos.z);
	}
	
	void ShotUpdate()
	{
		//If tmpFireTime is bigger than 0.2
		if (tmpFireTime > 0.2f)
		{
			//If the game is running on a android device
			if (Application.platform == RuntimePlatform.Android)
			{
				//If finger is on screen
				if (Input.GetTouch(0).tapCount != 0)
				{
					//Go through all the bullet spawn position
					for (int i = 0; i < bulletSpawnPos.Length; i++)
					{
						//Instantiate bullet
						Instantiate(bullet,bulletSpawnPos[i].position,Quaternion.identity);
						//Set tmpFireTime to 0
						tmpFireTime = 0;
					}
				}
				else
				{
					//Set tmpFireTime to 0.2
					tmpFireTime = 0.2f;
				}
			}
			//If the game is not running on a android device
			else
			{
				//If get left mouse button down
				if (Input.GetMouseButton(0))
				{
					//Go through all the bullet spawn position
					for (int i = 0; i < bulletSpawnPos.Length; i++)
					{
						//Instantiate bullet
						Instantiate(bullet,bulletSpawnPos[i].position,Quaternion.identity);
						//Set tmpFireTime to 0
						tmpFireTime = 0;
					}
				}
				else
				{
					//Set tmpFireTime to 0.2
					tmpFireTime = 0.2f;
				}
			}
		}
		//If tmpFireTime is less than 0.2
		else
		{
			//Add 1 to tmpFireTime
			tmpFireTime += 1 * Time.deltaTime;
		}
	}
	
	IEnumerator Hit()
	{
		//We cant be hit
		canBeHit = false;
		
		//Dont show player
		mesh.renderer.enabled = false;
		//Wait 0.2 second
		yield return new WaitForSeconds(0.2f);
		
		//Show player
		mesh.renderer.enabled = true;
		//Wait 0.2 second
		yield return new WaitForSeconds(0.2f);
		
		//Dont show player
		mesh.renderer.enabled = false;
		//Wait 0.2 second
		yield return new WaitForSeconds(0.2f);
		
		//Show player
		mesh.renderer.enabled = true;
		//Wait 0.2 second
		yield return new WaitForSeconds(0.2f);
		
		//Dont show player
		mesh.renderer.enabled = false;
		//Wait 0.2 second
		yield return new WaitForSeconds(0.2f);
		
		//Show player
		mesh.renderer.enabled = true;
		
		//We can be hit
		canBeHit = true;
	}
	
	public void AddScore(int _score)
	{
		//Add _score to score
		score += _score;
	}
	
	void OnTriggerEnter(Collider other)
	{
		//If we are in a enemy trigger
		if (other.tag == "Enemy")
		{
			//If we can be hit
			if (canBeHit)
			{
				//Remove 1 from hp
				hp--;
				
				//If hp is less than 1
				if (hp < 1)
				{
					//We are dead
					dead = true;
					
					//Dont show player
					mesh.renderer.enabled = false;
					
					//Set collider to false
					collider.enabled = false;
				}
				//If hp is bigger than 0
				else
				{
					//Start Hit
					StartCoroutine("Hit");
				}
				
				//Instantiate explosion
				Instantiate(explosion,transform.position,Quaternion.identity);
				//Destroy
				Destroy(other.gameObject);
			}
		}
	}
	
	void OnGUI()
	{
		GUI.skin = skin;
		
		//Score
		GUI.Label(new Rect(10,10,300,300),"Score: " + score.ToString());
		//Lives
		GUI.Label(new Rect(10,40,300,300),"Lives: " + hp.ToString());
		
		//Menu Button
		if(GUI.Button(new Rect(Screen.width - 120,0,120,40),"Menu"))
		{
			Application.LoadLevel("Menu");
		}
		//If we are dead
		if (dead)
		{
			//Play Again Button
			if(GUI.Button(new Rect(Screen.width / 2 - 90,Screen.height / 2 - 60,180,50),"Play Again"))
			{
				Application.LoadLevel("Game 9");
			}
			//Menu Button
			if(GUI.Button(new Rect(Screen.width / 2 - 90,Screen.height / 2,180,50),"Menu"))
			{
				Application.LoadLevel("Menu");
			}
		}	
	}
}
