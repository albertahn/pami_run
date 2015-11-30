	public var skin : GUISkin;		//GUI Skin
	public var score : int;			//Score
	public var lives : int;			//Lives
	
	private var pos : Vector3;		//Position
	private var dead = false;		//If we are dead
	
	function Start ()
	{
		//Set screen orientation to landscape
		Screen.orientation = ScreenOrientation.Landscape;
		//Set sleep timeout to never
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	function Update ()
	{
		//If dead
		if (dead)
		{
			//Set collider to false
			collider.enabled = false;
			return;
		}
		//If we have 0 lives left
		if (lives < 1)
		{
			//Kill
			dead = true;
			//Set collider to false
			collider.enabled = false;
		}
		
		//If the game is running on a android device
		if (Application.platform == RuntimePlatform.Android)
		{
			//If we are hitting the screen
			if (Input.touchCount == 1)
			{
				//Find screen touch position
				pos = Camera.main.ScreenToWorldPoint(Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 1));
				//Set position
				transform.position = Vector3(pos.x,pos.y,0);
				//Set collider to true
				collider.enabled = true;
				return;
			}
			//Set collider to false
			collider.enabled = false;
		}
		//If the game is not running on a android device
		else
		{
			//Find mouse position
			pos = Camera.main.ScreenToWorldPoint(Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
			//Set position
			transform.position = Vector3(pos.x,pos.y,0);
		}
	}
	
	function OnTriggerEnter(other : Collider)
	{
		//If we hits a fruit
		if (other.tag == "Fruit")
		{
			//Run hit function
			other.GetComponent(Game10_Fruit).Hit();
			//Add score
			score += 1;
		}
		//If we hits a enemy (bomb)
		else if (other.tag == "Enemy")
		{
			//Run hit function
			other.GetComponent(Game10_Bomb).Hit();
		}
	}
	
	function OnGUI()
	{
		GUI.skin = skin;
		
		//Score
		GUI.Label(new Rect(10,10,300,300),score.ToString());
		
		//If dead
		if (dead)
		{
			//Show "Lives: 0"
			GUI.Label(new Rect(10,Screen.height - 35,300,300),"Lives: 0");
		}
		else
		{
			//Show lives left
			GUI.Label(new Rect(10,Screen.height - 35,300,300),"Lives: " + lives.ToString());
		}

		//Menu Button
		if(GUI.Button(new Rect(Screen.width - 120,0,120,40),"Menu"))
		{
			//Load Menu scene
			Application.LoadLevel("Menu");
		}
		//If dead
		if (dead)
		{
			//Play Again Button
			if(GUI.Button(new Rect(Screen.width / 2 - 90,Screen.height / 2 - 60,180,50),"Play Again"))
			{
				Application.LoadLevel("Game 10");
			}
			//Menu Button
			if(GUI.Button(new Rect(Screen.width / 2 - 90,Screen.height / 2,180,50),"Menu"))
			{
				Application.LoadLevel("Menu");
			}
		}	
	}