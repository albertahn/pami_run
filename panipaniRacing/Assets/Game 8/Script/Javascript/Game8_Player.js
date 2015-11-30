	public var skin : GUISkin;				//GUI skin
	public var colorRoot : GameObject;		//The root gameobject of all the colors
	public var colors : GameObject[];		//All the colors in order as we will need them
	private var colorsLength : int;			//Number of colors
	public var lights : GameObject[];		//Lights
	private var nextColor = "";				//Next color
	private var nextColorGO : int;			//Next color (gameobject)
	private var time : float;				//The length of the game
	private var startTime = false;			//Has the game started
	private var oldRedTapCount : int;		//Old red tap count
	private var oldBlueTapCount : int;		//Old blue tap count
	private var win = false;				//Win
	private var countdown = true;			//Are we counting down
	private var countdownInt = 3;			//The count number
	public var redTouchPad : GameObject;	//red touchpad
	public var blueTouchPad : GameObject;	//blue touchpad
		
	function Start ()
	{
		//Set screen orientation to landscape left
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		//Find BlueTouchPad
		blueTouchPad = GameObject.Find("BlueTouchPad");
		//Find RedTouchPad
		redTouchPad = GameObject.Find("RedTouchPad");
		//Start SetupJoysticks
		StartCoroutine("SetupJoysticks");
		//Set sleep time to never
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		//Go through all the colors
		for(var i = 0;i < colors.Length;i++)
		{
			//Add 1 to colorsLength
			colorsLength++;
			//Set name
			var a = colors[i].name;
			colors[i].name = a;
		}
		//Set nextColorGO to 0
		nextColorGO = 0;
		//Set nextColor to the next gameobjects tag (the color)
		nextColor = colors[nextColorGO].tag;
		//Start StartCountdown
		StartCoroutine("StartCountdown");
	}
	
	function Update ()
	{
		//If the game is not running on a android device
		if (Application.platform != RuntimePlatform.Android)
		{
			//If get A key down
			if (Input.GetKeyDown(KeyCode.A) && !countdown)
			{
				//If nextColor is blue
				if(nextColor == "Blue")
				{
					//Start NextColor
					NextColor();
				}
			}
			//If get D key down
			if (Input.GetKeyDown(KeyCode.D) && !countdown)
			{
				//If nextColor is red
				if(nextColor == "Red")
				{
					//Start NextColor
					NextColor();
				}
			}
			
			//If the game has not started and we has not won and we are not counting down
			if (!startTime && !win && !countdown)
			{
				//If get A key down or get D key down
				if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
				{
					//Start the game
					startTime = true;
				}
			}
		}
		//If the game is running on a android device
		else
		{
			//Get red touchpad tap count
			var rC = redTouchPad.GetComponent(Joystick).tapCount;
			//Get blue touchpad tap count
			var bC = blueTouchPad.GetComponent(Joystick).tapCount;
			
			//If touchpad tap count is not equal to oldBlueTapCount and we are not counting down
			if (bC != oldBlueTapCount && bC != 0 && !countdown)
			{
				//Set oldBlueTapCount to touchpad tap count
				oldBlueTapCount = bC;
				//If nextColor is blue
				if(nextColor == "Blue")
				{
					//Start NextColor
					NextColor();
				}
			}
			//If touchpad tap count is not equal to oldBlueTapCount and we are not counting down
			if (rC != oldRedTapCount && rC != 0 && !countdown)
			{
				//Set oldredTapCount to touchpad tap count
				oldRedTapCount = rC;
				//If nextColor is red
				if(nextColor == "Red")
				{
					//Start NextColor
					NextColor();
				}
			}
			
			//If touchpad tap count is 0
			if (bC == 0)
			{
				//Set oldBlueTapCount to 0
				oldBlueTapCount = 0;
			}
			if (rC == 0)
			{
				//Set oldRedTapCount to 0
				oldRedTapCount = 0;
			}
			
			//If the game has not started and we has not won and we are not counting down
			if (!startTime && !win && !countdown)
			{
				//If blue touchpad tap count are not 0 or red touchpad tap count are not 0
				if (bC != 0 || rC != 0)
				{
					//Start the game
					startTime = true;
				}
			}
		}
		
		//Has the game started
		if (startTime)
		{
			//Add 1 to time
			time += 1 * Time.deltaTime;
		}
	}
	
	function NextColor()
	{
		//Set colorRoot position
		colorRoot.transform.position = Vector3(colorRoot.transform.position.x - 1,colorRoot.transform.position.y,colorRoot.transform.position.z);
		//Set color material colro to grey
		colors[nextColorGO].renderer.material.color = Color.grey;
		
		//If nextColorGO is less than colorsLength
		if (nextColorGO < colorsLength)
		{
			//If nextColorGO is less than colorsLength - 1
			if (nextColorGO < colorsLength - 1)
			{
				//Set nextColor to next color tag
				nextColor = colors[nextColorGO + 1].tag;
				//If nextColor is red
				if (nextColor == "Red")
				{
					//Set light color to red
					lights[0].GetComponent(Light).color = Color.red;
					lights[1].GetComponent(Light).color = Color.red;
				}
				//If nextColor is blue
				else
				{
					//Set light color to blue
					lights[0].GetComponent(Light).color = Color.blue;
					lights[1].GetComponent(Light).color = Color.blue;
				}
			}
			//If nextColorGO is bigger than colorsLength - 1
			else
			{
				//We have won
				win = true;
				//Set startTime to false
				startTime = false;
				//Set nextColor null
				nextColor = "null";
				
				//Set light color to grey
				lights[0].GetComponent(Light).color = Color.grey;
				lights[1].GetComponent(Light).color = Color.grey;
			}
		}
		
		//Add 1 to nextColorGO
		nextColorGO++;
	}
	
	function OnGUI()
	{
		GUI.skin = skin;
		
		//Time
		GUI.Label(new Rect(10,10,200,200),"Time: " + time);
		//If we are counting down
		if (countdown)
		{
			//Countdown
			GUI.Label(new Rect(Screen.width / 2,10,200,200),countdownInt.ToString());
		}
		//If we are not counting down
		else
		{
			//GO
			GUI.Label(new Rect(Screen.width / 2,10,200,200),"GO");
		}
		//Menu Button
		if(GUI.Button(new Rect(Screen.width - 120,0,120,40),"Menu"))
		{
			Application.LoadLevel("Menu");
		}
		//If we have won
		if (win)
		{
			//Play Again Button
			if(GUI.Button(new Rect(Screen.width / 2 - 90,Screen.height / 2 - 60,180,50),"Play Again"))
			{
				Application.LoadLevel("Game 8");
			}
			//Menu Button
			if(GUI.Button(new Rect(Screen.width / 2 - 90,Screen.height / 2,180,50),"Menu"))
			{
				Application.LoadLevel("Menu");
			}
		}
		
		//If the game is not running on a android device
		if (Application.platform != RuntimePlatform.Android)
		{
		//A and D
			GUI.Label(new Rect(30,Screen.height - 50,200,200),"A");
			GUI.Label(new Rect(Screen.width - 50,Screen.height - 50,200,200),"D");
		}
	}
	
	function SetupJoysticks()
	{
		//Set joysticks position
		blueTouchPad.transform.position = Vector3(0,0,0);
		redTouchPad.transform.position = Vector3(1,0,0);
		
		//Wait 1 second
		yield WaitForSeconds(1);
		
		//Start Joysticks
		blueTouchPad.GetComponent(Joystick).StartGame();
		redTouchPad.GetComponent(Joystick).StartGame();
	}
	
	function StartCountdown()
	{
		//Wait 1 second
		yield WaitForSeconds(1);
		//Remove 1 from countdownInt
		countdownInt--;
		
		//Wait 1 second
		yield WaitForSeconds(1);
		//Remove 1 from countdownInt
		countdownInt--;
		
		//Wait 1 second
		yield WaitForSeconds(1);
		//Remove 1 from countdownInt
		countdownInt--;
		
		//Set countdown to false
		countdown = false;
	}