	public var hp = 3;						//Hit points
	public var score : int;					//The score the player
	public var tex : Texture2D[];			//Textures
	public var mesh : GameObject;			//Mesh
	public var audioDead : AudioClip;		//Dead sound
	public var number500 : GameObject;		//Number prefab
	private var player : GameObject;		//The player
	private var hit = false;				//Is hit
	private var dead = false;				//Are dead
	
	function Start()
	{
		//Find player
		player = GameObject.Find("Player");	
	}

	function OnCollisionEnter(collision : Collision)
	{
		//Are we dead
		if (dead)
		{
			return;
		}
		//If are not been hit
		if (!hit)
		{
			//Set hit to true
			hit = true;
			return;
		}
		//If relative velocity is bigger than 10
        if (collision.relativeVelocity.magnitude > 10)
		{
			//Remove 3 from hp
			hp-=3;
		}
		//If relative velocity is bigger than 5
		else if (collision.relativeVelocity.magnitude > 5f)
		{
			//Remove 2 from hp
			hp-=2;
		}
		//If relative velocity is bigger than 2
		else if (collision.relativeVelocity.magnitude > 2f)
		{
			//Remove 1 from hp
			hp-=1;
		}
		
		//If hp is 2
		if (hp == 2)
		{
			mesh.renderer.material.mainTexture = tex[0];
		}
		//If hp is 1
		else if (hp == 1)
		{
			mesh.renderer.material.mainTexture = tex[1];
		}
		//If hp is 0
		else if (hp <= 0)
		{
			//Play dead sound
			audio.clip = audioDead;
			audio.Play();
			//We are dead
			dead = true;
			//Add score
			player.GetComponent(Game5_Player).AddScore(score);
			//Remove enemy from player
			player.GetComponent(Game5_Player).RemoveEnemy();
			//Spawn number prefab
			Instantiate(number500,transform.position,Quaternion.identity);
			//Start DestroyGO
			StartCoroutine("DestroyGO");
		}
    }
	
	function DestroyGO()
	{
		//Wait 0.1 second
		yield WaitForSeconds(0.1f);
		//Kill
		Destroy(gameObject);
	}