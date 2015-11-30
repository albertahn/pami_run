using UnityEngine;
using System.Collections;

public class Game6_Player : MonoBehaviour
{
	public GUISkin skin;				//GUI skin
	public GameObject mesh;				//Mesh
	public GameObject cameraGo;			//Main camera
	public GameObject block;			//Block prefab
	public GameObject explosion;		//Explosion prefab
	public float moveSpeed;				//Move speed
	public float jumpSpeed;				//Jump speed
	private Vector3 dir;				//The direction the player is moving
	private int score;					//Score
	private GameObject rightTouchPad;	//Right touchpad
	private bool dead;					//Are we dead
	private bool start;					//Has the game started
	private Vector2 velocity;			//Velocity
	private int spawnPositionX;			//X spawn position
	private float spawnPositionY;		//Y spawn position
	private bool newPositionY;			//New y position
	private float addToPositionY;		//The amount we add to the y position
	
	void Start ()
	{
		//Set screen orientation to landscape left
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		//Find right touchpad
		rightTouchPad = GameObject.Find("RightTouchPad");
		//Start SetupJoysticks
		StartCoroutine("SetupJoysticks");
		//Set label color to black
		skin.label.normal.textColor = Color.white;
		//Set sleep time to never
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	void Update ()
	{
		//If we are not dead and the game has started
		if (!dead && start)
		{
			//Update player
			MovePlayer();
		}
		else
		{
			//If the game is not running on a android device
			if (Application.platform != RuntimePlatform.Android)
			{
				//Get Space key down
				if (Input.GetKey(KeyCode.Space))
				{
					//Start the game
					start = true;
				}
			}
			//If the game is running on a android device
			else
			{
				//Get touchpad tapcount
				float tC = rightTouchPad.GetComponent<Joystick>().tapCount;
				//If touchpad tapcount is not 0
				if (tC != 0)
				{
					//Start the game
					start = true;
				}
			}
		}
		//Update camera
		MoveCamera();
		
		//Instantiate Level
		InstantiateLevel();
		
		//Get score
		score = (int)Vector3.Distance(new Vector3(0,0,-0.2f),transform.position);
	}
	
	void MovePlayer()
	{
		//If the game is not running on a android device
		if (Application.platform != RuntimePlatform.Android)
		{
			//Get Space key down
			if (Input.GetKey(KeyCode.Space))
			{
				//Set direction to up
				dir = new Vector3(moveSpeed,jumpSpeed,0);
			}
			else
			{
				//Set direction to down
				dir = new Vector3(moveSpeed,-jumpSpeed,0);
			}
		}
		//If the game is running on a android device
		else
		{
			//Get touchpad tapcount
			float tC = rightTouchPad.GetComponent<Joystick>().tapCount;
			
			//If touchpad tapcount is not 0
			if (tC != 0)
			{
				//Set direction to up
				dir = new Vector3(moveSpeed,jumpSpeed,0);
			}
			//If touchpad tapcount is 0
			else
			{
				//Set direction to down
				dir = new Vector3(moveSpeed,-jumpSpeed,0);
			}
		}
		
		//Move player
		transform.Translate(dir * Time.smoothDeltaTime);
	}
	
	void MoveCamera()
	{
		//Move camera
		cameraGo.transform.position = new Vector3(Mathf.SmoothDamp(cameraGo.transform.position.x, transform.position.x, ref velocity.x, Time.smoothDeltaTime), cameraGo.transform.position.y,-10);
	}
	
	void InstantiateLevel()
	{
		//If spawnPositionX is less the player position + 15
		if (spawnPositionX < transform.position.x + 15)
		{
			//If we can set new y position
			if (newPositionY)
			{
				//Get random int
				int upDown = Random.Range(0,2);
				//If upDown is bigger than 0
				if (upDown > 0)
				{
					//If spawnPositionY is less than 2
					if (spawnPositionY < 2)
					{
						//Add 0.4 to y position
						addToPositionY = 0.4f;
					}
					//If spawnPositionY is bigger than 2
					else
					{
						//Set y addToPositionY to 0
						addToPositionY = 0;
					}
				}
				//If upDown is 0
				else
				{
					//If spawnPositionY is bigger than -2
					if (spawnPositionY > -2)
					{
						//Add -0.4 to y position
						addToPositionY = -0.4f;
					}
					//If spawnPositionY is less than -2
					else
					{
						//Set y addToPositionY to 0
						addToPositionY = 0;
					}
				}
				//We cant set new y position
				newPositionY = false;
			}
			//If we cant set new y position
			else
			{
				//We can set new y position
				newPositionY = true;
			}
			//Add addToPositionY to spawnPositionY
			spawnPositionY += addToPositionY;
			
			//Spwan new blocks
			Instantiate(block,new Vector3(spawnPositionX,spawnPositionY,0),Quaternion.identity);
			
			//Add 1 to spawnPositionX
			spawnPositionX++;
		}
	}
	
	void OnGUI()
	{
		GUI.skin = skin;
		
		//Score
		GUI.Label(new Rect(10,10,300,300),"Score: " + score.ToString());
		
		//Menu Button
		if(GUI.Button(new Rect(Screen.width - 120,0,120,40),"Menu"))
		{
			Application.LoadLevel("Menu");
		}
		
		//If we are dead
		if (dead)
		{
			//Play Again Button
			if(GUI.Button(new Rect(Screen.width / 2 - 90,Screen.height / 2 - 60,180,50),"Play Again"))
			{
				Application.LoadLevel("Game 6");
			}
			//Menu Button
			if(GUI.Button(new Rect(Screen.width / 2 - 90,Screen.height / 2,180,50),"Menu"))
			{
				Application.LoadLevel("Menu");
			}
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		//If we are in a enemy trigger
		if (other.tag == "Enemy")
		{
			//Set dead to true
			dead = true;
			//Instantiate explosion
			Instantiate(explosion,transform.position,Quaternion.identity);
			//Dont show renderer
			mesh.renderer.enabled = false;
		}
	}
	
	IEnumerator SetupJoysticks()
	{
		//Set touchpad position
		rightTouchPad.transform.position = new Vector3(1,0,0);
		
		//Wait 1 second
		yield return new WaitForSeconds(1);
		
		//Start touchpad
		rightTouchPad.GetComponent<Joystick>().StartGame();
	}
}