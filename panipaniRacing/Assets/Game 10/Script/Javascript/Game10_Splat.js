	public var mesh : GameObject;		//Mesh
    private var color : Color;			//Color
    public var destroySpeed = 0.2f;		//Destroy Speed
	
	function Start()
	{
		//Set the color
		color = mesh.renderer.material.color;
	}
	
    function Update()
	{
		//Set the mesh material color
		mesh.renderer.material.color.a -= destroySpeed * Time.deltaTime;
		//If color a is 0
		if (color.a <= 0)
		{
			//Destroy
			Destroy(gameObject);
		}
    }