	public var mesh : GameObject;			//Mesh
	public var tex : Texture2D[];			//Textures
	private var showTex : int;				//Selected texture
	public var updateTexTime : float;		//Texture update time
	private var tmpUpdateTexTime : float;	//Tmp texture update time

	function Update ()
	{
		//If tmpUpdateTexTime is bigger than updateTexTime
		if (tmpUpdateTexTime > updateTexTime)
		{
			//Set tmpUpdateTexTime to 0
			tmpUpdateTexTime = 0;
			//Add 1 to showTex
			showTex++;
			//If showTex is bigger than tex.Length
			if (showTex >= tex.Length)
			{
				//Kill
				Destroy(gameObject);
			}
			//If showTex is less than tex.Length
			else
			{
				//Set main texture
				mesh.renderer.material.mainTexture = tex[showTex];
			}
		}
		//If tmpUpdateTexTime is less than updateTexTime
		else
		{
			//Add 1 to tmpUpdateTexTime
			tmpUpdateTexTime += 1 * Time.deltaTime;
		}
	}