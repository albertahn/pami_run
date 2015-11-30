	public var skin : GUISkin;					//GUI Skin
	public var speedLeftRight = 2000.0f;		//Move left and right speed
	public var speedForward = 3000.0f;			//Move forward speed
	public var speedJump : float;				//Jump Speed
	public var gravity = 20.0f;					//Gravity
	private var dead = false;					//Are we dead
	private var moveDirection = Vector3.zero;	//Move Direction
	private var score : float;					//Score
	private var isGrounded = true;				//Is grounded
	private var canJump = true;					//Can jump
	private var canTurn = false;				//Can turn
	private var canMoveLeft = false;			//Can move left
	private var canMoveRight = false;			//Can move right
	private var goCamera : GameObject;			//Main Camera
	private var dir : int;						//Move direction (the way we are looking)
	private var startTouchPos : Vector2;		//The first position we touch
	
	function Start ()
	{
		//Set the screen orientation to portrait
		Screen.orientation = ScreenOrientation.Portrait;
		//Set the sleep time to nerver
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		
		//Find Camera
		goCamera = GameObject.Find("Main Camera");
	}
	
	function Update()
	{
		//If we are not dead
		if (!dead)
		{
			//Update
			MoveUpdate();
			//Add 5 to the score every second
			score += 5 * Time.deltaTime;
		}
		
		//If we are hitting a object (down)
		var hit : RaycastHit;
        if (Physics.Raycast(transform.position, Vector3.down, hit,1))
		{
			//If it is not the player
            if (hit.transform.gameObject.tag != "Player")
			{
				//We are grounded
            	isGrounded = true;
            }
		}
		else
		{
			//We are in the air
			isGrounded = false;
		}
		
		//If we are hitting a object (right)
		var hitRight : RaycastHit;
        if (Physics.Raycast(transform.position, transform.right, hitRight,1))
		{
			//If it is a wall
            if (hitRight.transform.gameObject.name == "Wall")
			{
				//We can not move right
            	canMoveRight = false;
            }
		}
		else
		{
			//We can move right
			canMoveRight = true;
		}
		
		//If we are hitting a object (left)
		var hitLeft : RaycastHit;
        if (Physics.Raycast(transform.position, -transform.right, hitLeft,1))
		{
			//If it is a wall
            if (hitLeft.transform.gameObject.name == "Wall")
			{
				//We can not move left
            	canMoveLeft = false;
            }
		}
		else
		{
			//We can move left
			canMoveLeft = true;
		}
	}
	
	function MoveUpdate()
	{
		//If the game is not running on a android device
		if (Application.platform != RuntimePlatform.Android)
		{
			//The moveDirection x is the horizontal axis (A,D)
			moveDirection.x = Input.GetAxis("Horizontal");

			//If moveDirection x is bigger than 0.3 and we can move right
			if (moveDirection.x > 0.3f && canMoveRight)
			{
				//Set moveDirection x to 1
				moveDirection.x = 1f;
			}
			//If moveDirection x is less than -0.3 and we can move left
			else if (moveDirection.x < -0.3f && canMoveLeft)
			{
				//Set moveDirection x to -1
				moveDirection.x = -1f;
			}
			else
			{
				//Set moveDirection x to 0
				moveDirection.x = 0;
			}
			//If get Space key down and we can jump and is on the ground
			if (Input.GetKeyDown(KeyCode.Space) && canJump && isGrounded)
			{
				//Set canJump to false
				canJump = false;
				//Add up force
				rigidbody.AddForce(Vector3.up * 100 * speedJump);
				//Start WaitToJump
				StartCoroutine("WaitToJump");
			}
			//If can turn and get E key down
			if (canTurn && Input.GetKeyDown(KeyCode.E))
			{
				//Rotate right
				Rotate("Right");
			}
			//If can turn and get E key down
			if (canTurn && Input.GetKeyDown(KeyCode.Q))
			{
				//Rotate left
				Rotate("Left");
			}
		}
		else
		{
			//moveDirection x = the phone tilt value
			moveDirection.x = -Input.acceleration.y;
			//If moveDirection x is bigger than 0.15 and we can move right
			if (moveDirection.x > 0.15f && canMoveRight)
			{
				//Set moveDirection x to 1
				moveDirection.x = 1f;
			}
			//If moveDirection x is less than -0.15 and we can move left
			else if (moveDirection.x < -0.15f && canMoveLeft)
			{
				//Set moveDirection x to -1
				moveDirection.x = -1f;
			}
			else
			{
				//Set moveDirection x to 0
				moveDirection.x = 0;
			}
			
			//Get touches
			for (var touch : Touch in Input.touches)
			{
				//Touche phase = began
				if (touch.phase == TouchPhase.Began)
				{
					//Set first touch position
					startTouchPos = touch.position;
				}
				//Touche phase = moved
	            if (touch.phase == TouchPhase.Moved)
				{
					//If we can jump and is grounded and touch position y is bigger than first touch position y + 100
					if (canJump && isGrounded && touch.position.y > startTouchPos.y + 100)
					{
						//Set canJump to false
						canJump = false;
						//Add up force
						rigidbody.AddForce(Vector3.up * 100 * speedJump);
						//Start WaitToJump
						StartCoroutine("WaitToJump");
					}
					//If we can turn and touch position x is bigger than first touch position x + 100
					if (canTurn && touch.position.x > startTouchPos.x + 100)
					{
						//Rotate right
						Rotate("Right");
					}
					//If we can turn and touch position x is less than first touch position x - 100
					if (canTurn && touch.position.x < startTouchPos.x - 100)
					{
						//Rotate left
						Rotate("Left");
					}
				}
			}
		}
		
		//Move the player
		transform.Translate(Vector3(moveDirection.x * speedLeftRight,0,speedForward) * Time.deltaTime,Space.Self);
	}
	
	function OnTriggerEnter(other : Collider)
	{
		//If we are in a enemy trigger
		if (other.tag == "Enemy")
		{
			//Kill
			dead = true;
		}
		//If we are in a turning trigger
		if (other.tag == "Turning")
		{
			//We can turn
			canTurn = true;
		}
		//If we are in a coin trigger
		if (other.tag == "Coin")
		{
			//Add 10 to score
			score += 10;
			//Destroy coin
			Destroy(other.gameObject);
		}
	}
	function OnTriggerExit(other : Collider)
	{
		//If we are not in a turning trigger
		if (other.tag == "Turning")
		{
			//We cant turn
			canTurn = false;
		}
	}
	
	function OnGUI()
	{
		GUI.skin = skin;
		
		//Score
		GUI.Label(new Rect(10,10,300,300),(score).ToString());

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
				Application.LoadLevel("Game 11");
			}
			//Menu Button
			if(GUI.Button(new Rect(Screen.width / 2 - 90,Screen.height / 2,180,50),"Menu"))
			{
				Application.LoadLevel("Menu");
			}
		}	
	}
	
	function WaitToJump()
	{
		//Wait 1 second
		yield WaitForSeconds(1);
		canJump = true;
	}
	
	function Rotate(_dir : String)
	{
		//We can not rotate
		canTurn = false;
		//If the direction we are looking is left
		if (_dir == "Left")
		{
			//Add -90 to eulerAngles
			transform.eulerAngles += Vector3(0,-90,0);
		}
		//If the direction we are looking is right
		else
		{
			//Add 90 to eulerAngles
			transform.eulerAngles += Vector3(0,90,0);
		}
		//Spawn new platforms
		goCamera.GetComponent(Game11_InstantiateLevel).SpawnPlatform(_dir);
		
		//If the direction we are looking is left
		if (_dir == "Left")
		{
			dir--;
		}
		//If the direction we are looking is right
		else if (_dir == "Right")
		{
			dir++;
		}
		//If dir is bigger than 3
		if (dir > 3)
		{
			//Set dir to 0
			dir = 0;
		}
		//If dir is less than 0
		else if (dir < 0)
		{
			//Set dir to 3
			dir = 3;
		}
	}