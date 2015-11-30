	public var left : GameObject;		//The left prefab of the fruit
	public var right : GameObject;		//The right prefab of the fruit
	public var splat : GameObject;		//The splat prefab of the fruit
	public var force : float;			//The left and right force
	public var torque : float;			//The rotation speed after we are hit
	
	private var canBeDead = false;		//If we can destroy the object
	private var screen : Vector3;		//Position on the screen
	private var player : GameObject;	//The player
	private var rotDir = 50.0f;			//The rotate spped

	function Start ()
	{
		//If we tag is Fruit
		if (gameObject.tag == "Fruit")
		{
			//Find player
			player = GameObject.Find("Player");
		}
		//If random is 1
		if (Random.Range(0,2) > 0)
		{
			//Change rotate speed
			rotDir = -50;
		}
	}
	
	function Update ()
	{
		//Set screen position
		screen = Camera.main.WorldToScreenPoint(transform.position);
		//If we can die and are not on the screen
		if (canBeDead && screen.y < -20)
		{
			//If we tag is Fruit
			if (gameObject.tag == "Fruit")
			{
				//Remove 1 lives from the player
				player.GetComponent(Game10_Player).lives--;
			}
			//Destroy
			Destroy(gameObject);
		}
		//If we cant die and are on the screen
		else if (!canBeDead && screen.y > -10)
		{
			//We can die
			canBeDead = true;
		}
		
		//Rotate
		transform.Rotate(Vector3(0,0,rotDir) * Time.deltaTime);
	}
	
	public function Hit()
	{
		var go : GameObject;
		//Spawn left prefab of the fruit
		go = Instantiate(left,transform.position,transform.rotation) as GameObject;
		//Add force
		go.rigidbody.AddForce(-transform.right * force);
		//Add torque
		go.rigidbody.AddTorque(Vector3(0,0,torque));
		
		//Spawn right prefab of the fruit
		go = Instantiate(right,transform.position,transform.rotation) as GameObject;
		//Add force
		go.rigidbody.AddForce(transform.right * force);
		//Add torque
		go.rigidbody.AddTorque(Vector3(0,0,-torque));
		
		//Spawn splat prefab of the fruit
		Instantiate(splat,Vector3(transform.position.x,transform.position.y,1),transform.rotation);
		//Destroy
		Destroy(gameObject);
	}