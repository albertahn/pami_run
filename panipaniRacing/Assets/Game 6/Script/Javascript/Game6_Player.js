	public var skin : GUISkin;					//GUI skin
	public var mesh : GameObject;				//Mesh
	public var cameraGo : GameObject;			//Main camera
	public var block : GameObject;				//Block prefab
	public var explosion : GameObject;			//Explosion prefab
	public var moveSpeed : float;				//Move speed
	public var jumpSpeed : float;				//Jump speed
	private var dir : Vector3;					//The direction the player is moving
	private var score : int;					//Score
	private var rightTouchPad : GameObject;		//Right touchpad
	private var dead = false;					//Are we dead
	private var start = false;					//Has the game started
	private var velocity : Vector2;				//Velocity
	private var spawnPositionX : int;			//X spawn position
	private var spawnPositionY : float;			//Y spawn position
	private var newPositionY = false;			//New y position
	private var addToPositionY : float;			//The amount we add to the y position
	
	function Start ()
	{
		//Set screen orientation to landscape left
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		//Find right touchpad
		rightTouchPad = GameObject.Find("RightTouchPad");
		//Start SetupJoysticks
		StartCoroutine("SetupJoysticks");
		//Set label color to black
		skin.label.normal.textColor = Color.white;
		//Set sleep time to never
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	function Update ()
	{
		//If we are not dead and the game has started
		if (!dead && start)
		{
			//Update player
			MovePlayer();
		}
		else
		{
			//If the game is not running on a android device
			if (Application.platform != RuntimePlatform.Android)
			{
				//Get Space key down
				if (Input.GetKey(KeyCode.Space))
				{
					//Start the game
					start = true;
				}
			}
			//If the game is running on a android device
			else
			{
				//Get touchpad tapcount
				var tC = rightTouchPad.GetComponent(Joystick).tapCount;
				//If touchpad tapcount is not 0
				if (tC != 0)
				{
					//Start the game
					start = true;
				}
			}
		}
		
		//Update camera
		MoveCamera();
		
		//Instantiate Level
		InstantiateLevel();
		
		//Get score
		score = Vector3.Distance(Vector3(0,0,-0.2f),transform.position);
	}
	
	function MovePlayer()
	{
		//If the game is not running on a android device
		if (Application.platform != RuntimePlatform.Android)
		{
			//Get Space key down
			if (Input.GetKey(KeyCode.Space))
			{
				//Set direction to up
				dir = Vector3(moveSpeed,jumpSpeed,0);
			}
			else
			{
				//Set direction to down
				dir = Vector3(moveSpeed,-jumpSpeed,0);
			}
		}
		//If the game is running on a android device
		else
		{
			//Get touchpad tapcount
			var tC = rightTouchPad.GetComponent(Joystick).tapCount;
			
			//If touchpad tapcount is not 0
			if (tC != 0)
			{
				//Set direction to up
				dir = Vector3(moveSpeed,jumpSpeed,0);
			}
			//If touchpad tapcount is 0
			else
			{
				//Set direction to down
				dir = Vector3(moveSpeed,-jumpSpeed,0);
			}
		}
		
		//Move player
		transform.Translate(dir * Time.smoothDeltaTime);
	}
	
	function MoveCamera()
	{
		//Move camera
		cameraGo.transform.position = Vector3(Mathf.SmoothDamp(cameraGo.transform.position.x, transform.position.x, velocity.x, Time.smoothDeltaTime), cameraGo.transform.position.y,-10);
	}
	
	function InstantiateLevel()
	{
		//If spawnPositionX is less the player position + 15
		if (spawnPositionX < transform.position.x + 15)
		{
			//If we can set new y position
			if (newPositionY)
			{
				//Get random int
				var upDown = Random.Range(0,2);
				//If upDown is bigger than 0
				if (upDown > 0)
				{
					//If spawnPositionY is less than 2
					if (spawnPositionY < 2)
					{
						//Add 0.4 to y position
						addToPositionY = 0.4f;
					}
					//If spawnPositionY is bigger than 2
					else
					{
						//Set y addToPositionY to 0
						addToPositionY = 0;
					}
				}
				//If upDown is 0
				else
				{
					//If spawnPositionY is bigger than -2
					if (spawnPositionY > -2)
					{
						//Add -0.4 to y position
						addToPositionY = -0.4f;
					}
					//If spawnPositionY is less than -2
					else
					{
						//Set y addToPositionY to 0
						addToPositionY = 0;
					}
				}
				//We cant set new y position
				newPositionY = false;
			}
			//If we cant set new y position
			else
			{
				//We can set new y position
				newPositionY = true;
			}
			//Add addToPositionY to spawnPositionY
			spawnPositionY += addToPositionY;
			
			//Spwan new blocks
			Instantiate(block,Vector3(spawnPositionX,spawnPositionY,0),Quaternion.identity);
			
			//Add 1 to spawnPositionX
			spawnPositionX++;
		}
	}
	
	function OnGUI()
	{
		GUI.skin = skin;
		
		//Score
		GUI.Label(new Rect(10,10,300,300),"Score: " + score.ToString());

		//Menu Button
		if(GUI.Button(new Rect(Screen.width - 120,0,120,40),"Menu"))
		{
			Application.LoadLevel("Menu");
		}
		
		//Are we dead
		if (dead)
		{
			//Play Again Button
			if(GUI.Button(new Rect(Screen.width / 2 - 90,Screen.height / 2 - 60,180,50),"Play Again"))
			{
				Application.LoadLevel("Game 6");
			}
			//Menu Button
			if(GUI.Button(new Rect(Screen.width / 2 - 90,Screen.height / 2,180,50),"Menu"))
			{
				Application.LoadLevel("Menu");
			}
		}
	}
	
	function OnTriggerEnter(other : Collider)
	{
		//If we are in a enemy trigger
		if (other.tag == "Enemy")
		{
			//Set dead to true
			dead = true;
			//Instantiate explosion
			Instantiate(explosion,transform.position,Quaternion.identity);
			//Dont show renderer
			mesh.renderer.enabled = false;
		}
	}
	
	function SetupJoysticks()
	{
		//Set touchpad position
		rightTouchPad.transform.position = Vector3(1,0,0);
		
		//Wait 1 second
		yield WaitForSeconds(1);
		
		//Start touchpad
		rightTouchPad.GetComponent(Joystick).StartGame();
	}