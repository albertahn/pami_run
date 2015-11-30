	public var speed : float;		//Move speed
	public var destroyTime : float;	//Time it takes to destroy
	public var damage = 20;			//Damage
	
	function Start ()
	{
		//Start DestroyGo
		StartCoroutine("DestroyGo");
	}
	
	function Update ()
	{
		//Move bullet
		transform.Translate(Vector3(0,1,0) * speed * Time.deltaTime);
	}
	
	function OnTriggerEnter(other : Collider)
	{
		//If we are in a enemy trigger
		if (other.tag == "Enemy")
		{
			//Hit enemy
			other.GetComponent(Game9_Enemy).Hit(damage);
			//Destroy
			Destroy(gameObject);
		}
	}

	function DestroyGo()
	{
		//Wait
        yield WaitForSeconds(destroyTime);
        //Destroy
		Destroy(gameObject);
    }