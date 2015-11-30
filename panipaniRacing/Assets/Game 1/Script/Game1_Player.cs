using UnityEngine;
using System.Collections;

public class Game1_Player : MonoBehaviour
{
	public GUISkin skin;			//The GUI Skin
	public GameObject ray;			//The position of the ray that will check if we a on a platform
	public float moveSpeed;			//Move Speed
	public float jumpSpeed;			//Jump Speed
	public float gravity;			//Gravity
	public AudioClip audioJump; 	//Jump Sound
	public AudioClip audioDead; 	//Dead Sound
	public GameObject platform; 	//The prefab of the platform we will spawn
	public GameObject cameraGo; 	//The Main Camera
	private int score;				//The Score
	private Vector3 dir;			//The direction the player is moving
	private Vector3 screen;			//The screen position of the player to see if we are on the screen
	private bool updateRay = true;	//To control the ray update
	private bool dead;				//To see if we are dead
	private float bestYPosition;	//The highest y position (score)
	private GameObject lastPlatform;//The highest platform so we now where to start spawning new platform from
	public Transform spawnPosition;	//The spawn position
	public float spawnDistance;		//The distance between platforms
	
	void Start ()
	{
		//Sets the screen orientation to portrait
		Screen.orientation = ScreenOrientation.Portrait;
		
		//Spawns a new platform 
		lastPlatform = Instantiate(platform,spawnPosition.position,Quaternion.identity) as GameObject;
		
		//Sets the sleep time to never
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		
		//Sets the label color to blacks
		skin.label.normal.textColor = Color.black;
	}
	
	void Update ()
	{
		//If we are not dead
		if (!dead)
		{
			//Run the player move update
			MoveUpdate();
			
			//Run raycast update
			UpdateRay();
			
			//Find the player position on the screen
			screen = Camera.main.WorldToScreenPoint(transform.position);
			
			//If we are not on the screen
			if (screen.y < 0 - 50)
			{
				//kill the player and play dead sound
				dead = true;
				GetComponent<AudioSource>().clip = audioDead;
				GetComponent<AudioSource>().Play();
			}
			//If new best height
			if (transform.position.y > bestYPosition)
			{
				//Update the score and camera y position
				bestYPosition = transform.position.y;
				score = (int)Vector3.Distance(new Vector3(0,0,0),transform.position);
				cameraGo.transform.position = new Vector3(0,bestYPosition,-10);
			}
			
			//If we reached the distance to spawn a new platform
			if (lastPlatform.transform.position.y + spawnDistance < spawnPosition.position.y)
			{
				//Spawn Platform
				InstantiatePlatform();
			}
		}
	}
	
	void MoveUpdate()
	{
		//If the game is running on a android device
		if (Application.platform == RuntimePlatform.Android)
		{
			//If the phone is tilt more than 0.1 and the player is not on the edge of the level
			if (Input.acceleration.y > 0.1 && screen.x > 35)
			{
				//Set the move direction
				dir.x = -Input.acceleration.y * moveSpeed;
			}
			//If the phone is tilt more than -0.1 and the player is not on the edge of the level
			else if (Input.acceleration.y < -0.1 && screen.x < Screen.width - 35)
			{
				//Set the move direction
				dir.x = -Input.acceleration.y * moveSpeed;
			}
			//If the phone is not being tilt
			else
			{
				//Set the move direction to 0
				dir.x = 0;
			}
		}
		//If the game is not running on a android device
		else
		{
			//If the A key is down
			if (Input.GetKey(KeyCode.A) && screen.x > 35)
			{
				//Set the move direction to -5
				dir.x = -5;
			}
			//If the D key is down
			else if (Input.GetKey(KeyCode.D) && screen.x < Screen.width - 35)
			{
				//Set the move direction to 5
				dir.x = 5;
			}
			else
			{
				//Set the move direction to 0
				dir.x = 0;
			}
		}
		
		//Add gravity
		dir.y -= gravity * Time.smoothDeltaTime;
		//Move the player
		transform.Translate(dir  * Time.smoothDeltaTime);
	}
	
	void UpdateRay()
	{
		//If we can update the ray
		if (updateRay)
		{
			//If we are hitting a object
			RaycastHit hit;
			if (Physics.Raycast(ray.transform.position, Vector3.down, out hit, 0.5f))
			{
				//Sets the updateRay to false
				updateRay = false;
				//Jump
				Jump(hit);
				return;
			}
		}
	}
	
	void Jump(RaycastHit _hit)
	{
		//If we are hitting a platform
		if (_hit.transform.gameObject.tag == "Platform")
		{
			//Set the move direction
			dir.y = jumpSpeed;
			
			//Play jump sound
			GetComponent<AudioSource>().clip = audioJump;
			GetComponent<AudioSource>().Play();
		}
		
		//Sets the updateRay to true
		updateRay = true;
	}
	
	void InstantiatePlatform()
	{
		//Spawn new platform
		lastPlatform = Instantiate(platform,new Vector3(Random.Range(-13,13) * 0.2f,spawnPosition.position.y,1),Quaternion.identity) as GameObject;
	}
	
	void OnGUI()
	{
		GUI.skin = skin;
		
		//Score
		GUI.Label(new Rect(10,10,300,300),"Score: " + score.ToString());
		
		//Menu Button
		if(GUI.Button(new Rect(Screen.width - 120,0,120,40),"Menu"))
		{
			//Set time scale to 1
			Time.timeScale = 1;
			//Load menu scene
			Application.LoadLevel("Menu");
		}
		//If dead
		if (dead)
		{
			//Play Again Button
			if(GUI.Button(new Rect(Screen.width / 2 - 90,Screen.height / 2 - 60,180,50),"Play Again"))
			{
				Application.LoadLevel("Game 1");
			}
			//Menu Button
			if(GUI.Button(new Rect(Screen.width / 2 - 90,Screen.height / 2,180,50),"Menu"))
			{
				Application.LoadLevel("Menu");
			}
		}
	}
}
