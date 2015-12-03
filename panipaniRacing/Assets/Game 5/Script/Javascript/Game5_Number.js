	public var speed : float;		//Move speed
	public var destroyTime : float;	//Life time
	
	function Start ()
	{
		//Start DestroyGO
		StartCoroutine("DestroyGO");
	}
	
	function Update ()
	{
		//Move up
		transform.Translate(Vector3.up * speed * Time.smoothDeltaTime);
		
	}
	
	function DestroyGO()
	{
	
		//Wait
		yield WaitForSeconds(destroyTime);
		//Kill
		Destroy(gameObject);
		
	}