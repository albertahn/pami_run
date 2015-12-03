	public var skin : GUISkin;					//GUI skin
	public var balls = 33;						//Balls left
	public var tmpBalls : GameObject[];			//Tmp balls
	public var ball : GameObject;				//The ball we are using
	public var prefabBall : GameObject;			//Ball prefab
	public var ballStartPosition : Transform;	//Ball start position
	public var ropes : GameObject[];			//Ropes
	public var cameraGo : GameObject;			//Main camera
	public var maxCameraXPosition : int;		//Max camera x position
	public var minCameraXPosition : int;		//Min camera x position
	public var audioSlingshot : AudioClip;		//Slingshot sound
	private var touchDownPosition : Vector3;	//The first touch position
	private var touchPosition : Vector3;		//Touch position
	private var down = false;					//Is finger on the screen
	private var moveCamera = false;				//Is moving camera
	private var autoMoveCamera = false;			//Auto move camera
	private var velocity : Vector2;				//Velocity
	private var score : int;					//Score
	private var enemys : int;					//Enemys
	private var win = false;					//Have we won
	private var dead = false;					//Are we daed
	
	function Start ()
	{
		//Set screen orientation to landscape left
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		//Find enemys
		var enemyGo = GameObject.FindGameObjectsWithTag("Enemy");
		//Go through all the enemys
		for (var i = 0;i < enemyGo.Length;i++)
		{
			//Add enemy
			enemys++;
			//Set name
			var a = enemyGo[i].name;
			enemyGo[i].name = a;
		}
		//Set sleep time tp never
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		
		//Set label color to black
		skin.label.normal.textColor = Color.black;
	}

	function Update()
	{
		//If auto move camera
		if (autoMoveCamera)
		{
			//Move camera
			cameraGo.transform.position = Vector3(Mathf.SmoothDamp(cameraGo.transform.position.x, maxCameraXPosition, velocity.x, 0.3f),cameraGo.transform.position.y,cameraGo.transform.position.z);
			if (cameraGo.transform.position.x >= maxCameraXPosition - 0.1f)
			{
				//Set autoMoveCamera to false
				autoMoveCamera = false;
			}
		}
		
		//If the game is not running on a android device
		if (Application.platform != RuntimePlatform.Android)
		{
			//Set touchPosition to mouse position
			touchPosition = Camera.main.ScreenToWorldPoint (Vector3 (Input.mousePosition.x,Input.mousePosition.y,0));
		}
		//If the game is running on a android device
		else
		{
			//Set touchPosition to finger position
			touchPosition = Camera.main.ScreenToWorldPoint (Vector3 (Input.GetTouch(0).position.x,Input.GetTouch(0).position.y,0));
		}
		
		//Set touchPosition z to 0
		touchPosition = new Vector3(touchPosition.x,touchPosition.y,0);
		//Get left mouse button up
		if (Input.GetMouseButtonUp(0))
		{
			//If move camera
			if (moveCamera)
			{
				//Set moveCamera to false 
				moveCamera = false;
			}
			//If finger is on the screen
			if (down)
			{
				//Set down to false
				down = false;
				
				//Make the ball look at the ball start position
				ball.transform.LookAt(ballStartPosition);
				//Set isKinematic to false
				ball.rigidbody.isKinematic = false;
//Add forward force
				ball.rigidbody.AddForce(ball.transform.forward * (575 * Vector3.Distance(touchPosition,ballStartPosition.position)));
				//Set ball position
				ball.transform.position = ballStartPosition.position;
				
				//Go through all ropes
				for (var i = 0;i < ropes.Length;i++)
				{
					//Set LineRenderer position 1
					ropes[i].GetComponent(LineRenderer).SetPosition(1,ballStartPosition.position);
				}
				//Set autoMoveCamera to true
				autoMoveCamera = true;
				//Set ball to null
				ball = null;
				//Play slingshot sound
				audio.clip = audioSlingshot;
				audio.Play();
				//Start NewBall
				StartCoroutine("NewBall");
			}
		}
		//Get left mouse button down
		if (Input.GetMouseButtonDown(0))
		{
			//Set touchDownPosition z to 0
			touchDownPosition = Vector3(touchPosition.x,touchPosition.y,0);
			//If ball is not null and the distance between touchPosition and ball position is less than 1
			if (ball != null && Vector3.Distance(touchPosition,ball.transform.position) < 1)
			{
				//Set down to true
				down = true;
			}
			else
			{
				//Set moveCamera to true
				moveCamera = true;
			}
		}

		//If finger is on the screen and the distance between touchPosition and touchPosition is less than 5.5
		if (down && Vector3.Distance(touchDownPosition,touchPosition) < 5.5f)
		{
			//Set ball position
			ball.transform.position = touchPosition;
			
			//Go through all ropes
			for (var ii = 0;ii < ropes.Length;ii++)
			{
				//Set LineRenderer position
				ropes[ii].GetComponent(LineRenderer).SetPosition(1,ball.transform.position);
			}
		}
		//moveCamera is true and autoMoveCamera is false
		if (moveCamera && !autoMoveCamera)
		{
			//Set a to touchDownPosition.x - touchPosition.x (to see which side we will go)
			//If a is bigger than 0 go left, if it is less than 0 go right
			var a = touchDownPosition.x - touchPosition.x;
			//Get camera position
			var b = cameraGo.transform.position.x;
			//Set c to b + a
			var c = b+a;
			
			//If camera position is bigger than maxCameraXPosition
			if (b >= maxCameraXPosition)
			{
				//If we are trying to go right
				if (c > b)
				{
					return;
				}
			}
			//If camera position is less than minCameraXPosition
			if (b <= minCameraXPosition)
			{
				//If we are trying to go left
				if (c < b)
				{
					return;
				}
			}
			
			//Move camera
			cameraGo.transform.position = Vector3(c,cameraGo.transform.position.y,cameraGo.transform.position.z);
		}
		
		//If balls is 0
		if (balls == 0)
		{
			//If we cant find any balls
			if (GameObject.Find("Ball") == null && GameObject.Find("Ball(Clone)") == null)
			{
				//If we have not won
				if (!win)
				{
					//Kill
					dead = true;
				}
			}
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
		
		//If we are dad or have won
		if (win || dead)
		{
			//If we have won
			if (win)
			{
				GUI.Label(new Rect(Screen.width / 2 - 20,Screen.height / 2 - 100,300,300),"Win");
			}
			//If are dead
			else if (dead)
			{
				GUI.Label(new Rect(Screen.width / 2 - 20,Screen.height / 2 - 100,300,300),"Dead");
			}
			//Play Again Button
			if(GUI.Button(new Rect(Screen.width / 2 - 90,Screen.height / 2 - 60,180,50),"Play Again"))
			{
				Application.LoadLevel("Game 5");
			}
			//Menu Button
			if(GUI.Button(new Rect(Screen.width / 2 - 90,Screen.height / 2,180,50),"Menu"))
			{
				Application.LoadLevel("Menu");
			}
		}
	}
	
	public function AddScore(_score : int)
	{
		//Add _score value to score
		score += _score;
	}
	public function RemoveEnemy()
	{
		//Remove 1 enemy
		enemys--;
		//If enemys is 0
		if (enemys == 0)
		{
			//Start Win
			StartCoroutine("Win");
		}
	}
	function NewBall()
	{
		//Remove 1 ball
		balls--;
		//Wait 1 second
		yield WaitForSeconds(1);
		//If balls is bigger than 0
		if (balls > 0)
		{
			//Instantiate ball
			ball = Instantiate(prefabBall,ballStartPosition.position,ballStartPosition.transform.rotation) as GameObject;
			//Kill tmp balls
			Destroy(tmpBalls[tmpBalls.Length - balls]);
		}
	}
	function Win()
	{
		//Wait 2 second
		yield WaitForSeconds(2);
		//We have won
		win = true;
	}