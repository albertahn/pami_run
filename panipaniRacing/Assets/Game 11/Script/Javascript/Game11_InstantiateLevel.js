	public var startPlatform : Transform;		//The first platform
	public var platforms : GameObject[];		//The platforms we can spawn
	public var coins : GameObject[];			//The coins prefab we can spawn
	public var platformTurning : GameObject;	//The turn platform prefab
	
	private var oldPlatforms : GameObject;		//All the old platforms
	private var newPlatforms : GameObject;		//All the new platforms
	private var tmpPlatform : GameObject;		//Tmp platforms
	private var player : Transform;				//The Player
	private var dir : int;						//Move direction 
	private var spawnCoin = false;				//Can we spawn coins
	
	function Start ()
	{
		//Find player
		player = GameObject.Find("Player").transform;
		//Find old platforms
		oldPlatforms = GameObject.Find("OldPlatforms");
		//Find new platforms
		newPlatforms = GameObject.Find("NewPlatforms");
		//Find tmp platforms
		tmpPlatform = startPlatform.gameObject;
		
		//Spawn new platforms
		SpawnPlatform("Forward");
	}

	public function SpawnPlatform(_dir : String)
	{
		//If direction is left
		if (_dir == "Left")
		{
			//Remove 1
			dir--;
		}
		//If direction is right
		else if (_dir == "Right")
		{
			//Add 1
			dir++;
		}
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
		var rotation = Quaternion.identity;
		rotation.eulerAngles = player.eulerAngles;

		//Destroy the old platforms
		Destroy(oldPlatforms);
		//Set oldPlatforms to newPlatforms
		oldPlatforms = newPlatforms;
		//Rename the platforms to "OldPlatform"
		oldPlatforms.name = "OldPlatform";
		//Make new roto platform
		newPlatforms = GameObject("NewPlatform");
		
		//Spawn 10 platforms
		for (var i = 0; i < 10; i++)
		{
			//Set the spawn prefab to the first of all of our platforms
			var _platform = platforms[0];
			//Get random coin prefab
			var _coin = coins[Random.Range(0,3)];
			
			//If it is the last platform we are spawning
			if (i == 9)
			{
				//Set the spawn prefab to turn platform
				_platform = platformTurning;
				//We cant spawn coins
				spawnCoin = false;
			}
			//If it is numper 1 4 or 7 we are spawning
			else if (i == 2 || i == 5 || i == 8)
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
			}
			
			//Make new position
			var pos = Vector3(0,0,0);
			
			//If direction is 0 (right)
			if (dir == 0)
			{
				//Set position so that it will go right
				pos = Vector3(tmpPlatform.transform.localPosition.x,0,tmpPlatform.transform.localPosition.z + 10);
			}
			//If direction is 1 (left)
			else if (dir == 1)
			{
				//Set position so that it will go left
				pos = Vector3(tmpPlatform.transform.localPosition.x + 10,0,tmpPlatform.transform.localPosition.z);
			}
			//If direction is 2 (down)
			else if (dir == 2)
			{
				//Set position so that it will go down
				pos = Vector3(tmpPlatform.transform.localPosition.x,0,tmpPlatform.transform.localPosition.z - 10);
			}
			//If direction is 3 (up)
			else if (dir == 3)
			{
				//Set position so that it will go up
				pos = Vector3(tmpPlatform.transform.localPosition.x - 10,0,tmpPlatform.transform.localPosition.z);
			}
			//Spawn new platforms
			tmpPlatform = Instantiate(_platform,pos,rotation) as GameObject;
			//Set the parent of the new platforms
			tmpPlatform.transform.parent = newPlatforms.transform;
			
			//If we can spawn coin
			if (spawnCoin)
			{
				//Spawn coin
				var coin = Instantiate(_coin,pos,rotation) as GameObject;
				//Set the parent of the coin
				coin.transform.parent = newPlatforms.transform;
			}
		}
	}