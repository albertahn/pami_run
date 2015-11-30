	public var spawnTime : float;			//Spawn Time
	public var apple : GameObject;			//Apple prefab
	public var bomb : GameObject;			//Bomb prefab
	public var upForce = 750.0f;			//Up force
	public var leftRightForce = 200.0f;		//Left and right force
	public var maxX : float;				//Max x spawn position
	public var minX : float;				//Min x spawn position

	function Start()
	{
		//Start the spawn update
		StartCoroutine("Spawn");
	}
	
	function Spawn()
	{
		//Wait spawnTime
		yield WaitForSeconds(spawnTime);
		//Spawn prefab is apple
		var prefab = apple;
		//If random is over 30
		if (Random.Range(0,100) < 30)
		{
			//Spawn prefab is bomb
			prefab = bomb;
		}
		//Spawn prefab add randomc position
		var go = Instantiate(prefab,Vector3(Random.Range(minX,maxX + 1),transform.position.y,0),Quaternion.Euler(0,0,Random.Range(-50, 50))) as GameObject;
		//If x position is over 0 go left
		if (go.transform.position.x > 0)
		{
			go.rigidbody.AddForce(Vector3(-leftRightForce,upForce,0));
		}
		//Else go right
		else
		{
			go.rigidbody.AddForce(Vector3(leftRightForce,upForce,0));
		}
		
		//Start the spawn again
		StartCoroutine("Spawn");
	}