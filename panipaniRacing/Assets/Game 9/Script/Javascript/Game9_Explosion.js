	public var main : GameObject;			//Main camera
	public var speed : float;				//Time it takes to change texture
	private var tmpSpeed : float;			//Tmp time
	public var offset : float;				//Offset
	public var index = 0;					//The texture to show
	public var maxIndex = 0;				//Max texture
    public var textureName = "_MainTex";	//Material main texture

	
    function LateUpdate() 
    {
    	//If tmpSpeed is bigger than speed
		if (tmpSpeed >= speed)
		{
			//If index is the same as maxIndex
			if (index == maxIndex)
			{
				Destroy(main);
			}
			//Set tmpSpeed to 0
			tmpSpeed = 0;
			//Set texture offset
			renderer.material.SetTextureOffset(textureName,Vector2(index * offset,0));
			//Add 1 to index
			index++;			
		}
		//If tmpSpeed is less than speed
		else
		{
			//Add 1 to tmpSpeed
			tmpSpeed = tmpSpeed + 1 *Time.deltaTime;	
		}
    }