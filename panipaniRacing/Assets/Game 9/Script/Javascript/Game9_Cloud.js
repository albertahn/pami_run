	public var scrollSpeed = 0.5F;	//The speed the of the texture offset
	
    function Update()
	{
		//Make it smooth
        var offset = Time.time * scrollSpeed;
		
		//Set the texture offset
        renderer.material.mainTextureOffset = Vector2(offset, 0);
    }