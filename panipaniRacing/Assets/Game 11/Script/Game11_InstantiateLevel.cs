using UnityEngine;
using System.Collections;

public class Game11_InstantiateLevel : MonoBehaviour
{
	public Transform startPlatform;		//The first platform
	public GameObject[] platforms;		//The platforms we can spawn
	public GameObject[] coins;			//The coins prefab we can spawn
	public GameObject platformTurning;	//The turn platform prefab
	public GameObject lastblock;
	
	private GameObject oldPlatforms;	//All the old platforms
	private GameObject newPlatforms;	//All the new platforms
	private GameObject tmpPlatform;		//Tmp platforms
	private Transform player;			//The Player
	private int dir;					//Move direction 
	private bool spawnCoin;				//Can we spawn coins

	public GameObject ResumeButton;

	private GameObject _platform;

	public float laterTime;
	
	void Start ()
	{
		//Find player
		player = GameObject.Find("Horse_Prefab").transform;		
		//Find old platforms
		oldPlatforms = GameObject.Find("OldPlatforms");
		//Find new platforms
		newPlatforms = GameObject.Find("NewPlatforms");
		//Set tmpPlatform to the startPlatform
		tmpPlatform = startPlatform.gameObject;
		
		//Spawn new platforms
		SpawnPlatform("Forward");

		laterTime = Time.deltaTime +1140;
	}

	public void SpawnPlatform(string _dir)
	{

		//If direction is over 3
		if (dir > 3)
		{
			//Set direction to 0
			dir = 0;
		}
		//If direction is under 0
		else if (dir < 0)
		{
			//Set direction to 3
			dir = 3;
		}
		
		//Set euler angles to the same as the player euler angles
		Quaternion rotation = Quaternion.identity;
		rotation.eulerAngles = player.eulerAngles;
		
		//Destroy the old platforms
		Destroy(oldPlatforms);
		//Set oldPlatforms to newPlatforms
		oldPlatforms = newPlatforms;
		//Rename the platforms to "OldPlatform"
		oldPlatforms.name = "OldPlatform";
		//Make new roto platform
		newPlatforms = new GameObject("NewPlatform");
		
		//Spawn 10 platforms
		for (int i = 0; i < 10; i++)
		{
			//Set the spawn prefab to the first of all of our platforms
			 _platform = platforms[0];
			//Get random coin prefab
			GameObject _coin = coins[Random.Range(0,3)];
			
			//If it is the last platform we are spawning
			/*if (i == 19)
			{
				//Set the spawn prefab to turn platform
				_platform = platformTurning;
				//We cant spawn coins
				spawnCoin = false;
			}*/

			if (i == 9)
			{
				//Set the spawn prefab to turn platform
				_platform = lastblock;
				//We cant spawn coins
				spawnCoin = false;
			}


			if(Time.deltaTime > laterTime && i !=9){

				_platform =  platforms[0];

			}else{


			//If it is numper 1 4 or 7 we are spawning
			 if (i == 2 || i == 5 || i == 8)
			{
				//Get random platform
				_platform = platforms[Random.Range(1,platforms.Length)];
				//We cant spawn coins
				spawnCoin = false;
			}
			else
			{
				//If random is over 35
				if (Random.Range(0,100) < 35)
				{
					//We can spawn coins
					spawnCoin = true;
				}
				else
				{
					//We cant spawn coins
					spawnCoin = false;
				}
			}//else coin

		}//not first 10 seconds
			//Make new position
			Vector3 pos = new Vector3(0,0,0);
			
			//If direction is 0 (right)
			if (dir == 0)
			{
				//Set position so that it will go right
				pos = new Vector3(tmpPlatform.transform.localPosition.x,0,tmpPlatform.transform.localPosition.z + 10);
			}
			//If direction is 1 (left)
			else if (dir == 1)
			{
				//Set position so that it will go left
				pos = new Vector3(tmpPlatform.transform.localPosition.x + 10,0,tmpPlatform.transform.localPosition.z);
			}
			//If direction is 2 (down)
			else if (dir == 2)
			{
				//Set position so that it will go down
				pos = new Vector3(tmpPlatform.transform.localPosition.x,0,tmpPlatform.transform.localPosition.z - 10);
			}
			//If direction is 3 (up)
			else if (dir == 3)
			{
				//Set position so that it will go up
				pos = new Vector3(tmpPlatform.transform.localPosition.x - 10,0,tmpPlatform.transform.localPosition.z);
			}
			//Spawn new platforms
			tmpPlatform = Instantiate(_platform,pos,rotation) as GameObject;
			//Set the parent of the new platforms
			tmpPlatform.transform.parent = newPlatforms.transform;
			
			//If we can spawn coin
			if (spawnCoin)
			{
				//Spawn coin
				GameObject coin = Instantiate(_coin,pos,rotation) as GameObject;
				//Set the parent of the coin
				coin.transform.parent = newPlatforms.transform;
			}
		}
	}//spawn 

	public void PauseGame (){

		Time.timeScale = 0;

		GameObject.Find("Canvas").transform.Find("resumeButton").active = true;

	}//set plat

	public void UnPause(){

		Time.timeScale = 1;

		GameObject.Find("Canvas").transform.Find("resumeButton").active = false;

	}

}