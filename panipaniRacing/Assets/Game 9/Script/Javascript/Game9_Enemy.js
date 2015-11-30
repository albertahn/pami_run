	public var mesh : GameObject;		//Mesh
	public var speed = 2.0f;			//Move speed
	public var hp = 100;				//Health
	public var explosion : GameObject;	//Explosion prefab
	public var score : int;				//Score
	private var player : GameObject;	//The player
	private var canBeHit = true;		//Can we be hit

	function Start ()
	{
		//Find player
		player = GameObject.Find("Player");
	}
	
	function Update ()
	{
		//Move enemy
		transform.Translate(Vector3.down * speed * Time.deltaTime);
		
		//If position y is less than -10
		if (transform.position.y < -10)
		{
			//Destroy
			Destroy(gameObject);
		}
	}
	
	public function Hit(_damage : int)
	{
		//If we can be hit
		if (canBeHit)
		{
			//Start Hit2
			StartCoroutine("Hit2");
			//Remove _damage value from hp
			hp -= _damage;
			//If hp is less than 1
			if (hp < 1)
			{
				//Instantiate explosion
				Instantiate(explosion,transform.position,Quaternion.identity);
				//Add score to player
				player.GetComponent(Game9_Player).AddScore(score);
				//Destroy
				Destroy(gameObject);
			}
		}
	}

	function Hit2()
	{
		//We cant be hit
		canBeHit = false;
		
		//Set material color to red
		mesh.renderer.material.color = Color.red;
		
		//Wait 0.1 second
		yield WaitForSeconds(0.1f);
		
		//Set material color to white
		mesh.renderer.material.color = Color.white;
		
		//We can be hit
		canBeHit = true;
	}