	public var target : Transform;	//The target
	
	function  Update ()
	{
		//Set position
		transform.position = Vector3(target.position.x,10,target.position.z);
	}