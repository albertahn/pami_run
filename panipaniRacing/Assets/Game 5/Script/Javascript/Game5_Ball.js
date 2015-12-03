	private var hit = false;	//Is we hit
	
	function Update()
	{
		//Has we been hit and are the velocity bigger than 0.5
		if(hit && rigidbody.velocity.magnitude < 0.5)
		{
			//Start DestroyGo
			StartCoroutine("DestroyGo");
		}
	}
	
	function DestroyGo()
	{
		//Wait 1 second
		yield WaitForSeconds(1);
		//Kill
		Destroy(gameObject);
	}
	
	function OnCollisionEnter(collision : Collision)
	{
		//We are hit
		
		//this.transform.rigidbody.velocity = Vector3.Reflect( collision.relativeVelocity*-1, collision.contacts[0].normal );
		hit = true;
	}