private var player : Transform; //player transform

function Start ()
{
	//Find the player
	player = GameObject.Find("Player").transform;
}

function Update ()
{
	//If the player y position is higher than the platform
	//We will move the platform to the same z position as the player
	//So that the player will hit the collider
	if (player.position.y > transform.position.y + 0.7f)
	{
		transform.position = Vector3(transform.position.x,transform.position.y,0);
	}
}