	private var canBeDead = false;		//If we can destroy the object
	private var screen : Vector3;		//Position on the screen
	private var player : GameObject;	//The player
	
	function Start ()
	{
		//Find the player
		player = GameObject.Find("Player");
	}
	
	function Update ()
	{
		//Set screen position
		screen = Camera.main.WorldToScreenPoint(transform.position);
		//If we can die and are not on the screen
		if (canBeDead && screen.y < -20)
		{
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
		transform.Rotate(Vector3(0,0,50) * Time.deltaTime);
	}
	
	public function Hit()
	{
		//Set player lives to 0
		player.GetComponent(Game10_Player).lives = 0;
		//Destroy
		Destroy(gameObject);
	}