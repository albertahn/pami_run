	public var spawnSpeed : float;		//Spawn speed
	public var enemys : GameObject[];	//Enemy prefabs
	
	function Start()
	{
		//Start Spawn
		StartCoroutine("Spawn");
	}
	
	function Spawn()
	{
		//Wait
        yield WaitForSeconds(spawnSpeed);
        
        //Spawn enemy
		Instantiate(enemys[Random.Range(0,enemys.Length)],Vector3(Random.Range(-10,11) * 0.2f,transform.position.y,1),Quaternion.identity);
		
		//Start Spawn
		StartCoroutine("Spawn");
    }